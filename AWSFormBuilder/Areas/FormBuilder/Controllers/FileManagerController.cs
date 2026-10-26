using Microsoft.AspNetCore.Mvc;

namespace AWSFormBuilder.Areas.FormBuilder.Controllers
{
    /// <summary>
    /// Replacement for the old CKFinder ASP.NET (Web Forms) connector.aspx,
    /// rewritten for ASP.NET Core 8. Serves files from Areas/FormBuilder/Content/uploads.
    /// Wire this up to CKEditor's filebrowserBrowseUrl / filebrowserUploadUrl.
    /// </summary>
    [ApiController]
    [Area("FormBuilder")] // ADDED - optional, just keeps it organized under the FormBuilder area
    [Route("api/filemanager")] // unchanged - explicit route, so CKEditor calls need no changes
    public class FileManagerController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private const string RootFolderName = "uploads";

        // MODIFIED - URL prefix the browser uses. Must match the "RequestPath" you
        // configured for the Areas/FormBuilder/Content static files middleware in Program.cs
        private const string UrlPrefix = "/Areas/FormBuilder/Content/" + RootFolderName;

        // Only these extensions are allowed to be uploaded/browsed
        private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg",
            ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".zip", ".txt"
        };

        public FileManagerController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // MODIFIED - was Path.Combine(_env.WebRootPath, RootFolderName) (wwwroot/uploads).
        // Now saves/reads from Areas/FormBuilder/Content/uploads instead, using
        // ContentRootPath (project root) since this folder lives outside wwwroot.
        private string RootPhysicalPath =>
            Path.Combine(_env.ContentRootPath, "Areas", "FormBuilder", "Content", RootFolderName);

        /// <summary>
        /// Resolves a relative "path" query param (e.g. "/2026/07") into a safe
        /// physical path, blocking directory traversal outside the uploads root.
        /// </summary>
        private bool TryResolvePath(string? relativePath, out string physicalPath)
        {
            relativePath ??= "/";
            var cleaned = relativePath.Replace("\\", "/").TrimStart('/');
            var combined = Path.GetFullPath(Path.Combine(RootPhysicalPath, cleaned));

            var rootFull = Path.GetFullPath(RootPhysicalPath);
            if (!combined.StartsWith(rootFull, StringComparison.OrdinalIgnoreCase))
            {
                physicalPath = rootFull;
                return false;
            }

            physicalPath = combined;
            return true;
        }

        // GET /api/filemanager/folders?path=/
        [HttpGet("folders")]
        public IActionResult GetFolders([FromQuery] string path = "/")
        {
            if (!TryResolvePath(path, out var physicalPath))
                return BadRequest("Invalid path.");

            Directory.CreateDirectory(physicalPath); // ensure root exists on first run

            List<object> folders;
            if (Directory.Exists(physicalPath))
            {
                folders = Directory.GetDirectories(physicalPath)
                    .Select(d => new { name = Path.GetFileName(d) })
                    .OrderBy(f => f.name)
                    .Cast<object>()
                    .ToList();
            }
            else
            {
                folders = new List<object>();
            }

            return Ok(folders);
        }

        // GET /api/filemanager/files?path=/
        [HttpGet("files")]
        public IActionResult GetFiles([FromQuery] string path = "/")
        {
            if (!TryResolvePath(path, out var physicalPath))
                return BadRequest("Invalid path.");

            if (!Directory.Exists(physicalPath))
                return Ok(new List<object>());

            var relBase = path.Replace("\\", "/").TrimEnd('/');
            var files = Directory.GetFiles(physicalPath)
                .Select(f =>
                {
                    var name = Path.GetFileName(f);
                    var info = new FileInfo(f);
                    return new
                    {
                        name,
                        size = info.Length,
                        modified = info.LastWriteTimeUtc,
                        // MODIFIED - was $"/{RootFolderName}{relBase}/{name}" (served from wwwroot)
                        url = $"{UrlPrefix}{relBase}/{name}".Replace("//", "/")
                    };
                })
                .OrderBy(f => f.name)
                .ToList();

            return Ok(files);
        }

        // POST /api/filemanager/create-folder  (form: path, name)
        [HttpPost("create-folder")]
        public IActionResult CreateFolder([FromForm] string path, [FromForm] string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                return BadRequest("Invalid folder name.");

            if (!TryResolvePath(path, out var physicalPath))
                return BadRequest("Invalid path.");

            var newFolder = Path.Combine(physicalPath, name);
            Directory.CreateDirectory(newFolder);

            return Ok(new { success = true });
        }

        // POST /api/filemanager/upload?CKEditorFuncNum=1  (multipart form: path, file)
        // Accepts the uploaded file under either "file" (our own browse.html)
        // or "upload" (CKEditor 4's built-in Image Properties > Upload tab).
        [HttpPost("upload")]
        [RequestSizeLimit(20_000_000)] // 20 MB, adjust as needed
        public async Task<IActionResult> Upload(
            [FromQuery] string? CKEditorFuncNum)
        {
            var form = await Request.ReadFormAsync();
            var path = form["path"].ToString(); // optional; defaults to root if empty

            var file = Request.Form.Files["upload"] ?? Request.Form.Files["file"]
                ?? Request.Form.Files.FirstOrDefault();

            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var ext = Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(ext))
                return BadRequest($"File type {ext} is not allowed.");

            if (!TryResolvePath(path, out var physicalPath))
                return BadRequest("Invalid path.");


            Directory.CreateDirectory(physicalPath);

            // Avoid overwriting: append (1), (2)... if the name already exists
            var safeName = Path.GetFileNameWithoutExtension(file.FileName);
            var finalName = file.FileName;
            var counter = 1;
            while (System.IO.File.Exists(Path.Combine(physicalPath, finalName)))
            {
                finalName = $"{safeName}({counter}){ext}";
                counter++;
            }

            var destPath = Path.Combine(physicalPath, finalName);
            using (var stream = System.IO.File.Create(destPath))
            {
                await file.CopyToAsync(stream);
            }

            var relBase = (path ?? "/").Replace("\\", "/").TrimEnd('/');
            // MODIFIED - was $"/{RootFolderName}{relBase}/{finalName}" (served from wwwroot)
            var url = $"{UrlPrefix}{relBase}/{finalName}".Replace("//", "/");

            // CKEditor 4's classic "on complete" HTML response (calls back into
            // the opener window's CKEDITOR API). If you're on CKEditor 5, switch
            // this endpoint to return: Ok(new { url }) — see notes in the README.
            if (!string.IsNullOrEmpty(CKEditorFuncNum))
            {
                var html = $@"<script type=""text/javascript"">
                    window.parent.CKEDITOR.tools.callFunction({CKEditorFuncNum}, '{url}', '');
                </script>";
                return Content(html, "text/html");
            }

            return Ok(new { uploaded = 1, fileName = finalName, url });
        }

        // DELETE /api/filemanager/delete?path=/2026/07/photo.jpg
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return BadRequest("Path is required.");

            var relativePath = path.Replace("\\", "/").TrimStart('/');
            var physicalFile = Path.GetFullPath(Path.Combine(RootPhysicalPath, relativePath));
            var rootFull = Path.GetFullPath(RootPhysicalPath);

            if (!physicalFile.StartsWith(rootFull, StringComparison.OrdinalIgnoreCase))
                return BadRequest("Invalid path.");

            if (!System.IO.File.Exists(physicalFile))
                return NotFound();

            System.IO.File.Delete(physicalFile);
            return Ok(new { success = true });
        }
    }
}

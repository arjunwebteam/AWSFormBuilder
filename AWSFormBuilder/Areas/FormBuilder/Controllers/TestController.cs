using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
namespace AWSFormBuilder.Areas.FormBuilder.Controllers
{
    [Area("FormBuilder")]
    public class TestController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public TestController(IWebHostEnvironment env)

        {

            _env = env;

        }

        public IActionResult Index()
        {
            return Content(

               "ContentRootPath: " + _env.ContentRootPath +

               "\n\nWebRootPath: " + _env.WebRootPath

           );
            return View();
        }
    }
}


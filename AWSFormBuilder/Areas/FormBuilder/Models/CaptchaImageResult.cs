using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace AWSFormBuilder.Areas.FormBuilder.Controllers  // ✅ Fixed: Controllers not Models
{
    public class CaptchaController : Controller
    {
        private static Random rand = new Random();

        [HttpGet]
        public IActionResult ShowCaptchaImage()  // ✅ Fixed: removed wrong pixelFormat param
        {
            // 1. Generate random CAPTCHA code
            string code = GetRandomText();

            // 2. Store in session
            HttpContext.Session.SetString("CaptchaString", code);

            // 3. Create bitmap and draw
            using (Bitmap bitmap = new Bitmap(200, 60, PixelFormat.Format32bppArgb))  // ✅ Fixed: PixelFormat enum
            using (Graphics g = Graphics.FromImage(bitmap))
            using (MemoryStream ms = new MemoryStream())
            {
                // Colors and pen
                using (Pen pen = new Pen(Color.Yellow))
                using (SolidBrush blue = new SolidBrush(Color.CornflowerBlue))
                using (SolidBrush black = new SolidBrush(Color.Black))
                {
                    Rectangle rect = new Rectangle(0, 0, 200, 60);

                    // Draw background
                    g.FillRectangle(blue, rect);
                    g.DrawRectangle(pen, rect);

                    // Draw text characters
                    int counter = 0;
                    foreach (var c in code)
                    {
                        using (Font font = new Font("Tahoma", 15 + rand.Next(5, 15), FontStyle.Italic))
                        {
                            g.DrawString(c.ToString(), font, black, new PointF(10 + counter, 10));
                        }
                        counter += 28;
                    }

                    // Draw noise lines
                    DrawRandomLines(g);
                }

                // ✅ Fixed: ImageFormat.Gif (not BadImageFormatException.Gif)
                bitmap.Save(ms, ImageFormat.Gif);

                // Return image as file
                return File(ms.ToArray(), "image/gif");
            }
        }

        // Draw random lines as noise
        private void DrawRandomLines(Graphics g)
        {
            using (Pen yellowPen = new Pen(Color.Yellow, 1))
            {
                for (int i = 0; i < 20; i++)
                {
                    g.DrawLines(yellowPen, GetRandomPoints());
                }
            }
        }

        // Generate random points for a line
        private Point[] GetRandomPoints()
        {
            return new Point[]
            {
                new Point(rand.Next(0, 200), rand.Next(0, 60)),
                new Point(rand.Next(0, 200), rand.Next(0, 60))
            };
        }

        // Generate random CAPTCHA text
        private string GetRandomText()
        {
            const string chars = "0123456789ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            char[] result = new char[6];
            for (int i = 0; i < 6; i++)
            {
                result[i] = chars[rand.Next(chars.Length)];
            }
            return new string(result);
        }
    }
}
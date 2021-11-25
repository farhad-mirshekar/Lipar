using Lipar.Core.Common;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;

namespace Lipar.Web.Controllers
{
    public class CaptchaController : BaseController
    {
        #region Ctor
        public CaptchaController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        public IActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);

            //generate new question
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = $"{a} + {b} =";

            //store answer
            SetCookie(CookieDefaults.Captcha, a + b);

            //image stream
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(85, 25))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);

                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question
                gfx.DrawString(captcha, new Font("arial", 15), Brushes.Gray, 2, 0);

                //render as Jpeg
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }

        #region Utilities
        private void SetCookie(string cookieName, int value)
        {
            cookieName = $"{CookieDefaults.Prefix}{cookieName}";
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieName);

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(2),
                HttpOnly = true,
                Secure = true
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, value.ToString(), cookieOptions);
        }
        #endregion
    }
}

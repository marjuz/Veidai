using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Web.Mvc;
using System.Drawing.Imaging;

namespace FaceRec.Library
{
    public class JpegResult : ActionResult
    {
        public JpegResult(Image image, bool useBase64Response)
        {
            this.Image = image;
            this.UseBase64Response = useBase64Response;
        }

        public Image Image { get; private set; }
        public bool UseBase64Response { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;

            if (!UseBase64Response)
            {
                response.ContentType = "image/jpg";
                this.Image.Save(response.OutputStream, ImageFormat.Jpeg);
            }
            else
            {
                response.ContentType = "text/plain";
                using (MemoryStream ms = new MemoryStream())
                {
                    this.Image.Save(ms, ImageFormat.Jpeg);
                    response.Write(Convert.ToBase64String(ms.ToArray()));
                }
            }

            response.Flush();
            response.End();
        }
    }
}
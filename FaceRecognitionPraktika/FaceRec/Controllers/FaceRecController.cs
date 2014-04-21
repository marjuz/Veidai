using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Drawing;
using System.IO;
using FaceRec.Library;

using MarJuz.FaceRec.Servises;
using MarJuz.FaceRec.Servises.Entities;
using MarJuz.FaceRec.DataEntities.Entities;
using MarJuz.FaceRec.DataEntities;


namespace FaceRec.Controllers
{
    public class FaceRecController : Controller
    {
        public ActionResult Start ()
        {
            return View ();
        }

        private object _lockObj = new object();
        protected FaceDetection FaceDetection { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (this.FaceDetection == null)
            {
                lock (_lockObj)
                {
                    if (this.FaceDetection == null)
                    {
                        this.FaceDetection = new FaceDetection(requestContext.HttpContext.Server.MapPath("~/Resources/haarcascade_frontalface_default.xml"));
                    }
                }
            }

            base.Initialize(requestContext);
        }

        [HttpPost]
        public ActionResult Find(string outputType)
        {
            Bitmap bmp = null;

            WebImage img = WebImage.GetImageFromRequest();
            if (img != null)
            {
                using (MemoryStream ms = new MemoryStream(img.GetBytes()))
                {
                    bmp = new Bitmap(ms);
                }
            }

            if (bmp != null)
            {
                int maxWidth = 400;
                if (bmp.Width > maxWidth)
                {
                    Bitmap newBmp = new Bitmap(bmp, maxWidth, (int)(((float)maxWidth / (float)bmp.Width) * (float)bmp.Height));
                    bmp.Dispose();
                    bmp = newBmp;
                }

                FaceR face = this.FaceDetection.FindLargestFace(bmp);
                if (face != null)
                {
                    int padX = (int)(face.Bounds.Width * .05);
                    int padY = (int)(face.Bounds.Height * .20);

                    Rectangle newBounds = new Rectangle(face.Bounds.Left - padX, face.Bounds.Top - padY, face.Bounds.Width + (2 * padX), face.Bounds.Height + (2 * padY));

                    Bitmap faceBmp = new Bitmap(newBounds.Width, newBounds.Height);
                    using (Graphics g = Graphics.FromImage(faceBmp))
                    {
                        g.DrawImage(bmp, 0, 0, newBounds, GraphicsUnit.Pixel);
                        g.Flush();
                    }
                    return this.Jpeg(faceBmp, outputType.ToLower() == "base64");
                }
            }
            else
            {
                if (Request.HttpMethod == "POST")
                    throw new Exception("Uploaded image not found");
            }

            return View();
        }
    }
}

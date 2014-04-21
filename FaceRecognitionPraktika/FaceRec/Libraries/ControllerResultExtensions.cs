using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;

namespace FaceRec.Library
{
    public static class ControllerResultExtensions
    {
        public static JpegResult Jpeg(this Controller controller, Image img)
        {
            return new JpegResult(img, false);
        }
        public static JpegResult Jpeg(this Controller controller, Image img, bool base64)
        {
            return new JpegResult(img, base64);
        }
    }
}
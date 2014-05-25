using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarJuz.FaceDetection.servises;

namespace FaceDet.Controllers
{
    public class CreateController : Controller
    {
        //
        // GET: /Create/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            FacesDet db = new FacesDet();

            foreach (HttpPostedFileBase file in files)
            {

                string content = Path.Combine(Server.MapPath("~/"));
                
                db.MakeChanges(file.InputStream, file.FileName, content);
            }

            return Json("All files have been successfully stored.");
        }
    }
}

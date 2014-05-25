using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarJuz.FaceDetection.servises;

namespace FaceDet.Controllers
{
    public class PhotoController : Controller
    {
        //
        // GET: /Photo/

        public ActionResult Index()
        {
            DataBaseControl a = new DataBaseControl();
            var photo = a.GetPhotos();
            return View(photo);
        }

        public ActionResult Photo(int id)
        {
            DataBaseControl a = new DataBaseControl();
            var photo = a.GetPhotoId(id);
            return View(photo);
        }

        public ActionResult delete (int id)
        {
            DataBaseControl a = new DataBaseControl();
            a.DeletePhoto(id);
            return RedirectToAction("Index");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarJuz.FaceDetection.servises;
using MarJuz.FaceDetection.Entities;

namespace FaceDet.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string name)
        {
            DataBaseControl a = new DataBaseControl();
            var photo = a.GetbyName(name);

            return View(photo);
        }
        public ActionResult Find (int id)
        {
            DataBaseControl a = new DataBaseControl();
            var photo = a.GetPhotoById(id);

            return View(photo);
        }



    }
}

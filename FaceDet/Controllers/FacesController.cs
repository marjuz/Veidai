using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarJuz.FaceDetection.servises;
using MarJuz.FaceDetection.Entities;

namespace FaceDet.Controllers
{
    public class FacesController : Controller
    {
        //
        // GET: /Face/

        public ActionResult Index()
        {
            DataBaseControl a = new DataBaseControl();
            var face = a.GetFaces();
            return View(face);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            DataBaseControl a = new DataBaseControl();
            var face = a.GetFaceById(id);
            return View(face);
        }
        [HttpPost]
        public ActionResult Edit(string FaceName, int id)
        {
            DataBaseControl a = new DataBaseControl();
            a.EditFaceName(FaceName, id);
            return RedirectToAction("Index");
        }

    }
}

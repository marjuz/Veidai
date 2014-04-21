using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarJuz.FaceRec.DataEntities;
using MarJuz.FaceRec.DataEntities.Entities;

namespace FaceRec.Controllers
{
    public class FaceController : Controller
    {
        //
        // GET: /Face/

        public ActionResult Index()
        {

            List<Face> face = FaceDB.ReadFace.ToList();


            return View(face);
        }

    }
}

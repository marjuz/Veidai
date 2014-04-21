using MarJuz.FaceRec.DataEntities;
using MarJuz.FaceRec.DataEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FaceRec.Controllers
{
    public class PhotoController : Controller
    {
        //
        // GET: /Photo/

        public ActionResult Index()
        {

            List<UploadPhoto> photo = FaceDB.Read.ToList();


            return View(photo);
        }

    }
}

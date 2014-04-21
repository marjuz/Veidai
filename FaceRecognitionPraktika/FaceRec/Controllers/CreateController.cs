using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MarJuz.FaceRec.DataEntities.Entities;
using MarJuz.FaceRec.DataEntities;

namespace FaceRec.Controllers
{
    public class CreateController : Controller
    {
        //
        // GET: /Create/

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(int IsPictureRecognized, string PictureName, int NrOfFaces, string UploadDate)
        {
            Response.Write("IsPictureRecognized = " + IsPictureRecognized);
            Response.Write("PictureName = " + PictureName);
            Response.Write("NrOfFaces = " + NrOfFaces);
            Response.Write("UploadDate = " + UploadDate);

            return View();
        }

        [HttpPost]
        public ActionResult Load(int IsPictureRecognized, string PictureName, int NrOfFaces, string UploadDate)
        {
            UploadPhoto photo = new UploadPhoto();
            photo.IsPictureRecognized = 0;
            photo.PictureName = PictureName;
            photo.NrOfFaces = NrOfFaces;
            photo.UploadDate = Convert.ToDateTime(DateTime.Now);

            FaceDB.AddPhoto(photo);


            return new EmptyResult();


        }

    }
}

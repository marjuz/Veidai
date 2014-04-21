using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using MarJuz.FaceRec.Servises;
using MarJuz.FaceRec.DataEntities.Entities;
using MarJuz.FaceRec.DataEntities;

namespace FaceRec.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
                      

            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarJuz.FaceDetection.servises;

namespace FaceDet.Controllers
{
    public class HomeController : Controller
    {
        public MarJuz.FaceDetection.servises.FacesDet FacesDet
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
       
    }
}

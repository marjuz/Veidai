using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarJuz.FaceDetection.servises;

namespace FaceDet.Controllers
{
    public class NamesController : Controller
    {
        //
        // GET: /Names/

        public ActionResult Index()
        {
            DataBaseControl a = new DataBaseControl();
            var photo = a.GetDataUnRec();
            return View(photo);
        }
        [HttpPost]
        public ActionResult Send(FormCollection inputs)
        {
            List<string> names = new List<string>();
            DataBaseControl db = new DataBaseControl();
            foreach (var a in inputs.AllKeys)
            {
                
                
                var value = inputs[a];
                names.Add(value);
            }
            db.UpdateFace(names);

            return RedirectToAction("Index");
        }


    }
}

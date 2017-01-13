using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lamode.Controllers
{
    public class ImageController : Controller
    {
      
        [HttpGet]
        public ActionResult AddImage()
        {

            Photo photo = new Photo();
            return View(photo);
        }
        [HttpPost]
        public ActionResult AddImage(Photo model, HttpPostedFileBase image1, string Id)
        {
            LamodeEntities db = new LamodeEntities();
            Photo photo = new Photo();
            var p1 = db.Photos.Where(p => p.Id == Id).Count();
            if (image1 != null && p1 <= 15)
            {
                model.Photo1 = new byte[image1.ContentLength];
                image1.InputStream.Read(model.Photo1, 0, image1.ContentLength);
                model.Id = Id;

            }
            else
            {

            }
            db.Photos.Add(model);
            db.SaveChanges();

            return View(model);
        }


    }
}
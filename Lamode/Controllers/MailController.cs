using Lamode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lamode.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Message msg)
        {
            MailHelper mailer = new MailHelper();
            string response = mailer.EmailFromArvixe(
                                       new Message(msg.Sender, msg.Subject, msg.Body));
            ViewBag.Response = response;
            if (response == "Success! Your email has been sent.  Please allow up to 48 hrs for a reply.")
            {
                return RedirectToAction("Message", new { msg = ViewBag.Response });
            }
            else
            {
                return RedirectToAction("Index","Home");
            }




        }

    }
}
using Lamode.Repositiries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lamode.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search

        
        public ActionResult Index(string sortOrder)
        {
            LamodeEntities context = new LamodeEntities();
 
            UserRepository userRepo = new UserRepository();
            IEnumerable<AspNetUser> users = userRepo.GetUsers(sortOrder);

            // Store current sort filter parameter.
            ViewBag.CurrentSort = sortOrder;

            // Provide toggle option for name sort.
            if (String.IsNullOrEmpty(sortOrder))
                ViewBag.NameSortParm = UserRepository.LASTNAME;
            else
                ViewBag.NameSortParm = "";

            // Provide toggle  optionfor date sort.
            if (sortOrder == UserRepository.EMAIL)
                ViewBag.DateSortParm = UserRepository.EMAIL;
            else
                ViewBag.DateSortParm = UserRepository.LASTNAME;

            return View(users);
        }

    }
}
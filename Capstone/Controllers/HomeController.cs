using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vm = new RegisterViewModel();

            vm.Food = new[]
                {
                    new SelectListItem { Value = "1", Text = "No Preference" },
                    new SelectListItem { Value = "2", Text = "Vegitarian" },
                    new SelectListItem { Value = "3", Text = "Vegan" }
                };
            return View(vm);
        }
        public ActionResult Events()
        {
            return View();
        }
    }
}
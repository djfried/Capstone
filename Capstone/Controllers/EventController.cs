using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult CreateEvent()
        {

            var vm = new CreateEventViewModel();

            vm.TypeList = new[]
                {
                    new SelectListItem { Value = "1", Text = "Seminar" },
                    new SelectListItem { Value = "2", Text = "Meeting" },
                    new SelectListItem { Value = "3", Text = "Training" },
                    new SelectListItem { Value = "4", Text = "Social" },
                    new SelectListItem { Value = "5", Text = "Other" }
                };
            return View(vm);
        }
    }
}
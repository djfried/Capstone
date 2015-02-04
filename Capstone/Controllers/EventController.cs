using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Capstone.Models;
using Capstone.Managers;

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
        
         //post: /event/createEvent
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent(CreateEventViewModel model)
        {
            
           if(ModelState.IsValid)
               return RedirectToAction("Events", "Home");
             
            return View(model);
        }
    }
}
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
using Capstone.Container_Classes;

namespace Capstone.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult CreateEvent()
        {

            return View();
        }
        
         //post: /event/createEvent
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent(CreateEventViewModel model)
        {

            if (ModelState.IsValid)
            {
                EventManager eventManager = new EventManager();
                Event containerEvent = new Event();
                containerEvent.Title = model.Title;
                containerEvent.StartDate = Convert.ToDateTime(model.StartTime);
                containerEvent.EndDate = Convert.ToDateTime(model.EndTime);
                
                
                return RedirectToAction("Events", "Home");
            }
            return View(model);
        }
    }
}
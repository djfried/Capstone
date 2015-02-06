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
        //post: event/details
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Details(int eventID)
        {

            return View();
        }
        // GET: Event
        public ActionResult CreateEvent()
        {
            if (SessionManager.LoggedIn == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        
         //post: /event/createEvent
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent(CreateEventViewModel model)
        {

            if (SessionManager.LoggedIn == false)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                EventManager eventManager = new EventManager();
                Event containerEvent = new Event();

                containerEvent.Category = "All";
                containerEvent.Description = model.Description;
                containerEvent.EndDate = Convert.ToDateTime(model.EndTime);
                containerEvent.Location = model.Location;
                containerEvent.Logo_Path = model.LogoPath;
                containerEvent.Owner_ID = SessionManager.SessionID;
                containerEvent.StartDate = Convert.ToDateTime(model.StartTime);
                containerEvent.Status = "ON";
                containerEvent.Title = model.Title;
                containerEvent.Type = model.Type;

                EventViewModels eventModel = eventManager.CreateEvent(containerEvent);
                
                return RedirectToAction("Events", "Home");
            }
            return View(model);
        }
    }
}
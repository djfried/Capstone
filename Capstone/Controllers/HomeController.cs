using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Managers;
using Capstone.Container_Classes;

namespace Capstone.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        // When the events page is loaded find all events to display to the user. 
        public ActionResult Events()
        {

            if (SessionManager.LoggedIn == false)
            {
                return RedirectToAction("Index", "Home");
            }

            EventsViewModels eventsModel = new EventsViewModels();
            EventManager eventManager = new EventManager();
            
            eventsModel = eventManager.GetAllEvents();

            return View(eventsModel);
        }

        // When the my events page is loaded all the events which the user is the owner is loaded
        public ActionResult MyEvents()
        {

            if (SessionManager.LoggedIn == false)
            {
                return RedirectToAction("Index", "Home");
            }

            EventsViewModels eventsModel = new EventsViewModels();
            EventManager eventManager = new EventManager();

            eventsModel = eventManager.GetEventsCreatedByUser(SessionManager.SessionID);

            return View(eventsModel);
        }

        // Loads the events that the logged in user is attending. 
        public ActionResult AttendingEvents()
        {

            if (SessionManager.LoggedIn == false)
            {
                return RedirectToAction("Index", "Home");
            }

            EventsViewModels eventsModel = new EventsViewModels();
            EventManager eventManager = new EventManager();

            eventsModel = eventManager.GetEventsAttendingByUserID(SessionManager.SessionID);

            return View(eventsModel);
        }
    }
}
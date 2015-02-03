using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Container_Classes;

namespace Capstone.Models
{
    // Used for returning a series of Events opposed to one 
    public class EventsViewModels
    {

        public EventsViewModels()
        {

        }

        // Object for holding several events between the view and the database.
        // Used for AttendingEvents, ModeratingEvents, EventsCreatedByUser
        public List<Event> Events { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Container_Classes;

namespace Capstone.Models
{
    public class EventsViewModels
    {
        // Used to grab multiple events at one time for the view
        // Used by GetEventsAttendingByUserID, GetEventsCreatedByUser
        public List<Event> Events { get; set; }
    }
}
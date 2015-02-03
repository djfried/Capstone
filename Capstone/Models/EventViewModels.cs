using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Container_Classes;

namespace Capstone.Models
{
    public class EventViewModels
    {
        // A single object to pass to the view to be displayed.
        // Used in CreateEvent, UpdateEvent, CancelEvent
        public Event Event { get; set; }
    }
}
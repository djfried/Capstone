using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Container_Classes
{
    public class Event
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Category> Categories { get; set; }
        public List<Type> Types { get; set; }
        public string Description { get; set; }
        public User Owner { get; set; }
        public string Logo_Path { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }

        // Converts a Data.Event from database storage to container object
        public static Event DataEventToEvent(Data.Event dEvent, User owner, List<Category> categories, List<Type> types)
        {
            Event newEvent = new Event();

            newEvent.Categories = categories;
            newEvent.Description = dEvent.Description;
            newEvent.EndDate = dEvent.EndDate;
            newEvent.Location = dEvent.Location;
            newEvent.Owner = owner;
            newEvent.Logo_Path = dEvent.Logo_Path;
            newEvent.StartDate = dEvent.StartDate;
            newEvent.Status = dEvent.Status;
            newEvent.Title = dEvent.Title;
            newEvent.Types = types;
            
            return newEvent;
        }

        // Converts a Container Object to a Data.Event
        public static Data.Event EventToDataEvent(Event oEvent)
        {
            Data.Event dEvent = new Data.Event();

            dEvent.Description = oEvent.Description;
            dEvent.EndDate = oEvent.EndDate;
            dEvent.Location = oEvent.Location;
            dEvent.Logo_Path = oEvent.Logo_Path;
            dEvent.StartDate = oEvent.StartDate;
            dEvent.Status = oEvent.Status;
            dEvent.Title = oEvent.Title;

            return dEvent;
        }
    }
}
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
        public string Category { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Owner_ID { get; set; }
        public string Logo_Path { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }

        // Converts a Data.Event from database storage to container object
        public static Event DataEventToContainerEvent(Data.Event dataEvent, int owner_ID, string category, string type)
        {
            Event containerEvent = new Event();

            containerEvent.ID = dataEvent.EventID;
            containerEvent.Category = category;
            containerEvent.Description = dataEvent.Description;
            containerEvent.EndDate = dataEvent.EndDate;
            containerEvent.Location = dataEvent.Location;
            containerEvent.Owner_ID = owner_ID;
            containerEvent.Logo_Path = dataEvent.Logo_Path;
            containerEvent.StartDate = dataEvent.StartDate;
            containerEvent.Status = dataEvent.Status;
            containerEvent.Title = dataEvent.Title;
            containerEvent.Type = type;
            
            return containerEvent;
        }

        // Converts a Container Object to a Data.Event
        public static Data.Event ContainerEventToDataEvent(Event containerEvent, int category_ID, int type_ID, int owner_ID)
        {
            Data.Event dataEvent = new Data.Event();

            dataEvent.EventID = containerEvent.ID;
            dataEvent.Category_ID = category_ID;
            dataEvent.Description = containerEvent.Description;
            dataEvent.EndDate = containerEvent.EndDate;
            dataEvent.Location = containerEvent.Location;
            dataEvent.Logo_Path = containerEvent.Logo_Path;
            dataEvent.Owner_ID = owner_ID;
            dataEvent.StartDate = containerEvent.StartDate;
            dataEvent.Status = containerEvent.Status;
            dataEvent.Title = containerEvent.Title;
            dataEvent.Type_ID = type_ID;

            return dataEvent;
        }

        public static List<Data.Event> DatabaseToDataEvent(IEnumerable<Data.Event> source)
        {
            List<Data.Event> dataEvents = new List<Data.Event>();

            foreach (Data.Event dataEvent in source)
            {
                dataEvents.Add(dataEvent);
            }

            return dataEvents;
        }
    }
}
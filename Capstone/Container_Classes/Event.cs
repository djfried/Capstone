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
        public static Event DataEventToContainerEvent(Data.Event dataEvent, User owner, List<Category> categories, List<Type> types)
        {
            Event containerEvent = new Event();

            containerEvent.ID = dataEvent.Id;
            containerEvent.Categories = categories;
            containerEvent.Description = dataEvent.Description;
            containerEvent.EndDate = dataEvent.EndDate;
            containerEvent.Location = dataEvent.Location;
            containerEvent.Owner = owner;
            containerEvent.Logo_Path = dataEvent.Logo_Path;
            containerEvent.StartDate = dataEvent.StartDate;
            containerEvent.Status = dataEvent.Status;
            containerEvent.Title = dataEvent.Title;
            containerEvent.Types = types;
            
            return containerEvent;
        }

        public static Event DataEventToContainerEvent(Data.Event dataEvent)
        {
            Event containerEvent = new Event();

            containerEvent.ID = dataEvent.Id;
            containerEvent.Description = dataEvent.Description;
            containerEvent.EndDate = dataEvent.EndDate;
            containerEvent.Location = dataEvent.Location;
            containerEvent.Logo_Path = dataEvent.Logo_Path;
            containerEvent.StartDate = dataEvent.StartDate;
            containerEvent.Status = dataEvent.Status;
            containerEvent.Title = dataEvent.Title;

            return containerEvent;
        }

        // Converts a Container Object to a Data.Event
        public static Data.Event ContainerEventToDataEvent(Event containerEvent)
        {
            Data.Event dataEvent = new Data.Event();

            dataEvent.Id = containerEvent.ID;
            dataEvent.Description = containerEvent.Description;
            dataEvent.EndDate = containerEvent.EndDate;
            dataEvent.Location = containerEvent.Location;
            dataEvent.Logo_Path = containerEvent.Logo_Path;
            dataEvent.StartDate = containerEvent.StartDate;
            dataEvent.Status = containerEvent.Status;
            dataEvent.Title = containerEvent.Title;

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
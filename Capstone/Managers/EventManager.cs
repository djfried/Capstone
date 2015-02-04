using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Container_Classes;
using Capstone.Core;
using Capstone.Models;
using Capstone.Data;


//return _Map(_repository.GetAll<Data.Resource>());
//return _Map(_repository.GetAll<Data.Resource>(resource => resource.ResourceType == typeId));
//return _Map(_repository.Get<Data.Resource>(resource => resource.ResourceId == resourceId));

namespace Capstone.Managers
{
    public class EventManager
    {
        // Entities will be the connection to the database. 
        private static CapstoneEntities _entities = new CapstoneEntities();
        private IRepository _repository = new Repository(_entities);

        public EventViewModels CreateEvent(Container_Classes.Event NewEvent)
        {
            // Convert the type string and category string to database keys
            Data.Type dataType = _repository.Get<Data.Type>(x => x.Type1.Equals(NewEvent.Type));
            if(dataType == null){
                // Could not map type to the database
                return null;
            }

            Data.Category dataCategory = _repository.Get<Data.Category>(x => x.Category1.Equals(NewEvent.Category));
            if(dataCategory == null){
                // Could not mape category string to the database.
                return null;
            }

            // Convert the Container Event into a Data Event so it can be added to the database
            Data.Event dataEvent = Container_Classes.Event.ContainerEventToDataEvent(NewEvent, NewEvent.Owner_ID, dataType.TypeID, dataCategory.CategoryID);
            _repository.Add<Data.Event>(dataEvent);

            // Weak way of getting new event, this should break if two events have the same name!
            dataEvent = _repository.Get<Data.Event>(x => x.Title == NewEvent.Title);
            
            // Assign the ID to the added container object event
            NewEvent.ID = dataEvent.EventID;

            _repository.SaveChanges();

            EventViewModels model = new EventViewModels();
            model.Event = NewEvent;

            return model;
        }

        public EventViewModels UpdateEvent(Container_Classes.Event UpdatedEvent)
        {
            // Retreieve the existing event from the data to update it.
            Data.Event dataEvent = _repository.Get<Data.Event>(x => x.EventID == UpdatedEvent.ID);
            if (dataEvent == null)
            {
                // The event does not exist in the database.
                return EventNotFound();
            }

            // Convert the type string and category string to database keys
            Data.Type dataType = _repository.Get<Data.Type>(x => x.Type1.Equals(UpdatedEvent.Type));
            if (dataType == null)
            {
                // Could not map type to the database
                return null;
            }

            Data.Category dataCategory = _repository.Get<Data.Category>(x => x.Category1.Equals(UpdatedEvent.Category));
            if (dataCategory == null)
            {
                // Could not mape category string to the database.
                return null;
            }

            // The event exists and we have it stored as data event. Let's update it.
            // First the event table will be updated
            dataEvent = Container_Classes.Event.ContainerEventToDataEvent(UpdatedEvent, UpdatedEvent.Owner_ID, dataType.TypeID, dataCategory.CategoryID);
            _repository.Update<Data.Event>(dataEvent);

            _repository.SaveChanges();

            EventViewModels model = new EventViewModels();
            model.Event = UpdatedEvent;

            return model;
        }

        public EventViewModels CancelEvent(Container_Classes.Event containerEvent)
        {
            Data.Event dataEvent = _repository.Get<Data.Event>(x => x.EventID == containerEvent.ID);
            if (dataEvent == null)
            {
                // We did not find the event to cancel through the id
                return EventNotFound();
            }

            // Change the status to cancelled :(
            dataEvent.Status = "Cancelled";
            // Push the update back to the database
            _repository.Update<Data.Event>(dataEvent);
            // Make sure we save our work!
            _repository.SaveChanges();

            // Returned the updated event back to the view

            EventViewModels model = new EventViewModels();
            model.Event = containerEvent;

            return model;
        }

        public EventsViewModels GetAllEvents()
        {
            // Grab all events that the user would be able to sign up for.
            List<Data.Event> dataEvents = Container_Classes.Event.DatabaseToDataEvent(_repository.GetAll<Data.Event>(x => x.Status.Equals("ACTIVE", StringComparison.Ordinal)));
            List<Container_Classes.Event> containerEvents = new List<Container_Classes.Event>();
            Data.Type dataType;
            Data.Category dataCategory;

            // Put the events into the container classes.
            foreach (Data.Event dataEvent in dataEvents)
            {
                dataType = _repository.Get<Data.Type>(x => x.TypeID == dataEvent.Type_ID);
                dataCategory = _repository.Get<Data.Category>(x => x.CategoryID == dataEvent.Category_ID);
                containerEvents.Add(Container_Classes.Event.DataEventToContainerEvent(dataEvent, dataEvent.Owner_ID, dataCategory.Category1, dataType.Type1));
            }

            EventsViewModels model = new EventsViewModels();
            model.Events = containerEvents;

            return model;

        }

        public EventsViewModels GetEventsAttendingByUserID(Container_Classes.User containerUser)
        {
            // Get the registrations table for the events - but only the ones that pertain to our user
            List<Data.Registration> dataRegistrations = DatabaseToDataRegistration(_repository.GetAll<Data.Registration>(x => x.User_ID == containerUser.ID));
            // Extract the events from the remaining entries 
            List<Data.Event> dataEvents = new List<Data.Event>();

            foreach (Data.Registration dataRegistration in dataRegistrations)
            {
                dataEvents.Add(_repository.Get<Data.Event>(x => x.EventID == dataRegistration.Event_ID));
            }

            List<Container_Classes.Event> containerEvents = new List<Container_Classes.Event>();
            Data.Type dataType;
            Data.Category dataCategory;
            
            foreach (Data.Event dataEvent in dataEvents)
            {
                dataType = _repository.Get<Data.Type>(x => x.TypeID == dataEvent.Type_ID);
                dataCategory = _repository.Get<Data.Category>(x => x.CategoryID == dataEvent.Category_ID);
                containerEvents.Add(Container_Classes.Event.DataEventToContainerEvent(dataEvent, dataEvent.Owner_ID, dataCategory.Category1, dataType.Type1));
            }

            EventsViewModels model = new EventsViewModels();
            model.Events = containerEvents;

            return model;
        }

        public EventsViewModels GetEventsCreatedByUser(Container_Classes.User containerUser)
        {
            // Find the events that are being moderated by this user's ID
            List<Data.Event> dataEvents = Container_Classes.Event.DatabaseToDataEvent(_repository.GetAll<Data.Event>(x => x.Owner_ID == containerUser.ID));

            // Transform these events into a list of container objects
            List<Container_Classes.Event> containerEvents = new List<Container_Classes.Event>();
            Data.Type dataType;
            Data.Category dataCategory;

            foreach (Data.Event dataEvent in dataEvents)
            {
                dataType = _repository.Get<Data.Type>(x => x.TypeID == dataEvent.Type_ID);
                dataCategory = _repository.Get<Data.Category>(x => x.CategoryID == dataEvent.Category_ID);
                containerEvents.Add(Container_Classes.Event.DataEventToContainerEvent(dataEvent, dataEvent.Owner_ID, dataCategory.Category1, dataType.Type1));
            }

            EventsViewModels model = new EventsViewModels();
            model.Events = containerEvents;

            return model;
        }

        public EventViewModels EventNotFound()
        {
            // This could break if we have multiple events with the same name
            Data.Event dataEvent =_repository.Get<Data.Event>(x => x.Title.Equals("NOTFOUND", StringComparison.Ordinal));
            if (dataEvent == null)
            {
                // We can't even find the fake event!
                return null;
            }
            Container_Classes.Event containerEvent = Container_Classes.Event.DataEventToContainerEvent(dataEvent, 0, "NOTFOUND", "NOTFOUND");

            EventViewModels model = new EventViewModels();
            model.Event = containerEvent;

            return model;
        }

        public List<Data.Registration> DatabaseToDataRegistration(IEnumerable<Data.Registration> source)
        {
            List<Data.Registration> dataRegistrations = new List<Data.Registration>();

            foreach (Data.Registration dataRegistration in source)
            {
                dataRegistrations.Add(dataRegistration);
            }

            return dataRegistrations;
        }
    }
}
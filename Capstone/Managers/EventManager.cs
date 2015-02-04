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
        private static Entities2 _entities = new Entities2();
        private IRepository _repository = new Repository(_entities);

        public EventViewModels CreateEvent(Container_Classes.Event NewEvent)
        {
            // Convert the Container Event into a Data Event so it can be added to the database
            Data.Event dataEvent = Container_Classes.Event.ContainerEventToDataEvent(NewEvent);
            _repository.Add<Data.Event>(dataEvent);

            // Weak way of getting new event, this should break if two events have the same name!
            dataEvent = _repository.Get<Data.Event>(x => x.Title == NewEvent.Title);
            
            // Assign the ID to the added container object event
            NewEvent.ID = dataEvent.Id;

            // Now we must add the types and categories to the database.
            // First the categories will be inserted to the database
            List<Data.Category> dataCategories = Container_Classes.Category.ContainerCategoriesToDataCategories(NewEvent.Categories);
            dataCategories = Container_Classes.Category.AddEventIDToDataCategories(dataCategories, dataEvent.Id);

            // Insert all of the categories into the database
            foreach (Data.Category dataCategory in dataCategories)
            {
                _repository.Add<Data.Category>(dataCategory);
            }

            // Second the types will be inserted to the database.
            List<Data.Type> dataTypes = Container_Classes.Type.ContainerTypeToDataType(NewEvent.Types);
            dataTypes = Container_Classes.Type.AddEventIDToDataTypes(dataTypes, dataEvent.Id);

            // Insert all of the types into the database
            foreach (Data.Type dataType in dataTypes)
            {
                _repository.Add<Data.Type>(dataType);
            }

            _repository.SaveChanges();

            EventViewModels model = new EventViewModels();
            model.Event = NewEvent;

            return model;
        }

        public EventViewModels UpdateEvent(Container_Classes.Event UpdatedEvent)
        {
            // Retreieve the existing event from the data to update it.
            Data.Event dataEvent = _repository.Get<Data.Event>(x => x.Id == UpdatedEvent.ID);
            if (dataEvent == null)
            {
                // The event does not exist in the database.
                return null;
            }

            // The event exists and we have it stored as data event. Let's update it.
            // First the event table will be updated
            dataEvent = Container_Classes.Event.ContainerEventToDataEvent(UpdatedEvent);
            _repository.Update<Data.Event>(dataEvent);


            // Second the categories will be deleted and updated
            // first we delete
            List<Data.Category> dataCategories = Container_Classes.Category.DatabaseToDataCategories(_repository.GetAll<Data.Category>(x => x.Event_ID == UpdatedEvent.ID));
            foreach (Data.Category dataCategory in dataCategories)
            {
                _repository.Delete<Data.Category>(dataCategory);
            }

            // and then we update (or add in this case)
            dataCategories = Container_Classes.Category.ContainerCategoriesToDataCategories(UpdatedEvent.Categories);
            dataCategories = Container_Classes.Category.AddEventIDToDataCategories(dataCategories, UpdatedEvent.ID);

            foreach (Data.Category dataCategory in dataCategories)
            {
                _repository.Add<Data.Category>(dataCategory);
            }


            // Third the types will be deleted and updated
            // first we delete the old
            List<Data.Type> dataTypes = Container_Classes.Type.DatabaseToDataTypes(_repository.GetAll<Data.Type>(x => x.Event_ID == UpdatedEvent.ID));
            foreach (Data.Type dataType in dataTypes)
            {
                _repository.Delete<Data.Type>(dataType);
            }

            // and then we update (or add in this case)
            dataTypes = Container_Classes.Type.ContainerTypeToDataType(UpdatedEvent.Types);
            dataTypes = Container_Classes.Type.AddEventIDToDataTypes(dataTypes, UpdatedEvent.ID);

            foreach (Data.Type dataType in dataTypes)
            {
                _repository.Add<Data.Type>(dataType);
            }

            _repository.SaveChanges();

            EventViewModels model = new EventViewModels();
            model.Event = UpdatedEvent;

            return model;
        }

        public EventViewModels CancelEvent(Container_Classes.Event containerEvent)
        {
            Data.Event dataEvent = _repository.Get<Data.Event>(x => x.Id == containerEvent.ID);
            if (dataEvent == null)
            {
                // We did not find the event to cancel through the id
                return null;
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

        public EventsViewModels GetEventsAttendingByUserID(Container_Classes.User containerUser)
        {
            // Get the registrations table for the events - but only the ones that pertain to our user
            List<Data.Registration> dataRegistrations = DatabaseToDataRegistration(_repository.GetAll<Data.Registration>(x => x.User_ID == containerUser.ID));
            // Extract the events from the remaining entries 
            List<Data.Event> dataEvents = new List<Data.Event>();

            foreach (Data.Registration dataRegistration in dataRegistrations)
            {
                dataEvents.Add(_repository.Get<Data.Event>(x => x.Id == dataRegistration.Event_ID));
            }

            List<Container_Classes.Event> containerEvents = new List<Container_Classes.Event>();
            foreach (Data.Event dataEvent in dataEvents)
            {
                containerEvents.Add(Container_Classes.Event.DataEventToContainerEvent(dataEvent));
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

            foreach (Data.Event dataEvent in dataEvents)
            {
                containerEvents.Add(Container_Classes.Event.DataEventToContainerEvent(dataEvent));
            }

            EventsViewModels model = new EventsViewModels();
            model.Events = containerEvents;

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
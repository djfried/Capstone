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
            Data.Event dataEvent = Container_Classes.Event.EventToDataEvent(NewEvent);
            _repository.Add<Data.Event>(dataEvent);

            // Weak way of getting new event, this should break if two events have the same name!
            dataEvent = _repository.Get<Data.Event>(x => x.Title == NewEvent.Title);

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
            if (_repository.Get<Container_Classes.Event>(x => x.ID == UpdatedEvent.ID) == null)
            {
                // The event does not exist to update!
                return null;
            }

            // Update the event 
            _repository.Update<Container_Classes.Event>(UpdatedEvent);
            Container_Classes.Event newEvent = _repository.Get<Container_Classes.Event>(x => x.ID == UpdatedEvent.ID);

            _repository.SaveChanges();

            EventViewModels model = new EventViewModels();
            model.Event = newEvent;

            return model;
        }

        public EventViewModels CancelEvent(int EventID)
        {
            Container_Classes.Event fetchEvent = _repository.Get<Container_Classes.Event>(x => x.ID == EventID);
            if (fetchEvent == null)
            {
                // Event does not exist!
                return null;
            }

            fetchEvent.Status = "Cancelled";
            // Send the object back to the database to be updated
            _repository.Update<Container_Classes.Event>(fetchEvent);
            // Pull the event back down to ensure the data was correctly updated
            fetchEvent = _repository.Get<Container_Classes.Event>(x => x.ID == fetchEvent.ID);
            if (!fetchEvent.Status.Equals("Cancelled", StringComparison.Ordinal))
            {
                // The event was not correctly cancelled!
                return null;
            }

            _repository.SaveChanges();

            EventViewModels model = new EventViewModels();
            model.Event = fetchEvent;

            return model;
        }

        public EventsViewModels GetEventsAttendingByUserID(int UserID)
        {
            //List<Event> attendingEvents = new List<Event>();
            //List<Data.Registration


            return null;
        }

        public EventsViewModels GetEventsCreatedByUser(int UserID)
        {

            return null;
        }

    }
}
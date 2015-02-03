using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Container_Classes;
using Capstone.Core;
using Capstone.Models;


//return _Map(_repository.GetAll<Data.Resource>());
//return _Map(_repository.GetAll<Data.Resource>(resource => resource.ResourceType == typeId));
//return _Map(_repository.Get<Data.Resource>(resource => resource.ResourceId == resourceId));

namespace Capstone.Managers
{
    public class EventManager
    {
        // Entities will be the connection to the database. 
        private static Entities _entities = new Entities();
        private IRepository _repository = new Repository(_entities);

        public EventViewModels CreateEvent(Event NewEvent)
        {

            return null;
        }

        public Event UpdateEvent(Event UpdatedEvent)
        {

            return null;
        }

        public Event CancelEvent(int EventID)
        {

            return null;
        }

        public List<Event> GetEventsAttendingByUserID(int UserID)
        {

            return null;
        }

        public List<Event> GetEventsCreatedByUser(int UserID)
        {

            return null;
        }

    }
}
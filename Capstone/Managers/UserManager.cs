using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Container_Classes;
using Capstone.Data;
using Capstone.Core;
using Capstone.Models;

//return _Map(_repository.GetAll<Data.Resource>());
//return _Map(_repository.GetAll<Data.Resource>(resource => resource.ResourceType == typeId));
//return _Map(_repository.Get<Data.Resource>(resource => resource.ResourceId == resourceId));

namespace Capstone.Managers
{
    public class UserManager
    {

        private static CapstoneEntities _entities = new CapstoneEntities();
        private IRepository _repository = new Repository(_entities);

        // Once the controller has passed the User info and foods list it can be inserted into the database
        // and a view of that inserted user is returned. 
        public UserViewModels CreateUser(Container_Classes.User NewUser)
        {
            // Ensure that the User does not already exist in the database.
            if (_repository.Get<Data.User>(x => x.Username == NewUser.Username) == null)
            {
                // The user is in the database, this should be a special error. 
                return null;
            }

            // Find out the food id that the user provided and get the ID number for the database entry.
            Data.Food userFood = _repository.Get<Data.Food>(x => x.Food1.Equals(NewUser.Foods));
            if (userFood == null)
            {
                // Food requested was not in the database!
                return null;
            }

            // Add the new user to the database.
            Data.User dUser = Container_Classes.User.UserToDataUser(NewUser, userFood.FoodID);
            _repository.Add<Data.User>(dUser);

            // Get the inserted NewUser back from the database
            dUser =_repository.Get<Data.User>(x => x.Username == NewUser.Username);
            if (dUser == null)
            {
                // The user was not uploaded into the database.
                return UserNotFound();
            }

            _repository.SaveChanges();

            // Create the model to return the information to the view.
            UserViewModels model = new UserViewModels();
            model.User = NewUser;

            return model;
        }

        public UserViewModels UpdateUser(Container_Classes.User UpdatedUser)
        {
            //Attempt to find the user from the database before sending update.
            if (_repository.Get<Container_Classes.User>(x => x.Username == UpdatedUser.Username) == null)
            {
                // The user was not found in the database
                return UserNotFound();
            }

            // Get the food ID from the database to store in the user object
            Data.Food dataFood = _repository.Get<Data.Food>(x => x.Food1.Equals(UpdatedUser.Foods));
            if (dataFood == null)
            {
                // The food ID wasn't found
                return null;
            }

            Data.User dUser = Container_Classes.User.UserToDataUser(UpdatedUser, dataFood.FoodID);
            _repository.Update<Data.User>(dUser);

            Data.User addedDataUser = _repository.Get<Data.User>(x => x.Username == UpdatedUser.Username);
            if (addedDataUser == null)
            {
                // The user should have been inserted into the database, but they were not. (Or failed during insertion)
                return UserNotFound();
            }

            _repository.SaveChanges();

            UpdatedUser = Container_Classes.User.DataUserToUser(addedDataUser, UpdatedUser.Foods);

            // Create the model to return the information to the view.
            UserViewModels model = new UserViewModels();
            model.User = UpdatedUser;

            return model;
        }

        public UserViewModels GetUserByID(int UserID)
        {
            // Attempt to find the user through the requested username.
            Data.User dUser = _repository.Get<Data.User>(x => x.UserID == UserID);

            // Ensure that we actually found someone through the username.
            if (dUser == null)
            {
                // We did not find anyone by the provided username in the database.
                return UserNotFound();
            }

            // Get the User's food preference from the database
            Data.Food dataFood = _repository.Get<Data.Food>(x => x.FoodID == dUser.Food_ID);

            // Ensure that we found a valid food preference
            if (dataFood == null)
            {
                // The food wasn't found
                return null;
            }

            // Combine the foods and user information for the database for the user object
            Container_Classes.User containerUser = Container_Classes.User.DataUserToUser(dUser, dataFood.Food1);

            // Create the model to return the information to the view.
            UserViewModels model = new UserViewModels();
            model.User = containerUser;

            return model;
        }

        // This cannot be implemented until the Events have been added to the database entities.
        public UsersViewModels GetUsersByEventID(int EventID)
        {
            // Find the list of users attending the event
            List<Data.Registration> dataRegistrations = DatabaseToDataRegistration(_repository.GetAll<Data.Registration>(x => x.Event_ID == EventID));
            
            // Create the List of Data.User id's attending the event
            List<int> dataUsersID = new List<int>();

            foreach (Data.Registration dataRegistration in dataRegistrations)
            {
                dataUsersID.Add(dataRegistration.User_ID);
            }

            // Retieve these users from the database and read into List of Data.User
            List<Data.User> dataUsers = new List<Data.User>();

            foreach (int userID in dataUsersID)
            {
                dataUsers.Add(_repository.Get<Data.User>(x => x.UserID == userID));
            }

            // Convert the data.users to usable user objects
            List<Container_Classes.User> containerUsers = new List<Container_Classes.User>();
            // Have an data object to fetch the User's food preference
            Data.Food dataFood;
            foreach (Data.User dataUser in dataUsers)
            {
                dataFood = _repository.Get<Data.Food>(x => x.FoodID == dataUser.Food_ID);
                containerUsers.Add(Container_Classes.User.DataUserToUser(dataUser, dataFood.Food1));
            }

            UsersViewModels model = new UsersViewModels();
            model.Users = containerUsers;

            return model;
        }

        // Returns the view of all of the events the user is registered for. 
        public EventsViewModels RegisterUserForEvent(Container_Classes.Event containerEvent, Container_Classes.User containerUser)
        {
            // Get the food ID from the database using the containerUser object
            Data.Food dataFood = _repository.Get<Data.Food>(x => x.Food1.Equals(containerUser.Foods));
            // Ensure that we found the food in the database
            if (dataFood == null)
            {
                return null;
            }

            // Ensure that the user exists in the database
            Data.User dataUser = Container_Classes.User.UserToDataUser(containerUser, dataFood.FoodID);
            dataUser = _repository.Get<Data.User>(x => x.UserID == dataUser.UserID);
            
            if (dataUser == null)
            {
                // The user was not found in the database
                return null;
            }

            // Grab the Category and type from the database so we can read an event from the database
            Data.Category dataCategory = _repository.Get<Data.Category>(x => x.Category1.Equals(containerEvent.Category));
            if (dataCategory == null)
            {
                // We could not find the category in the database
                return null;
            }

            Data.Type dataType = _repository.Get<Data.Type>(x => x.Type1.Equals(containerEvent.Type));
            if (dataType == null)
            {
                // We could not find the type in the database
                return null;
            }

            // Ensure that the event exists in the database
            Data.Event dataEvent = Container_Classes.Event.ContainerEventToDataEvent(containerEvent, dataCategory.CategoryID, dataType.TypeID, dataUser.UserID);
            dataEvent = _repository.Get<Data.Event>(x => x.EventID== dataEvent.EventID);

            if (dataEvent == null)
            {
                // The user was not found in the database
                return null;
            }

            // Prepare the information to be updated in the database
            Data.Registration registration = new Data.Registration();
            registration.User_ID = dataUser.UserID;
            registration.Event_ID = dataEvent.EventID;

            // Add the registration to the database
            _repository.Add<Data.Registration>(registration);

            _repository.SaveChanges();

            // Get all of the users registered events
            List<Data.Registration> registrations = DatabaseToDataRegistration(_repository.GetAll<Data.Registration>(x => x.User_ID == dataUser.UserID));
            List<Data.Event> dataEvents = new List<Data.Event>();

            // Create a list of all events this user is attending
            foreach(Data.Registration dataRegistration in registrations)
            {
                dataEvents.Add(_repository.Get<Data.Event>(x => x.EventID == dataRegistration.Event_ID));
            }

            // Conver the list of dataEvents to containerEvents
            List<Container_Classes.Event> containerEvents = new List<Container_Classes.Event>();
            foreach(Data.Event dataEvent2 in dataEvents)
            {
                dataType = _repository.Get<Data.Type>(x => x.TypeID == dataEvent2.Type_ID);
                dataCategory = _repository.Get<Data.Category>(x => x.CategoryID == dataEvent2.Category_ID);
                containerEvents.Add(Container_Classes.Event.DataEventToContainerEvent(dataEvent2, dataUser.UserID, dataCategory.Category1, dataType.Type1));
            }

            EventsViewModels model = new EventsViewModels();
            model.Events = containerEvents;

            return model;
        }

        public UserViewModels UserNotFound()
        {
            Data.User dataUser = _repository.Get<Data.User>(x => x.Username.Equals("NOTFOUND", StringComparison.Ordinal));
            if (dataUser == null)
            {
                // We cannot even find the fake user!
                return null;
            }
            // Convert our fake user to something the view can use
            Container_Classes.User containerUser = Container_Classes.User.DataUserToUser(dataUser, "NOTFOUND");

            UserViewModels model = new UserViewModels();
            model.User = containerUser;
            return model;
        }

        public List<Data.Registration> DatabaseToDataRegistration(IEnumerable<Data.Registration> source)
        {
            List<Data.Registration> dataRegistrations = new List<Data.Registration>();

            foreach (Data.Registration dbRegistration in source)
            {
                dataRegistrations.Add(dbRegistration);
            }

            return dataRegistrations;
        }

        
    }
}
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

        private static Entities _entities = new Entities();
        private IRepository _repository = new Repository(_entities);

        public UserViewModels CreateUser(Container_Classes.User NewUser)
        {
            // Ensure that the User does not already exist in the database.
            if (_repository.Get<Container_Classes.User>(x => x.Username == NewUser.Username) == null)
            {
                // The user is in the database, this should be a special error. 
                return null;
            }
            // Add the new user to the database.
            _repository.Add(NewUser);

            // Get the inserted NewUser back from the database
            Container_Classes.User addedUser = _repository.Get<Container_Classes.User>(x => x.Username == NewUser.Username);
            if (addedUser == null)
            {
                // The user should have been inserted into the database, but they were not. (Or failed during insertion)
                return null;
            }

            // Create the model to return the information to the view.
            UserViewModels model = new UserViewModels();
            model.User = addedUser;

            return model;
        }

        public UserViewModels UpdateUser(Container_Classes.User UpdatedUser)
        {
            //Attempt to find the user from the database before sending update.
            if (_repository.Get<Container_Classes.User>(x => x.Username == UpdatedUser.Username) == null)
            {
                // The user was not found in the database
            }

            // I am unsure if this is working correctly, how does it know if the ID is correct.
            // Perhaps we should use the Session ID to specifiy.
            _repository.Update<Container_Classes.User>(UpdatedUser);


            Container_Classes.User addedUser = _repository.Get<Container_Classes.User>(x => x.Username == UpdatedUser.Username);
            if (addedUser == null)
            {
                // The user should have been inserted into the database, but they were not. (Or failed during insertion)
                return null;
            }

            // Create the model to return the information to the view.
            UserViewModels model = new UserViewModels();
            model.User = addedUser;

            return model;
        }

        public UserViewModels GetUserByUsername(String Username)
        {
            // Attempt to find the user through the requested username.
            Container_Classes.User fetchedUser = _repository.Get<Container_Classes.User>(x => x.Username == Username);
            // Ensure that we actually found someone through the username.
            if (fetchedUser == null)
            {
                // We did not find anyone by the provided username in the database.
                return null;
            }

            // Create the model to return the information to the view.
            UserViewModels model = new UserViewModels();
            model.User = fetchedUser;

            return model;
        }

        // This cannot be implemented until the Events have been added to the database entities.
        public UsersViewModels GetUsersByEventID(int EventID)
        {

            return null;
        }
    }
}
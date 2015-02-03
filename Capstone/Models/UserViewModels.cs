using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Container_Classes;

namespace Capstone.Models
{

    // Interacts with the Event Manager to encapsulate the between the Database and the View. 
    public class UserViewModels
    {

        // Used to have an object that contains a single user to pass between view and model
        // Used for CreateUser, UpdateUser, GetUser
        public User User { get; set; }

    }
}
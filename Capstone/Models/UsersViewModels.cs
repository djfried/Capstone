using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Container_Classes;

namespace Capstone.Models
{
    // Used for returning a list of users opposed to a single user.
    public class UsersViewModels
    {
        // Used to have a list of objects that pass all users between a view and model
        // Used for UsersByEvent
        public List<User> Users { get; set; }
    }
}
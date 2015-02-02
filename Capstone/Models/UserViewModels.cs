using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Models
{

    public class User
    {
        string Username;
        string Password;
        string FirstName;
        string LastName;
        string Address1;
        string Address2;
        string City;
        string State;
        int Zip;
        string PhoneHome;
        string PhoneCell;
        string CompanyName;
        string BranchLocation;
        int Food_ID;
        string AdditionalInfo;
    }

    public class UserViewModels
    {
        public User GetMyProfile { get; set; }

        public User GetUserByID { get; set; }
    }
}
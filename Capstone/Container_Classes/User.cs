using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Container_Classes
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PhoneHome { get; set; }
        public string PhoneCell { get; set; }
        public string PhoneOffice { get; set; }
        public string CompanyName { get; set; }
        public string BranchLocation { get; set; }
        public List<Food> Foods { get; set; }
        public string AdditionalInfo { get; set; }

        // Converts a Database Data.User to a Container object
        public static User DataUserToUser(Data.User dUser, List<Food> foods){
            User user = new User();

            user.AdditionalInfo = dUser.AdditionalInfo;
            user.Address1 = dUser.Address1;
            user.Address2 = dUser.Address2;
            user.BranchLocation = dUser.BranchLocation;
            user.City = dUser.City;
            user.CompanyName = dUser.CompanyName;
            user.FirstName = dUser.FirstName;
            user.Foods = foods;
            user.LastName = dUser.LastName;
            user.Password = dUser.Password;
            user.PhoneCell = dUser.PhoneCell;
            user.PhoneHome = dUser.PhoneHome;
            user.PhoneOffice = dUser.PhoneOffice;
            user.State = dUser.State;
            user.Username = dUser.Username;
            user.Zip = dUser.Zip;

            return user;
        }

        // Used when we don't need the information of foods
        public static User DataUserToUser(Data.User dUser)
        {
            User user = new User();

            user.AdditionalInfo = dUser.AdditionalInfo;
            user.Address1 = dUser.Address1;
            user.Address2 = dUser.Address2;
            user.BranchLocation = dUser.BranchLocation;
            user.City = dUser.City;
            user.CompanyName = dUser.CompanyName;
            user.FirstName = dUser.FirstName;
            user.LastName = dUser.LastName;
            user.Password = dUser.Password;
            user.PhoneCell = dUser.PhoneCell;
            user.PhoneHome = dUser.PhoneHome;
            user.PhoneOffice = dUser.PhoneOffice;
            user.State = dUser.State;
            user.Username = dUser.Username;
            user.Zip = dUser.Zip;

            return user;
        }

        // Converts a Container User Object to a Data.User object
        public static Data.User UserToDataUser(User user)
        {
            Data.User dUser = new Data.User();

            dUser.AdditionalInfo = user.AdditionalInfo;
            dUser.Address1 = user.Address1;
            dUser.Address2 = dUser.Address2;
            dUser.BranchLocation = user.BranchLocation;
            dUser.City = user.City;
            dUser.CompanyName = user.CompanyName;
            dUser.FirstName = user.FirstName;
            dUser.LastName = user.LastName;
            dUser.Password = user.Password;
            dUser.PhoneCell = user.PhoneCell;
            dUser.PhoneHome = user.PhoneHome;
            dUser.PhoneOffice = user.PhoneOffice;
            dUser.State = user.State;
            dUser.Username = user.Username;
            dUser.Zip = user.Zip;

            return dUser;

        }
    }
}
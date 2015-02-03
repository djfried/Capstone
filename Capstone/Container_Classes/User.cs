using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Container_Classes
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PhoneHome { get; set; }
        public string PhoneCell { get; set; }
        public string PhoneOficce { get; set; }
        public string CompanyName { get; set; }
        public string BranchLocation { get; set; }
        List<string> Foods { get; set; }
        public string AdditionalInfo { get; set; }
        List<Event> OwnedEvents { get; set; }
        List<Event> AttendingEvents { get; set; }
    }
}
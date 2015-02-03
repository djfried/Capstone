using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Container_Classes
{
    public class Event
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Types { get; set; }
        public string Description { get; set; }
        public int OwnerID { get; set; }
        public string Logo_Path { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }

    }
}
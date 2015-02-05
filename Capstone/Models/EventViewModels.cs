using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Capstone.Container_Classes;


namespace Capstone.Models
{
    public class EventViewModels
    {
        // A single object to pass to the view to be displayed.
        // Used in CreateEvent, UpdateEvent, CancelEvent
        public Event Event { get; set; }

        
    }
    public class CreateEventViewModel
    {
       
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public string StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public string EndTime{ get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> TypeList { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Additional Info")]
        public string AdditionalInfo { get; set; }
        
        [FileExtensions(Extensions=".jpg",ErrorMessage="File must be .jpg")]
        [Display(Name = "Logo")]
        public string LogoPath { get; set; }



    }
}
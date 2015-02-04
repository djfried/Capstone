using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Container_Classes
{
    public class Category
    {
        public int ID { get; set; }
        public string CategoryString { get; set; }


        public static List<Data.Category> ContainerCategoriesToDataCategories(List<Container_Classes.Category> containerCategories)
        {
            List<Data.Category> dataCategories = new List<Data.Category>();
            Data.Category dataCategory;

            foreach (Container_Classes.Category containerCategory in containerCategories)
            {
                dataCategory = new Data.Category();
                dataCategory.Category1 = containerCategory.CategoryString;

                dataCategories.Add(dataCategory);
            }

            return dataCategories;
        }

        public static List<Container_Classes.Category> DataCategoriesToContainerCategories(List<Data.Category> dataCategories)
        {
            List<Container_Classes.Category> containerCategories = new List<Container_Classes.Category>();
            Container_Classes.Category containerCategory;

            foreach (Data.Category dataCategory in dataCategories)
            {
                containerCategory = new Container_Classes.Category();
                containerCategory.CategoryString = dataCategory.Category1;

                containerCategories.Add(containerCategory);
            }

            return containerCategories;
        }

        public static List<Data.Category> AddEventIDToDataCategories(List<Data.Category> dataCategories, int eventID)
        {
            foreach (Data.Category dataCategory in dataCategories)
            {
                dataCategory.Event_ID = eventID;
            }

            return dataCategories;
        }
    }
}
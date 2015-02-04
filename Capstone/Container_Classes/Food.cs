using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Container_Classes
{
    public class Food
    {
        public int ID { get; set; }
        public string FoodString { get; set; }

        public static List<Container_Classes.Food> DataFoodsToContainerFoods(List<Data.Food> source)
        {
            List<Container_Classes.Food> containerFoods = new List<Container_Classes.Food>();
            Container_Classes.Food containerFood;

            foreach (Data.Food dataFood in source)
            {
                containerFood = new Container_Classes.Food();
                containerFood.FoodString = dataFood.Food1;
                containerFood.ID = dataFood.Id;

                containerFoods.Add(containerFood);
            }

            return containerFoods;
        }

        public static List<Data.Food> ContainerFoodsToDataFoods(List<Container_Classes.Food> source)
        {
            List<Data.Food> dataFoods = new List<Data.Food>();
            Data.Food dataFood;

            foreach (Container_Classes.Food containerFood in source)
            {
                dataFood = new Data.Food();
                dataFood.Food1 = containerFood.FoodString;

                dataFoods.Add(dataFood);
            }

            return dataFoods;
        }

        public static List<Data.Food> DatabaseToDataFoods(IEnumerable<Data.Food> source)
        {
            List<Data.Food> dataFoods = new List<Data.Food>();

            foreach (Data.Food dbFood in source)
            {
                dataFoods.Add(dbFood);
            }

            return dataFoods;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Container_Classes
{
    public class Type
    {
        public int ID { get; set; }
        public string TypeString { get; set; }

        public static List<Container_Classes.Type> DataTypeToContainerType(List<Data.Type> dataTypes)
        {
            List<Container_Classes.Type> containerTypes = new List<Container_Classes.Type>();
            Container_Classes.Type containerType;

            foreach (Data.Type dataType in dataTypes)
            {
                containerType = new Container_Classes.Type();
                containerType.TypeString = dataType.Type1;

                containerTypes.Add(containerType);
            }

            return containerTypes;
        }

        public static List<Data.Type> ContainerTypeToDataType(List<Container_Classes.Type> containerTypes)
        {
            List<Data.Type> dataTypes = new List<Data.Type>();
            Data.Type dataType;

            foreach (Container_Classes.Type containerType in containerTypes)
            {
                dataType = new Data.Type();
                dataType.Type1 = containerType.TypeString;

                dataTypes.Add(dataType);
            }

            return dataTypes;
        }

        public static List<Data.Type> AddEventIDToDataTypes(List<Data.Type> dataTypes, int eventID)
        {
            foreach(Data.Type dataType in dataTypes){
                dataType.Event_ID = eventID;
            }

            return dataTypes;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Practica8
{
   public class Areas_Miguel
    {
        string Id;
        string Area;

        [JsonProperty(PropertyName = "id")]
        public string id
        {
            get { return Id; }
            set { Id = value; }
        }


        [JsonProperty(PropertyName = "area")]
        public string area
        {
            get { return Area; }
            set { Area = value; }
        }


    }
}

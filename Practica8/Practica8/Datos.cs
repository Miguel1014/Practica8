using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Practica8
{
    public class Datos
    {
        string user_id;

        [JsonProperty(PropertyName = "user_id")]
        public string User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }

    }
}

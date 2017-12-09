using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Practica8
{
    public class Tecnicos_Miguel
    {

        string Id;
        string Nombre;
        string Apellidos;
        string Especialidad;
        string Telefono;
        string Direccion;
        string Correo;
        string Usuario;
        string Password;


        [JsonProperty(PropertyName = "id")]
        public string id
        {
            get { return Id; }
            set { Id = value; }
        }


        [JsonProperty(PropertyName = "nombre")]
        public string nombre
        {
            get { return Nombre; }
            set { Nombre = value; }
        }

        [JsonProperty(PropertyName = "apellidos")]
        public string apellidos
        {
            get { return Apellidos; }
            set { Apellidos = value; }
        }

        [JsonProperty(PropertyName = "especialidad")]
        public string especialidad
        {
            get { return Especialidad; }
            set { Especialidad = value; }
        }

        [JsonProperty(PropertyName = "telefono")]
        public string telefono
        {
            get { return Telefono; }
            set { Telefono = value; }
        }

        [JsonProperty(PropertyName = "direccion")]
        public string direccion
        {
            get { return Direccion; }
            set { Direccion = value; }
        }

        [JsonProperty(PropertyName = "correo")]
        public string correo
        {
            get { return Correo; }
            set { Correo = value; }
        }

        [JsonProperty(PropertyName = "usuario")]
        public string usuario
        {
            get { return Usuario; }
            set { Usuario = value; }
        }

        [JsonProperty(PropertyName = "password")]
        public string password
        {
            get { return Password; }
            set { Password = value; }
        }


    }
}

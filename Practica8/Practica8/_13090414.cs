using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Practica8

{
    public class _13090414
    {
        string id;
        long matricula;
        string nombre;
        string apellidos;
        string direccion;
        long telefono;
        string carrera;
        string semestre;
        string correo;
        string github;
        bool deleted;

        [JsonProperty(PropertyName = "id")]
        public  string Id
        {
            get { return id; }
            set { id = value; }
        }


        [JsonProperty(PropertyName = "matricula")]
        public long Matricula
        {
            get { return matricula; }
            set { matricula = value; }
        }

        [JsonProperty(PropertyName = "Nombre")]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        [JsonProperty(PropertyName = "Apellidos")]
        public string Apellidos
        {
            get { return apellidos; }
            set { apellidos = value; }
        }

        [JsonProperty(PropertyName = "Direccion")]
        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        [JsonProperty(PropertyName = "Telefono")]
        public long Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        [JsonProperty(PropertyName = "Carrera")]
        public string Carrera
        {
            get { return carrera; }
            set { carrera = value; }
        }

        [JsonProperty(PropertyName = "Semestre")]
        public string Semestre
        {
            get { return semestre; }
            set { semestre = value; }
        }

        [JsonProperty(PropertyName = "Correo")]
        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        [JsonProperty(PropertyName = "Github")]
        public string Github
        {
            get { return github; }
            set { github = value; }
        }

        

        [JsonProperty(PropertyName = "Deleted")]
        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }

    }
}

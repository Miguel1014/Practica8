using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica8
{
    public class Solicitud_Miguel
    {
        

        string Id;
        string Problema;
        string Tecnico;
        DateTime Elaboracion;
        DateTime Fecha_limite;
        TimeSpan Hora_Limite;
        string Prioridad;
        string CArea;
        string Fecha_Resolucion;
        string Descripcion;
        string Status;
        string Comentarios;
        string Id_tecnico;
        string Reg;

        [JsonProperty(PropertyName = "id")]
        public string id
        {
            get { return Id; }
            set { Id = value; }
        }


        [JsonProperty(PropertyName = "Problema")]
        public string problema
        {
            get { return Problema; }
            set { Problema = value; }
        }

        [JsonProperty(PropertyName = "Tecnico")]
        public string tecnico
        {
            get { return Tecnico; }
            set { Tecnico = value; }
        }

        [JsonProperty(PropertyName = "Elaboracion")]
        public DateTime elaboracion
        {
            get { return Elaboracion; }
            set { Elaboracion = value; }
        }

        [JsonProperty(PropertyName = "Fecha_limite")]
        public DateTime fecha_limite
        {
            get { return Fecha_limite; }
            set { Fecha_limite = value; }
        }

        [JsonProperty(PropertyName = "Hora_limite")]
        public TimeSpan hora_limite
        {
            get { return Hora_Limite; }
            set { Hora_Limite = value; }
        }

        [JsonProperty(PropertyName = "Prioridad")]
        public string prioridad
        {
            get { return Prioridad; }
            set { Prioridad = value; }
        }

        [JsonProperty(PropertyName = "Area")]
        public string carea
        {
            get { return CArea; }
            set { CArea = value; }
        }

        [JsonProperty(PropertyName = "Fecha_Resolucion")]
        public string fecha_Resolucion
        {
            get { return Fecha_Resolucion; }
            set { Fecha_Resolucion = value; }
        }

        [JsonProperty(PropertyName = "Descripcion")]
        public string descripcion
        {
            get { return Descripcion; }
            set { Descripcion = value; }
        }
        [JsonProperty(PropertyName = "Status")]
        public string status
        {
            get { return Status; }
            set { Status = value; }
        }
        [JsonProperty(PropertyName = "Comentarios")]
        public string comentarios
        {
            get { return Comentarios; }
            set { Comentarios = value; }
        }
        [JsonProperty(PropertyName = "id_tecnico")]
        public string id_tecnico
        {
            get { return Id_tecnico; }
            set { Id_tecnico = value; }
        }
        [JsonProperty(PropertyName = "Reg")]
        public string reg
        {
            get { return Reg; }
            set { Reg = value; }
        }



    }
}

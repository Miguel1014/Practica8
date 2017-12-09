using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.ObjectModel;
using Microsoft.Graph;
using System.Net.Http.Headers;
using Microsoft.Identity.Client;

namespace Practica8
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class N_Solicitud : ContentPage
    {
        public ObservableCollection<Tecnicos_Miguel> OC_tecnico { get; set; }
        public static IMobileServiceTable<Tecnicos_Miguel> Tabla;
        public ObservableCollection<Areas_Miguel> OC_area { get; set; }
        public static IMobileServiceTable<Areas_Miguel> Tabla1;
        public ObservableCollection<Solicitud_Miguel> OC_tareas { get; set; }
        public static IMobileServiceTable<Solicitud_Miguel> Tabla2;
        public List<string> TItems { get; private set; }
        public List<string> ID { get; private set; }
        public List<string> TReg { get; private set; }
        public ObservableCollection<Solicitud_Miguel> Items { get; set; }
        public string correo, Xid, XReg;
        public int ZReg;
        public N_Solicitud()
        {
            InitializeComponent();
            Tabla = Practica8.Autenticacion.Cliente.GetTable<Tecnicos_Miguel>();
            Tabla1 = Practica8.Autenticacion.Cliente.GetTable<Areas_Miguel>();
            Tabla2 = Practica8.Autenticacion.Cliente.GetTable<Solicitud_Miguel>();

            string[] prioridad = {"Alta", "Mediana", "Baja" };
            VPrioridad.ItemsSource = prioridad;
            VPrioridad.SelectedItem = 3;

            LeerTabla();
            LeerTabla1();
            LeerTabla4();
            
        }
        private async void LeerTabla4()
        {

            IEnumerable<Solicitud_Miguel> elementos = await Tabla2.Where(Solicitud_Miguel=>Solicitud_Miguel.status=="Activo").ToEnumerableAsync();

            Items = new ObservableCollection<Solicitud_Miguel>(elementos);

            BindingContext = this;
        }

    

        async void insertar(object sender, EventArgs e)
        {
            try { 

            string Mcorreo = ((User)Autenticacion.Me).UserPrincipalName;
            ZReg++;

            var datos = new Solicitud_Miguel

            {

                problema = VProblema.Text,
                tecnico = Convert.ToString(VTecnico.SelectedItem),
                elaboracion = DateTime.Now.ToLocalTime(),
                carea = Convert.ToString(VDept.SelectedItem),
                fecha_limite = VFecha.Date,
                hora_limite = VHora.Time,
                descripcion = VDescripcion.Text,
                status = "activo",
                id_tecnico = Xid,
                reg = "" + ZReg,
                prioridad = Convert.ToString(VPrioridad.SelectedItem)
                  

            };


            await N_Solicitud.Tabla2.InsertAsync(datos);
            await DisplayAlert("Ok", "Tarea asignada", "Ok");




            

            string tiempo = TimeZoneInfo.Utc.ToString();
          

            var evento = new Event
            {
                Attendees = new List<Attendee>() { new Attendee() { EmailAddress = new EmailAddress() { Address = correo } } },
                Subject = "Tarea Asignada",
                Body = new ItemBody
                {
                    Content = "Hola " + VTecnico.SelectedItem + " Tienes una nueva tarea, <br/>" +
                                                         "Problema: " + VProblema.Text + " <br/>" +
                                                         "Descripcion : " + VDescripcion.Text + " <br/>" +
                                                         "Area: " + VDept.SelectedItem + " <br/>" +
                                                         "Hora Limite: " + VHora.Time + " <br/>" +
                                                         "Fecha Limite: " + VFecha.Date + " <br/> <br/>" +
                                                          $"Administrador :{((User)Autenticacion.Me).DisplayName}<br/> <br/> <br/>" +

                                                          $"Enviado desde { Xamarin.Forms.Device.RuntimePlatform }",
                    ContentType = BodyType.Html
                },
                Start = new DateTimeTimeZone
                {
                    DateTime = DateTime.Now.ToString(),
                    TimeZone = TimeZoneInfo.Utc.StandardName
                },
                End = new DateTimeTimeZone
                {
                    DateTime = VFecha.Date.Date.ToString(),
                    TimeZone = TimeZoneInfo.Utc.StandardName
                },
                Location = new Location {
                    DisplayName = "Tecnologico de Estudios Superiores de Huixquilucan"
                }
                
            };

            var req2 = Practica8.Autenticacion.Client.Me.Events;
            await req2.Request().AddAsync(evento);

            }
            catch
            {
                await DisplayAlert("Ok", "Error datos no validos", "Ok");
            }

            var req1 = Practica8.Autenticacion.Client.Me.Events;
            {
                await req1.Request().GetAsync();
            }
            
    }




        private async void LeerTabla()
        {
            IEnumerable<Tecnicos_Miguel> elementos = await Tabla.ToEnumerableAsync();
            OC_tecnico = new ObservableCollection<Tecnicos_Miguel>(elementos);
            string[] tecnicos = new string[OC_tecnico.Count()];
            int i = 0;
            foreach (var tecnico in OC_tecnico)
            {
                tecnicos[i] = tecnico.nombre+" "+ tecnico.apellidos;
                //tecnicos[i] = tecnico.nombre;
                i++;
            }
            VTecnico.ItemsSource = tecnicos;
            VTecnico.SelectedIndex = 0;
 }

        private async void LeerTabla1()
        {
            IEnumerable<Areas_Miguel> elementos = await Tabla1.ToEnumerableAsync();
            OC_area = new ObservableCollection<Areas_Miguel>(elementos);
            string[] tecnicos = new string[OC_area.Count()];
            int i = 0;
            foreach (var tecnico in OC_area)
            {
                tecnicos[i] = tecnico.area;
                i++;
            }
            VDept.ItemsSource = tecnicos;
            VDept.SelectedIndex = 0;
        }

      


        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            

        }


        

        async void Tecnico_SelectedIndexChanged(object sender, EventArgs e)
        {
  var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            
            if (selectedIndex != -1)
            {
 TItems = await Tabla.Select(Tecnicos_Miguel => Tecnicos_Miguel.correo).ToListAsync();
                ID = await Tabla.Select(Tecnicos_Miguel => Tecnicos_Miguel.id).ToListAsync();

                correo = TItems[selectedIndex];
                Xid = ID[selectedIndex];
            }}

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
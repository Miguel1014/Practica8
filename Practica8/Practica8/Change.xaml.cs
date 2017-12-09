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

    public partial class Change : ContentPage
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
        public string ZReg;
        public string ATecnico,AID;
        public string AArea;
        public DateTime AFecha;
        public Change(Object SelectedItem)

        {
            InitializeComponent();
            Tabla = Practica8.Autenticacion.Cliente.GetTable<Tecnicos_Miguel>();
            Tabla1 = Practica8.Autenticacion.Cliente.GetTable<Areas_Miguel>();
            Tabla2 = Practica8.Autenticacion.Cliente.GetTable<Solicitud_Miguel>();

            string[] prioridad = { "Alta", "Mediana", "Baja" };
            VPrioridad.ItemsSource = prioridad;
            

            LeerTabla();
            LeerTabla1();
            LeerTabla4();
          

            var Datos = SelectedItem as Solicitud_Miguel;
            VProblema.Text = Datos.problema;
            ATecnico = Datos.tecnico;
            AFecha = Datos.fecha_limite;
            VHora.Time = Datos.hora_limite;
            AArea = Datos.carea;
            VPrioridad.SelectedItem = Datos.prioridad;
            VDescripcion.Text = Datos.descripcion;
            AID = Datos.id;
            ZReg   = Datos.reg;
        }
        private async void LeerTabla4()
        {

            IEnumerable<Solicitud_Miguel> elementos = await Tabla2.ToEnumerableAsync();

            Items = new ObservableCollection<Solicitud_Miguel>(elementos);

            BindingContext = this;
        }
     
        
        private async void LeerTabla()
        {
            
            IEnumerable<Tecnicos_Miguel> elementos = await Tabla.ToEnumerableAsync();
            OC_tecnico = new ObservableCollection<Tecnicos_Miguel>(elementos);
            string[] tecnicos = new string[OC_tecnico.Count()];
            int i = 0;
            foreach (var tecnico in OC_tecnico)
            {
                tecnicos[i] = tecnico.nombre + " " + tecnico.apellidos;
                //tecnicos[i] = tecnico.nombre;
                i++;
            }
            VTecnico.ItemsSource = tecnicos;
            VTecnico.SelectedIndex = 0;
            VTecnico.SelectedItem = ATecnico;
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
            VDept.SelectedItem = AArea;
            VFecha.Date = AFecha;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
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
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var datos = new Solicitud_Miguel
            {
                id=AID
            };


            var email = new Message
            {

                ToRecipients = new List<Recipient>() { new Recipient() { EmailAddress = new EmailAddress() { Address = correo } } },
                Subject = "TAREA CANCELADA!!!",
                Body = new ItemBody
                {
                    Content = "Hola  " + (((User)Autenticacion.Me).DisplayName) + " la tarea se cancelo deberas cancelar el evento tambien, <br/>" +
                                                        "Problema: " + VProblema.Text + " <br/>" +
                                                         "Descripcion : " + VDescripcion.Text + " <br/>" +
                                                         "Area: " + VDept.SelectedItem + " <br/>" +
                                                         "Hora Limite: " + VHora.Time + " <br/>" +
                                                         "Fecha Limite: " + VFecha.Date + " <br/> <br/>" +

                                                          $"Enviado desde  { Xamarin.Forms.Device.RuntimePlatform }",
                    ContentType = BodyType.Html
                }
            };

            var req = Practica8.Autenticacion.Client.Me.SendMail(email);
            await req.Request().PostAsync();

            await Change.Tabla2.DeleteAsync(datos);
            await DisplayAlert("Ok", "Tarea Eliminada", "Ok");
        }



        async void insertar(object sender, EventArgs e)
        {


            string Mcorreo = ((User)Autenticacion.Me).UserPrincipalName;
            

            var datos = new Solicitud_Miguel

            {
                id=AID,
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


            await Change.Tabla2.UpdateAsync(datos);
            await DisplayAlert("Ok", "Tarea Actualziada", "Ok");

            string tiempo = TimeZoneInfo.Utc.ToString();


            var evento = new Event
            {
                Attendees = new List<Attendee>() { new Attendee() { EmailAddress = new EmailAddress() { Address = correo } } },
                Subject = "Tarea Modificada",
                Body = new ItemBody
                {
                    Content = "Hola " + VTecnico.SelectedItem + " la tarea a sido modificada, <br/>" +
                                                         "Problema: " + VProblema.Text + " <br/>" +
                                                         "Descripcion : " + VDescripcion.Text + " <br/>" +
                                                         "Area: " + VDept.SelectedItem + " <br/>" +
                                                         "Hora Limite: " + VHora.Time + " <br/>" +
                                                         "Fecha Limite: " + VFecha.Date + " <br/> <br/>" +
                                                       

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
                Location = new Location
                {
                    DisplayName = "Tecnologico de Estudios Superiores de Huixquilucan"
                }

            };

            var req2 = Practica8.Autenticacion.Client.Me.Events;
            await req2.Request().AddAsync(evento);



            var req1 = Practica8.Autenticacion.Client.Me.Events;
            {
                await req1.Request().GetAsync();
            }

        }
    }
}
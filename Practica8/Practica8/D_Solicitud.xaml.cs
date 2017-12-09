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

    public partial class D_Solicitud : ContentPage
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
        public string correo, Xid, XReg,ATecnico,AID_Tecnico;
        public DateTime AFecha;
        
        public int ZReg;
        public string ID_Tarea;
        public D_Solicitud(Object SelectedItem)

        {
            var Datos = SelectedItem as Solicitud_Miguel;
            BindingContext = Datos;
            InitializeComponent();
            AFecha = Datos.fecha_limite;
            
            VDept.Text = Datos.carea;
            VPrioridad.Text = Datos.prioridad;
            VDescripcion.Text = Datos.descripcion;
            ID_Tarea = Datos.id;
            ATecnico = Datos.tecnico;
            AID_Tecnico = Datos.id_tecnico;
            Tabla = Practica8.Autenticacion.Cliente.GetTable<Tecnicos_Miguel>();
            Tabla1 = Practica8.Autenticacion.Cliente.GetTable<Areas_Miguel>();
            Tabla2 = Practica8.Autenticacion.Cliente.GetTable<Solicitud_Miguel>();

            string[] semestres = {"Completada"};
            VStatus.ItemsSource = semestres;
            VStatus.SelectedItem =1;

            // LeerTabla();
            //LeerTabla1();


        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem == null)
                return;
            
        }

        async void insertar(object sender, EventArgs e, Object SelectedItem)
        { var datos = new Solicitud_Miguel

            {   
                id = ID_Tarea,
                problema=VProblema.Text,
                tecnico=ATecnico,
                fecha_limite=AFecha,
                descripcion=VDescripcion.Text,
                id_tecnico=AID_Tecnico,
                carea=VDept.Text,
                prioridad=VPrioridad.Text,
                comentarios=VComentarios.Text,
                status= Convert.ToString(VStatus.SelectedItem)
            };

            await D_Solicitud.Tabla2.UpdateAsync(datos);
            await DisplayAlert("Ok", "Listo", "Ok");

            if (VStatus.SelectedItem.ToString().Equals("Completada"))
            {


                
                var email = new Message
                {

                    ToRecipients = new List<Recipient>() { new Recipient() { EmailAddress = new EmailAddress() { Address = Autenticacion.CorreoAdmin } } },
                    Subject = "TAREA TERMINADA!!!",
                    Body = new ItemBody
                    {
                        Content = "El tecnico " + (((User)Autenticacion.Me).DisplayName) + " completo su tarea, <br/>" +
                                                         "Problema: " + VProblema.Text + " <br/>" +
                                                         "Descripcion: " + VDescripcion.Text + " <br/>" +
                                                         "Area: " + VDept.Text + " <br/>" +
                                                         "Prioridad :" + VPrioridad.Text + "<br/>" +
                                                         "Comentarios :" + VComentarios.Text + "<br/>" +
                                                          $"Enviado desde  { Xamarin.Forms.Device.RuntimePlatform }",
                        ContentType = BodyType.Html
                    }
                };

                var req = Practica8.Autenticacion.Client.Me.SendMail(email);
                await req.Request().PostAsync();

                await Navigation.PushAsync(new Vista3());
            }

            /*
            if (VStatus.SelectedItem.ToString().Equals("En ejecucion"))
            {


                var DatosTarea = SelectedItem as Solicitud_Miguel;
                var email = new Message
                {

                    ToRecipients = new List<Recipient>() { new Recipient() { EmailAddress = new EmailAddress() { Address = Autenticacion.CorreoAdmin } } },
                    Subject = "TAREA EN EJECUCIÓN!!!",
                    Body = new ItemBody
                    {
                        Content = "El tecnico" + (((User)Autenticacion.Me).UserPrincipalName) + " esta llevando a cabo su tarea, <br/>" +
                                                         "Problema: " + VProblema.Text + " <br/>" +
                                                         "Descripcion: " + VDescripcion.Text + " <br/>" +
                                                         "Area: " + VDept.Text + " <br/>" +
                                                         "Hora Limite: " + VHora.Text + " <br/>" +
                                                         "Fecha Limite: " + VFecha.Text + " <br/>" +
                                                          "Prioridad :" + VPrioridad.Text + "<br/>"+

                                                          $"Enviado desde  { Xamarin.Forms.Device.RuntimePlatform }",
                        ContentType = BodyType.Html
                    }
                };

                var req = Practica8.Autenticacion.Client.Me.SendMail(email);
                await req.Request().PostAsync();

                await Navigation.PushAsync(new Vista3());
            }

    */
            

        }
    }
}
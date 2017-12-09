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

namespace Practica8
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Vista2 : ContentPage
    {
        public ObservableCollection<Solicitud_Miguel> Items { get; set; }
        public ObservableCollection<Tecnicos_Miguel> tecnicos { get; set; }
        public ObservableCollection<Solicitud_Miguel> fechas { get; set; }
        public static IMobileServiceTable<Solicitud_Miguel> Tabla;
        public static IMobileServiceTable<Tecnicos_Miguel> Tabla1;

        public Vista2()
        {
            InitializeComponent();

            Tabla = Practica8.Autenticacion.Cliente.GetTable<Solicitud_Miguel>();
            Tabla1 = Practica8.Autenticacion.Cliente.GetTable<Tecnicos_Miguel>();
            LeerTabla();

        }

        public async void LeerTabla()
        {
            

            var correo1  = (((User)Autenticacion.Me).UserPrincipalName);

            string hoy = DateTime.Today.ToString();

            IEnumerable<Solicitud_Miguel> fec = await Tabla.Where(Solicitud_Miguel=> Solicitud_Miguel.fecha_limite< DateTime.Today).ToEnumerableAsync();
            fechas = new ObservableCollection<Solicitud_Miguel>(fec);
            string[] Sfecha = new string[fechas.Count()];
            string[] Sproblema = new string[fechas.Count()];
            string[] STecnico = new string[fechas.Count()];
            int i = 0;
            foreach (var Sfechas in fechas)
            {
                Sfecha[i] = Sfechas.id;
                Sproblema[i] = Sfechas.problema;
                STecnico[i] = Sfechas.tecnico;
                var datos = new Solicitud_Miguel

                {
                    id = Sfecha[i],
                    problema = Sproblema[i],
                    tecnico = STecnico[i],
                    status = "No completado"
                };


                await Vista2.Tabla.UpdateAsync(datos);

                var email = new Message
                {

                    ToRecipients = new List<Recipient>() { new Recipient() { EmailAddress = new EmailAddress() { Address = Autenticacion.CorreoAdmin } } },
                    Subject = "TAREA NO COMPLETADA!!!",
                    Body = new ItemBody
                    {
                        Content = "Una tarea ya ha vencido su fecha limite y pasara como no completada, <br/>" +
                        "Problema= " + Sproblema[i] + "<br/>" +
                        "Problema= " + STecnico[i] + "<br/>" +

                                                        $"Enviado desde  { Xamarin.Forms.Device.RuntimePlatform }",
                        ContentType = BodyType.Html
                    }
                };


                i++;
            }






            IEnumerable<Tecnicos_Miguel> elementos1 = await Tabla1.Where(Tecnicos_Miguel => Tecnicos_Miguel.correo == correo1).ToEnumerableAsync();
            tecnicos = new ObservableCollection<Tecnicos_Miguel>(elementos1);
            string[] nombres = new string[tecnicos.Count()];
            int j = 0;
            foreach (var nombre in tecnicos)
            {
                nombres[j] = nombre.id;
            }
            string nomb = nombres[0];
            

            IEnumerable<Solicitud_Miguel> elementos = await Tabla.Where(Solicitud_Miguel => Solicitud_Miguel.status==("Activo")).Where(Solicitud_Miguel=> Solicitud_Miguel.id_tecnico==nomb).ToEnumerableAsync();
            
            Items = new ObservableCollection<Solicitud_Miguel>(elementos);
            
            BindingContext = this;
 }
       



                private void insersion_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Insertar());
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem == null)
                return;
            Navigation.PushAsync(new D_Solicitud (myListV.SelectedItem as Solicitud_Miguel));
        }

        async void Reciclaje(object sender, EventArgs e)
        {           
           await Navigation.PushAsync(new Reciclaje());
           }
    }
}
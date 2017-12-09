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
    public partial class Vista3 : ContentPage
    {
        public ObservableCollection<Solicitud_Miguel> Items { get; set; }
        public ObservableCollection<Solicitud_Miguel> Items1 { get; set; }
        public ObservableCollection<Tecnicos_Miguel> tecnicos { get; set; }
        public static IMobileServiceTable<Solicitud_Miguel> Tabla;
        public static IMobileServiceTable<Tecnicos_Miguel> Tabla1;

        public Vista3()
        {
            InitializeComponent();

            Tabla = Practica8.Autenticacion.Cliente.GetTable<Solicitud_Miguel>();
            Tabla1 = Practica8.Autenticacion.Cliente.GetTable<Tecnicos_Miguel>();
            LeerTabla();

        }

        private async void LeerTabla()
        {
            

            var correo1  = (((User)Autenticacion.Me).UserPrincipalName);


            
            IEnumerable<Tecnicos_Miguel> elementos1 = await Tabla1.Where(Tecnicos_Miguel => Tecnicos_Miguel.correo == correo1).ToEnumerableAsync();
            tecnicos = new ObservableCollection<Tecnicos_Miguel>(elementos1);
            string[] nombres = new string[tecnicos.Count()];
            int i = 0;
            foreach (var nombre in tecnicos)
            {
                nombres[i] = nombre.id;
                
            }
            string nomb = nombres[0];
            

            IEnumerable<Solicitud_Miguel> elementos = await Tabla.Where(Solicitud_Miguel => Solicitud_Miguel.status==("Activo")).Where(Solicitud_Miguel=> Solicitud_Miguel.id_tecnico==nomb).Where(Solicitud_Miguel => Solicitud_Miguel.prioridad == "Alta").ToEnumerableAsync();
            
            Items = new ObservableCollection<Solicitud_Miguel>(elementos);

            IEnumerable<Solicitud_Miguel> elementosR = await Tabla.OrderBy(Solicitud_Miguel => Solicitud_Miguel.fecha_limite).Where(Solicitud_Miguel => Solicitud_Miguel.id_tecnico == nomb).Where(Solicitud_Miguel => Solicitud_Miguel.status == "Activo").ToEnumerableAsync();

            Items1 = new ObservableCollection<Solicitud_Miguel>(elementosR);

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
            Navigation.PushAsync(new D_Solicitud(myListV.SelectedItem as Solicitud_Miguel));
        }

        async void Reciclaje(object sender, EventArgs e)
        {
           


           await Navigation.PushAsync(new Reciclaje());
          



        }

        private void myListV2_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            Navigation.PushAsync(new D_Solicitud(myListV2.SelectedItem as Solicitud_Miguel));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.ObjectModel;


namespace Practica8
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Vista : ContentPage
    {
        public ObservableCollection<_13090414> Items { get; set; }

        public static IMobileServiceTable<_13090414> Tabla;

        public Vista()
        {
            InitializeComponent();

            Tabla = Practica8.Autenticacion.Cliente.GetTable<_13090414>();
            LeerTabla();

        }

        public class id
        {
            public static string Ident;
        }
        

        private async void LeerTabla()
        {

            IEnumerable < _13090414> elementos = await Tabla.Where(_13090414 => _13090414.Deleted==false).ToEnumerableAsync();
            Items = new ObservableCollection<_13090414>(elementos);
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
            Navigation.PushAsync(new Change (myListV.SelectedItem as _13090414));
        }

        async void Reciclaje(object sender, EventArgs e)
        {
           


           await Navigation.PushAsync(new Reciclaje());
          



        }
    }
}

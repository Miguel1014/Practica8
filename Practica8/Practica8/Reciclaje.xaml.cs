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
    public partial class Reciclaje : ContentPage
    {
        
        public ObservableCollection<_13090414> Items { get; set; }
        public static MobileServiceClient Cliente;
        public static IMobileServiceTable<_13090414> Tabla;

        public Reciclaje()
        {
            InitializeComponent();
            Cliente = new MobileServiceClient(AzureConnection.AzureURL);
            Tabla = Cliente.GetTable<_13090414>() ;
            LeerTabla();
            ;


        }

        private async void LeerTabla()
        {
            IEnumerable<_13090414> items = await Tabla.IncludeDeleted().Where(_13090414 => _13090414.Deleted == true).ToEnumerableAsync();
            Items = new ObservableCollection<_13090414>(items);
            BindingContext = this;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            Navigation.PushAsync(new Aceptar(myListV.SelectedItem as _13090414));

        }
    }
}

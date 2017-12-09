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
    public partial class Historial : ContentPage
    {
        public ObservableCollection<Solicitud_Miguel> Items { get; set; }
        public static IMobileServiceTable<Solicitud_Miguel> Tabla;
        
 public Historial()
        {
            InitializeComponent();

            Tabla = Practica8.Autenticacion.Cliente.GetTable<Solicitud_Miguel>();
           
            LeerTabla();

        }

        private async void LeerTabla()
        {

            IEnumerable<Solicitud_Miguel> elementos = await Tabla.ToEnumerableAsync();
            
            Items = new ObservableCollection<Solicitud_Miguel>(elementos);
            
            BindingContext = this;
 }
       



          
      

        
    }
}

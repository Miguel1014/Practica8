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

	public partial class Area : ContentPage
{
        public ObservableCollection<Areas_Miguel> OC_area { get; set; }
        public static IMobileServiceTable<Areas_Miguel> Tabla;

        public Area ()
		{

			InitializeComponent ();
            Tabla = Practica8.Autenticacion.Cliente.GetTable<Areas_Miguel>();
            LeerTabla();



        }
        private async void LeerTabla()
        {


            IEnumerable<Areas_Miguel> elementos = await Tabla.ToEnumerableAsync();
            OC_area = new ObservableCollection<Areas_Miguel>(elementos);
            string[] areas = new string[OC_area.Count()];
            int i = 0;
            foreach(var area in OC_area)
            {
                areas[i] = area.area;
                i++;
            }
            picker.ItemsSource = areas;
            picker.SelectedIndex = 0;

           
            


        }



        async void insertar(object sender, EventArgs e)
        {
            try
            {

                var Datos = new Areas_Miguel

                {
                    area = VArea.Text

                };


                await Area.Tabla.InsertAsync(Datos);
                await DisplayAlert("Inserción", "Area Insertada", "Ok");

            }
            catch
            {
                await DisplayAlert("Error", "Area no insertada", "Ok");
            }

            LeerTabla();
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {

            
                

        }
    }
}
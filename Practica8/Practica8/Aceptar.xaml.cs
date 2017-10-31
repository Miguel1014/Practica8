using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Practica8
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Aceptar : ContentPage
    {
        string VID;
        public Aceptar(Object SelectedItem)
        {



            var Datos = SelectedItem as _13090414;
            BindingContext = Datos;
            InitializeComponent();

            VID = Datos.Id;



            string[] semestres = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            picker1.ItemsSource = semestres;
            picker1.SelectedItem = Datos.Semestre;

            string[] carreras = { "Ing. Sistemas Computacionales", "Ingenieria Civil", "Ingenieria Industrial", "Gastronomia", "Lic. Biologia", "Lic. Administración", "Ingenieria en Mecatronica" };
            picker.ItemsSource = carreras;
            picker.SelectedItem = Datos.Carrera;





        }
        async void Button_Actualizar_Clicked(object sender, EventArgs e)
        {



            var datos = new _13090414

            {
                Id = VID,
                

            };

           
            await Vista.Tabla.UndeleteAsync(datos);
            await Navigation.PushAsync(new Vista());
        }

        private void VTelefono_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        async void Eliminar(object sender, EventArgs e)
        {
           

            
            await Navigation.PushAsync(new Reciclaje());

        }
    }
}
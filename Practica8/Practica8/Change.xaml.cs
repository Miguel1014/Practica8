using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;

namespace Practica8
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Change : ContentPage
    {
        string VID;
        public Change(Object SelectedItem)
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


            try
            {
                var datos = new _13090414

                {
                    Id = VID,
                    Matricula = (long)Convert.ToInt64(VMatricula.Text),
                    Nombre = VNombre.Text,
                    Apellidos = VApellido.Text,
                    Direccion = VDireccion.Text,
                    Telefono = (long)Convert.ToInt64(VTelefono.Text),
                    Carrera = Convert.ToString(picker.SelectedItem),
                    Semestre = Convert.ToString(picker1.SelectedItem),
                    Correo = VCorreo.Text,
                    Github = VGithub.Text

                };


                await Vista.Tabla.UpdateAsync(datos);
                await Navigation.PushAsync(new Vista());

            }
            catch
            {
                await DisplayAlert("Error", "Datos no validados", "Ok");
            }



        }

        private void VTelefono_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        async void Eliminar(object sender, EventArgs e)
        {
            var datos = new _13090414

            {
                Id = VID,   



            };


            await Vista.Tabla.DeleteAsync(datos);
            await Navigation.PushAsync(new Vista());

        }
    }
}
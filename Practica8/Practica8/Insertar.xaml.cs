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
    public partial class Insertar : ContentPage
    {
       
        public Insertar()
        {
            InitializeComponent();
            
            string[] semestres = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            picker1.ItemsSource = semestres;
            picker1.SelectedIndex = 0;

            string[] carreras = { "Ing. Sistemas Computacionales", "Ingenieria Civil", "Ingenieria Industrial", "Gastronomia", "Lic. Biologia", "Lic. Administración", "Ingenieria en Mecatronica" };
            picker.ItemsSource = carreras;
            picker.SelectedIndex = 0;
            


        }

        async void Button_Actualizar_Clicked(object sender, EventArgs e)
        {
            try
            {

                var datos = new _13090414

                {
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

              
                    await Vista.Tabla.InsertAsync(datos);
                await Navigation.PushAsync(new Vista());
            }
            catch
            {
                await DisplayAlert("Error", "Datos no validados", "Ok");
            }


        }

        private void Button_Eliminar_Clicked(object sender, EventArgs e)
        {


        }

        private void VTelefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            int limite = 10;


            string text = VTelefono.Text;
            if (text.Length > limite)
            {
                text = text.Remove(text.Length - 1);
                VTelefono.Text = text;
            }

        }

        private void VMatricula_TextChanged(object sender, TextChangedEventArgs e)
        {
            int limite = 10;


            string text = VMatricula.Text;
            if (text.Length > limite)
            {
                text = text.Remove(text.Length - 1);
                VMatricula.Text = text;
            }
        }
    }
}
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
    public partial class Insertar : ContentPage
    {

        public ObservableCollection<Tecnicos_Miguel> area { get; set; }
        public static IMobileServiceTable<Tecnicos_Miguel> Tabla;

        public Insertar()
        {
            InitializeComponent();

            Tabla = Practica8.Autenticacion.Cliente.GetTable < Tecnicos_Miguel>();



        }

        async void Aceptar(object sender, EventArgs e)
        {
            try
            {

                var datos = new Tecnicos_Miguel

                {
                    nombre = VNombre.Text,
                    apellidos = VApellidos.Text,
                    especialidad = VEspecialidad.Text,
                    telefono = VTelefono.Text,
                    direccion = VDireccion.Text,
                    correo = VCorreo.Text,
                   


                };

              
                    await Insertar.Tabla.InsertAsync(datos);
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

        async void Actualziar(object sender, EventArgs e)
        {
            try
            {

                var datos = new Tecnicos_Miguel

                {
                    id = "d206c90d-c365-4794-afcd-bf124c5ca056",
                    nombre = VNombre.Text,
                    apellidos = VApellidos.Text,
                    especialidad = VEspecialidad.Text,
                    telefono = VTelefono.Text,
                    direccion = VDireccion.Text,
                    correo = VCorreo.Text
                   


                };


                await Insertar.Tabla.UpdateAsync(datos);
                await Navigation.PushAsync(new Vista());
            }

            catch
            {
                await DisplayAlert("Error", "Datos no validados", "Ok");
            }
        }
    }
}
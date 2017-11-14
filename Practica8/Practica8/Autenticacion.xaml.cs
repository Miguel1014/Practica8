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
    public partial class Autenticacion : ContentPage

    {
        public static MobileServiceClient Cliente;
        public static MobileServiceUser usuario;
        public Autenticacion()
        {

            Cliente = new MobileServiceClient(AzureConnection.AzureURL);

            InitializeComponent();

            
        }

        private async void Login(object sender, EventArgs e)
        {
            try
            {
                usuario = await App.Authenticator.Authenticate();
                if (App.Authenticator != null)
                {
                    if (usuario != null)
                    {
                        await DisplayAlert("Usuario Autenticado", usuario.UserId, "ok");
                        await Navigation.PushAsync(new Vista());
                    }
                    if (usuario == null)
                    {
                        await DisplayAlert("No", usuario.UserId, "ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "ok");
            }
        }

    }
}
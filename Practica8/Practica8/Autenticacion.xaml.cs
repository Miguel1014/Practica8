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
using System.Net.Http.Headers;
using Microsoft.Identity.Client;

namespace Practica8
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Autenticacion : ContentPage

    {
        public static MobileServiceClient Cliente;
        public static MobileServiceUser usuario;
        public static AuthenticationResult result;
        public static GraphServiceClient Client;      
        public static DirectoryObject Me;
        public static string Nombre;
        public static string CorreoAdmin = "siul-leugim10@hotmail.com"; //Debe de poner solo su correo profe, para poder entrar como administrador


        public Autenticacion()
        {
            
            Cliente = new MobileServiceClient(AzureConnection.AzureURL);
            InitializeComponent();
            }
           public class datos
        {
            public static Label  Nombre1 = new Label();
            
        }  

        public static async Task<string> gettoken()
        {
            result = await App.IdentityClientApp.AcquireTokenAsync(App.Scopes, App.UiParent).ConfigureAwait(false);
            return result.AccessToken;
        }   
        

        public async  void Login(object sender, EventArgs e)
        {
            try
            {
                usuario = await App.Authenticator.Authenticate();
                var resultado = await gettoken();
                if (App.Authenticator != null)
                {
                    if (usuario != null)
                    {
                        //AuthenticationResult ar = await App.IdentityClientApp.AcquireTokenAsync(App.Scopes, App.UiParent).ConfigureAwait(false);
                        DelegateAuthenticationProvider provider = new DelegateAuthenticationProvider(async (requestMessage) =>
                        {
                            requestMessage.Headers.Authorization =  new AuthenticationHeaderValue("bearer", resultado.ToString());
                        });
                        Client = new GraphServiceClient("https://graph.microsoft.com/v1.0", provider);

                        Me = await Client.Me.Request().GetAsync();

                        if (((User)Me).UserPrincipalName.Equals(CorreoAdmin)) 
                        {

                            datos.Nombre1.Text = ((User)Me).DisplayName;
                            await DisplayAlert("Bienvenido", ((User)Me).DisplayName, "ok");
                            
                           

                            await Navigation.PushAsync(new MasterDetailPage1());

                           
                            
                        }
                        else
                        {
                            await DisplayAlert("Bienvenido", ((User)Me).DisplayName, "ok");
                            await Navigation.PushAsync(new Master_Tecnico()); 
                        }
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
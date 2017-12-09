using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Xamarin.Forms;

namespace Practica8
{
    public partial class App : Application
    {
        public static ISQLAzure Authenticator { get; private set; }
        public static void Init(ISQLAzure authenticator) {
            Authenticator = authenticator;
        }

        public static PublicClientApplication IdentityClientApp = null;
        public static UIParent UiParent = null;

        public static string ClientID = "78ff4792-4fb1-4d7e-9f0d-b843d468bfed"; // TODO: Replace this with the Application ID from Step #1.
        public static string[] Scopes = { "User.ReadWrite", "User.ReadBasic.All", "User.Read", "User.ReadBasic.All ", "Mail.Send", "Calendars.ReadWrite","User.Read.All", "User.ReadWrite.All" };



        public App()
        {
            InitializeComponent();
            IdentityClientApp = new PublicClientApplication(ClientID);
            MainPage = new NavigationPage(new Practica8.Autenticacion());
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

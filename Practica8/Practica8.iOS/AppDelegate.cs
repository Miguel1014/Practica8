using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Practica8.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, ISQLAzure
    {

        private MobileServiceUser usuario;
        public async Task<MobileServiceUser> Authenticate()
        {
            var message = string.Empty;
            try
            {
                usuario = await Practica8.Autenticacion.Cliente.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController, MobileServiceAuthenticationProvider.MicrosoftAccount, "tesh.azurewebsites.net");
                if (usuario != null)
                {
                    message = string.Format("Usuario autenticado {0}.", usuario.UserId);
                    //await new MessageDialog(user.MobileServiceAuthenticationToken, "Token").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            IUIAlertViewDelegate iUIAlert = null;
            UIAlertView avAlert = new UIAlertView("Resultado de autenticación", message, iUIAlert, "ok", null);
            avAlert.Show();
            return usuario;

        }




        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            App.Init(this);
            LoadApplication(new App());
            App.IdentityClientApp.RedirectUri = "msal78ff4792-4fb1-4d7e-9f0d-b843d468bfed://auth";
            return base.FinishedLaunching(app, options);
        }
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(url);
            return true;
        }
    }
}

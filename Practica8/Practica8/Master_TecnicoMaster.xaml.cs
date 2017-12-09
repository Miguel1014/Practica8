using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Practica8
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master_TecnicoMaster : ContentPage
    {
        public ListView ListView;

        public Master_TecnicoMaster()
        {
            InitializeComponent();
            VNombre.Text = (((User)Autenticacion.Me).UserPrincipalName);
            
            BindingContext = new Master_TecnicoMasterViewModel();
            ListView = MenuItemsListView;
        }

        class Master_TecnicoMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<Master_TecnicoMenuItem> MenuItems { get; set; }

            public Master_TecnicoMasterViewModel()
            {
                MenuItems = new ObservableCollection<Master_TecnicoMenuItem>(new[]
                {
                    new Master_TecnicoMenuItem { Id = 0, Title = "Inicio" ,TargetType=typeof(Vista3)},
                    new Master_TecnicoMenuItem { Id = 1, Title = "Tareas" ,TargetType=typeof(Vista2)}                
                  
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        private void Salir_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Autenticacion());
        }
    }
}
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
using Microsoft.Graph;

namespace Practica8
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Master : ContentPage
    {
        public ListView ListView;
       
        public MasterDetailPage1Master()
        {
            InitializeComponent();

            BindingContext = new MasterDetailPage1MasterViewModel();
            ListView = MenuItemsListView;
            VNombre.Text = (((User)Autenticacion.Me).UserPrincipalName);
        }

        class MasterDetailPage1MasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailPage1MenuItem> MenuItems { get; set; }
            
            public MasterDetailPage1MasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterDetailPage1MenuItem>(new[]
                {
                    new MasterDetailPage1MenuItem { Id = 0, Title = "Inicio" },
                    new MasterDetailPage1MenuItem { Id = 1, Title = "Historial",TargetType=typeof(Historial) },
                    new MasterDetailPage1MenuItem { Id = 2, Title = "Nueva Solicitud " ,TargetType=typeof(N_Solicitud)},
                    new MasterDetailPage1MenuItem { Id = 3, Title = "Tecnicos",TargetType=typeof(Insertar)},
                    new MasterDetailPage1MenuItem { Id = 4, Title = "Departamentos",TargetType=typeof(Area)},
                    
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
    }
}
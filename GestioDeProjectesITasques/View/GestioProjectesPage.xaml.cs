using DB_MySQL;
using ModelGestioProjectes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace GestioDeProjectesITasques.View
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class GestioProjectesPage : Page
    {
        ComponentDB componentDB = new ComponentDB();

        public GestioProjectesPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {


            dgrProjectes.ItemsSource = componentDB.GetLlistaProjectes();
        }

        private void dgrProjectes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrProjectes.SelectedItem != null)
            {
                Projecte proj = (Projecte)dgrProjectes.SelectedItem;
                dgrUsuaris.ItemsSource = componentDB.GetLlistaUsuarisAssignats(proj.Id);
            }
        }
    }
}

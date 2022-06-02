using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class InformesPage : Page
    {
        private ObservableCollection<String> informes = new ObservableCollection<String>();
        private ObservableCollection<String> urlInformes = new ObservableCollection<String>();

        public InformesPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            informes.Add("Usuari amb les seves tasques");
            urlInformes.Add("http://51.68.224.27:8080/jasperserver/flow.html?_flowId=viewReportFlow&_flowId=viewReportFlow&ParentFolderUri=%2Fdam2-areyes%2FM13_Projecte&reportUnit=%2Fdam2-areyes%2FM13_Projecte%2Fusuaris_amb_tasques&standAlone=true");

            informes.Add("Projectes amb tasques pendents");
            urlInformes.Add("http://51.68.224.27:8080/jasperserver/flow.html?_flowId=viewReportFlow&_flowId=viewReportFlow&ParentFolderUri=%2Fdam2-areyes%2FM13_Projecte&reportUnit=%2Fdam2-areyes%2FM13_Projecte%2Fprojectes_amb_tasques_pendents&standAlone=true");

            lvwListaInformes.ItemsSource = informes;
        }

        private void lvwListaInformes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            wvwInforme.Navigate(new Uri(urlInformes[lvwListaInformes.SelectedIndex]));
        }
    }
}

using DB_MySQL;
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

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace GestioDeProjectesITasques.View
{
    public sealed partial class ContentDialogTascaEsborrar : ContentDialog
    {
        public ContentDialogTascaEsborrar()
        {
            this.InitializeComponent();
        }

        ComponentDB componentDB = new ComponentDB();

        private int idTasca;

        public int IdTasca
        {
            get { return idTasca; }
            set { idTasca = value; }
        }

        private void dialogTascaEsborrar_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnEsborrar_Click(object sender, RoutedEventArgs e)
        {
            componentDB.deleteTasca(idTasca);
            dialogTascaEsborrar.Hide();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            dialogTascaEsborrar.Hide();
        }
    }
}

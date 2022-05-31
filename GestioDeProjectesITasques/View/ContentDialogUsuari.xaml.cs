using DB_MySQL;
using ModelGestioProjectes;
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

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace GestioDeProjectesITasques.View
{
    public sealed partial class ContentDialogUsuari : ContentDialog
    {
        public ContentDialogUsuari()
        {
            this.InitializeComponent();
        }

        ComponentDB componentDB = new ComponentDB();
        ObservableCollection<Usuari> usuarisFiltrats;

        private int idProjecte;

        public int IdProjecte
        {
            get { return idProjecte; }
            set { idProjecte = value; }
        }

        private Estat estat;
        public Estat ElEstat
        {
            get { return estat; }
            set { estat = value; }
        }

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            cbxRol.ItemsSource = componentDB.GetLlistaRols();
            dgrUsuaris.ItemsSource = componentDB.GetLlistaUsuarisNoAssignats(idProjecte);
            btnBuscar.IsEnabled = false;
            btnGuardar.IsEnabled = false;
        }

        private void txtNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnBuscar.IsEnabled = true;
        }

        private void txtCognom1_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnBuscar.IsEnabled = true;
        }

        private void cbxRol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (validarFormulari())
            {
                btnGuardar.IsEnabled = true;
            }
        }

        private void dgrUsuaris_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (validarFormulari())
            {
                btnGuardar.IsEnabled = true;
            }
        }
        private bool validarFormulari()
        {
            return cbxRol.SelectedItem != null && dgrUsuaris.SelectedItem != null;
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //projectes.get(i).getNom().toLowerCase().contains(textFiltre.getText().toLowerCase())
            dgrUsuaris.ItemsSource = null;
            usuarisFiltrats = new ObservableCollection<Usuari>();
            for (int i = 0; i < componentDB.GetLlistaUsuarisNoAssignats(idProjecte).Count; i++)
            {
                if ((componentDB.GetLlistaUsuarisNoAssignats(idProjecte)[i].Nom.ToLower().Contains(txtNom.Text) && 
                    componentDB.GetLlistaUsuarisNoAssignats(idProjecte)[i].Cognom1.ToLower().Contains(txtCognom1.Text)) ||
                    (componentDB.GetLlistaUsuarisNoAssignats(idProjecte)[i].Nom.ToLower().Contains(txtNom.Text) && txtCognom1.Text.Length == 0) ||
                    (componentDB.GetLlistaUsuarisNoAssignats(idProjecte)[i].Cognom1.ToLower().Contains(txtCognom1.Text) && txtNom.Text.Length == 0))
                {
                    usuarisFiltrats.Add(componentDB.GetLlistaUsuarisNoAssignats(idProjecte)[i]);
                }
            }
            dgrUsuaris.ItemsSource = usuarisFiltrats;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Usuari usu = (Usuari)dgrUsuaris.SelectedItem;
            Rol rol = (Rol)cbxRol.SelectedItem;
            componentDB.assignarUsuari(IdProjecte, usu.Id, rol.Id);
            dialogUsuari.Hide();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            dialogUsuari.Hide();
        }
    }
}

using DB_MySQL;
using Microsoft.Toolkit.Uwp.UI.Controls;
using ModelGestioProjectes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public sealed partial class ContentDialogProjecte : ContentDialog
    {
        ComponentDB componentDB = new ComponentDB();

        public ContentDialogProjecte()
        {
            this.InitializeComponent();
        }

        private Projecte elProjecte;

        public Projecte ElProjecte
        {
            get { return elProjecte; }
            set { elProjecte = value; }
        }

        private Estat estat;
        public Estat ElEstat
        {
            get { return estat; }
            set { estat = value; }
        }

        private Boolean actualitzarTaula;

        public Boolean ActualitzarTaula
        {
            get { return actualitzarTaula; }
            set { actualitzarTaula = value; }
        }


        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            cbxCapProjecte.ItemsSource = componentDB.GetLlistaUsuaris();
            
            btnCancelar.IsEnabled = true;
            btnGuardar.IsEnabled = false;

            if (elProjecte == null && estat == Estat.ALTA_PROJECTE)
            {
                netejarFormulari();
            } 
            else if (elProjecte != null && estat == Estat.MODIFICACIO_PROJECTE)
            {
                mostrarFormulari();
            }
        }

        private void mostrarFormulari()
        {
            txtNom.Text = elProjecte.Nom;
            txtDescripcio.Text = elProjecte.Descripcio;
            cbxCapProjecte.SelectedItem = elProjecte.CapProjecte;
        }

        private void netejarFormulari()
        {
            txtNom.Text = "";
            txtDescripcio.Text = "";
            cbxCapProjecte.SelectedItem = null;
        }

        private Boolean validarFormulari()
        {
            return Projecte.validaNom(txtNom.Text) && Projecte.validaDescripcio(txtDescripcio.Text) && cbxCapProjecte.SelectedItem != null;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (estat == Estat.ALTA_PROJECTE)
            {
                Projecte proj = new Projecte(1, txtNom.Text, txtDescripcio.Text, (Usuari)cbxCapProjecte.SelectedItem);
                componentDB.addProjecte(proj);
            }
            else if (estat == Estat.MODIFICACIO_PROJECTE)
            {
                Projecte proj = new Projecte(elProjecte.Id, txtNom.Text, txtDescripcio.Text, (Usuari)cbxCapProjecte.SelectedItem);
                componentDB.updateProjecte(proj);
            }

            dialogProjecte.Hide();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            dialogProjecte.Hide();
        }

        private void cbxCapProjecte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (validarFormulari())
            {
                btnGuardar.IsEnabled = true;
            }
        }

        private void txtNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (validarFormulari())
            {
                btnGuardar.IsEnabled = true;
            }
        }

        private void txtDescripcio_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (validarFormulari())
            {
                btnGuardar.IsEnabled = true;
            }
        }
    }
}

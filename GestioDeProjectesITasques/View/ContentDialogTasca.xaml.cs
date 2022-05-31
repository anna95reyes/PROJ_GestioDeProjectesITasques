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

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace GestioDeProjectesITasques.View
{
    public sealed partial class ContentDialogTasca : ContentDialog
    {
        public ContentDialogTasca()
        {
            this.InitializeComponent();
        }

        ComponentDB componentDB = new ComponentDB();

        private int idProjecte;

        public int IdProjecte
        {
            get { return idProjecte; }
            set { idProjecte = value; }
        }

        private Tasca laTasca;

        public Tasca LaTasca
        {
            get { return laTasca; }
            set { laTasca = value; }
        }

        private EnumEstats estat;
        public EnumEstats ElEstat
        {
            get { return estat; }
            set { estat = value; }
        }

        private void dialogTasca_Loaded(object sender, RoutedEventArgs e)
        {
            btnCancelar.IsEnabled = true;
            btnGuardar.IsEnabled = false;

            cbxPropietari.ItemsSource = componentDB.GetLlistaUsuaris();
            cbxResponsable.ItemsSource = componentDB.GetLlistaUsuaris();
            cbxEstat.ItemsSource = componentDB.GetLlistaEstats();

            if (laTasca == null && estat == EnumEstats.ALTA_TASCA)
            {
                netejarFormulari();
            }
            else if (laTasca != null && estat == EnumEstats.MODIFICACIO_TASCA)
            {
                mostrarFormulari();
                cdpDataCreacio.IsEnabled = false;
                cbxPropietari.IsEnabled = false;
            }
        }

        private void mostrarFormulari()
        {
            cdpDataCreacio.Date = laTasca.DataCreacio;
            txtNom.Text = laTasca.Nom;
            txtDescripcio.Text = laTasca.Descripcio != null? laTasca.Descripcio : "";
            cdpDataLimit.Date = laTasca.DataLimit;
            cbxPropietari.SelectedItem = laTasca.Propietari;
            cbxResponsable.SelectedItem = laTasca.Responsable;
            cbxEstat.SelectedItem = laTasca.Estat;
        }

        private void netejarFormulari()
        {
            cdpDataCreacio.Date = DateTime.Now;
            txtNom.Text = "";
            txtDescripcio.Text = "";
            cdpDataLimit.Date = DateTime.Now.AddDays(1);
            cbxPropietari.SelectedItem = null;
            cbxResponsable.SelectedItem = null;
            cbxEstat.SelectedItem = null;
        }

        private Boolean validarFormulari()
        {
            String descripcio = txtDescripcio.Text != "" ? txtDescripcio.Text : null;
            return cdpDataCreacio.Date != null && Tasca.validaNom(txtNom.Text) && Tasca.validaDescripcio(descripcio) &&
                   cbxPropietari.SelectedItem != null && cbxEstat.SelectedItem != null; 
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

        private void cdpDataCreacio_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (validarFormulari())
            {
                btnGuardar.IsEnabled = true;
            }
        }

        private void cdpDataLimit_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (validarFormulari())
            {
                btnGuardar.IsEnabled = true;
            }
        }

        private void cbxPropietari_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (validarFormulari())
            {
                btnGuardar.IsEnabled = true;
            }
        }

        private void cbxResponsable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (validarFormulari())
            {
                btnGuardar.IsEnabled = true;
            }
        }

        private void cbxEstat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (validarFormulari())
            {
                btnGuardar.IsEnabled = true;
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (estat == EnumEstats.ALTA_TASCA)
            {
                DateTime? dataLimit = null;
                if (cdpDataLimit.Date != null) dataLimit = cdpDataLimit.Date.Value.DateTime;
                String descripcio = txtDescripcio.Text != "" ? txtDescripcio.Text : null;

                Tasca tasc = new Tasca(1, cdpDataCreacio.Date.Value.DateTime, txtNom.Text, descripcio,
                                       dataLimit, (Usuari)cbxPropietari.SelectedItem,
                                       (Usuari)cbxResponsable.SelectedItem, (Estat)cbxEstat.SelectedItem);

                componentDB.addTasca(tasc, idProjecte);
            }
            else if (estat == EnumEstats.MODIFICACIO_TASCA)
            {
                DateTime? dataLimit = null;
                if (cdpDataLimit.Date != null) dataLimit = cdpDataLimit.Date.Value.DateTime;
                String descripcio = txtDescripcio.Text != "" ? txtDescripcio.Text : null;

                Tasca tasc = new Tasca(laTasca.Id, laTasca.DataCreacio, txtNom.Text, descripcio,
                                       dataLimit, laTasca.Propietari,
                                       (Usuari)cbxResponsable.SelectedItem, (Estat)cbxEstat.SelectedItem);

                componentDB.updateTasca(tasc);
            }
            dialogTasca.Hide();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            dialogTasca.Hide();
        }
    }
}

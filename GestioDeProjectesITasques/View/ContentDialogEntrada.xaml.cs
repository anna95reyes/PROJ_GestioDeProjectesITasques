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
    public sealed partial class ContentDialogEntrada : ContentDialog
    {
        public ContentDialogEntrada()
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

        private Entrada laEntrada;

        public Entrada LaEntrada
        {
            get { return laEntrada; }
            set { laEntrada = value; }
        }

        private EnumEstats estat;
        public EnumEstats ElEstat
        {
            get { return estat; }
            set { estat = value; }
        }

        private void dialogEntrada_Loaded(object sender, RoutedEventArgs e)
        {
            btnCancelar.IsEnabled = true;
            btnGuardar.IsEnabled = false;

            cbxEscriptor.ItemsSource = componentDB.GetLlistaUsuaris();
            cbxNovaAssignacio.ItemsSource = componentDB.GetLlistaUsuaris();
            cbxNouEstat.ItemsSource = componentDB.GetLlistaEstats();

            if (laEntrada == null && estat == EnumEstats.ALTA_ENTRADA)
            {
                netejarFormulari();
                cbxNovaAssignacio.IsEnabled = false;
            }
            else if (laEntrada != null && estat == EnumEstats.MODIFICACIO_ENTRADA)
            {
                mostrarFormulari();
                cbxEscriptor.IsEnabled = false;
                
            }
        }

        private void mostrarFormulari()
        {
            cdpData.Date = laEntrada.Data;
            txtEntrada.Text = laEntrada._Entrada;
            cbxEscriptor.SelectedItem = laEntrada.Escriptor;
            cbxNovaAssignacio.SelectedItem = laEntrada.NovaAssignacio;
            cbxNouEstat.SelectedItem = laEntrada.NouEstat;
        }

        private void netejarFormulari()
        {
            cdpData.Date = DateTime.Now;
            txtEntrada.Text = "";
            cbxEscriptor.SelectedItem = null;
            cbxNovaAssignacio.SelectedItem = null;
            cbxNouEstat.SelectedItem = null;
        }

        private Boolean validarFormulari()
        {
            return cdpData.Date != null && Entrada.validaEntrada(txtEntrada.Text != "" ? txtEntrada.Text : null) &&
                   cbxEscriptor.SelectedItem != null && cbxNouEstat.SelectedItem != null;
        }

        private void cdpData_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (validarFormulari())
            {
                btnGuardar.IsEnabled = true;
            }
        }

        private void txtEntrada_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (validarFormulari())
            {
                btnGuardar.IsEnabled = true;
            }
        }

        private void cbxEscriptor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (validarFormulari())
            {
                btnGuardar.IsEnabled = true;
            }
        }

        private void cbxNovaAssignacio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (validarFormulari())
            {
                btnGuardar.IsEnabled = true;
            }
        }

        private void cbxNouEstat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (validarFormulari())
            {
                btnGuardar.IsEnabled = true;
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (estat == EnumEstats.ALTA_ENTRADA)
            {
                Entrada entrada = new Entrada(1, cdpData.Date.Value.DateTime, txtEntrada.Text != "" ? txtEntrada.Text : null,
                                              (Usuari)cbxEscriptor.SelectedItem,
                                              cbxNovaAssignacio.SelectedItem != null ? (Usuari)cbxNovaAssignacio.SelectedItem : null,
                                              (Estat)cbxNouEstat.SelectedItem);
                componentDB.addEntrada(idTasca, entrada);
            }
            else if (estat == EnumEstats.MODIFICACIO_ENTRADA)
            {
                Entrada entrada = new Entrada(laEntrada.Numero, cdpData.Date.Value.DateTime, txtEntrada.Text != "" ? txtEntrada.Text : null,
                                              (Usuari)cbxEscriptor.SelectedItem,
                                              cbxNovaAssignacio.SelectedItem != null ? (Usuari)cbxNovaAssignacio.SelectedItem : null,
                                              (Estat)cbxNouEstat.SelectedItem);
                componentDB.updateEntrada(idTasca, entrada);
            }
            dialogEntrada.Hide();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            dialogEntrada.Hide();
        }

        
    }
}

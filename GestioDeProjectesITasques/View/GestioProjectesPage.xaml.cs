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

        public enum Estat
        {
            VIEW,
            ALTA_PROJECTE,
            MODIFICACIO_PROJECTE,
            ASSIGNACIO_USUARI,
            ALTA_TASCA,
            MODIFICACIO_TASCA,
            ALTA_ENTRADA,
            MODIFICACIO_ENTRADA
        }

        ComponentDB componentDB = new ComponentDB();
        Estat estat = Estat.VIEW;

        public GestioProjectesPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dgrProjectes.ItemsSource = componentDB.GetLlistaProjectes();
            canviEstat(Estat.VIEW);
        }

        private void canviEstat (Estat estatNou)
        {
            estat = estatNou;

            if (estat == Estat.VIEW)
            {
                if (dgrProjectes.SelectedItem == null)
                {
                    btnNouProjecte.IsEnabled = true;
                    btnEsborrarProjecte.IsEnabled = false;
                    btnEditarProjecte.IsEnabled = false;

                    btnAssignarUsuari.IsEnabled = false;
                    btnDesassignarUsuari.IsEnabled = false;

                    btnNovaTasca.IsEnabled = false;
                    btnEsborrarTasca.IsEnabled = false;
                    btnEditarTasca.IsEnabled = false;

                    btnNovaEntrada.IsEnabled = false;
                    btnEsborrarEntrada.IsEnabled = false;
                    btnEditarEntrada.IsEnabled = false;
                }
                else if (dgrProjectes.SelectedItem != null)
                {
                    btnNouProjecte.IsEnabled = false;
                    btnEsborrarProjecte.IsEnabled = true;
                    btnEditarProjecte.IsEnabled = true;

                    btnAssignarUsuari.IsEnabled = true;
                    btnDesassignarUsuari.IsEnabled = false;

                    btnNovaTasca.IsEnabled = true;
                    btnEsborrarTasca.IsEnabled = false;
                    btnEditarTasca.IsEnabled = false;

                    btnNovaEntrada.IsEnabled = false;
                    btnEsborrarEntrada.IsEnabled = false;
                    btnEditarEntrada.IsEnabled = false;
                }

                if (dgrUsuaris.SelectedItem != null)
                {
                    btnNouProjecte.IsEnabled = false;
                    btnEsborrarProjecte.IsEnabled = false;
                    btnEditarProjecte.IsEnabled = false;

                    btnAssignarUsuari.IsEnabled = false;
                    btnDesassignarUsuari.IsEnabled = true;

                    btnNovaTasca.IsEnabled = false;
                    btnEsborrarTasca.IsEnabled = false;
                    btnEditarTasca.IsEnabled = false;

                    btnNovaEntrada.IsEnabled = false;
                    btnEsborrarEntrada.IsEnabled = false;
                    btnEditarEntrada.IsEnabled = false;
                }
                if (dgrTasques.SelectedItem != null)
                {
                    btnNouProjecte.IsEnabled = false;
                    btnEsborrarProjecte.IsEnabled = false;
                    btnEditarProjecte.IsEnabled = false;

                    btnAssignarUsuari.IsEnabled = false;
                    btnDesassignarUsuari.IsEnabled = false;

                    btnNovaTasca.IsEnabled = false;
                    btnEsborrarTasca.IsEnabled = true;
                    btnEditarTasca.IsEnabled = true;

                    btnNovaEntrada.IsEnabled = true;
                    btnEsborrarEntrada.IsEnabled = false;
                    btnEditarEntrada.IsEnabled = false;
                }
                if (dgrEntrades.SelectedItem != null)
                {
                    btnNouProjecte.IsEnabled = false;
                    btnEsborrarProjecte.IsEnabled = false;
                    btnEditarProjecte.IsEnabled = false;

                    btnAssignarUsuari.IsEnabled = false;
                    btnDesassignarUsuari.IsEnabled = false;

                    btnNovaTasca.IsEnabled = false;
                    btnEsborrarTasca.IsEnabled = false;
                    btnEditarTasca.IsEnabled = false;

                    btnNovaEntrada.IsEnabled = false;
                    btnEsborrarEntrada.IsEnabled = true;
                    btnEditarEntrada.IsEnabled = true;
                }

            } 
            else if (estat == Estat.ALTA_PROJECTE)
            {

            }
            else if (estat == Estat.MODIFICACIO_PROJECTE)
            {

            }
            else if (estat == Estat.ASSIGNACIO_USUARI)
            {

            }
            else if (estat == Estat.ALTA_TASCA)
            {

            }
            else if (estat == Estat.MODIFICACIO_TASCA)
            {

            }
            else if (estat == Estat.ALTA_ENTRADA)
            {

            }
            else if (estat == Estat.MODIFICACIO_ENTRADA)
            {

            }


        }

        private void dgrProjectes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrProjectes.SelectedItem != null)
            {
                Projecte proj = (Projecte)dgrProjectes.SelectedItem;
                dgrUsuaris.ItemsSource = componentDB.GetLlistaUsuarisAssignats(proj.Id);
                dgrTasques.ItemsSource = componentDB.GetLlistaTasques(proj.Id);
                dgrEntrades.ItemsSource = null;
                canviEstat(Estat.VIEW);
            }
        }

        private void dgrUsuaris_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrUsuaris.SelectedItem != null)
            {
                canviEstat(Estat.VIEW);
            }
            
        }

        private void dgrTasques_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrTasques.SelectedItem != null && dgrProjectes.SelectedItem != null)
            {
                Tasca tasc = (Tasca)dgrTasques.SelectedItem;
                dgrEntrades.ItemsSource = componentDB.GetLlistaEntrades(tasc.Id);
                canviEstat(Estat.VIEW);
            }
        }

        private void dgrEntrades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrEntrades.SelectedItem != null)
            {
                canviEstat(Estat.VIEW);
            }
        }

        private void btnNouProjecte_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEsborrarProjecte_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditarProjecte_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAssignarUsuari_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDesassignarUsuari_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNovaTasca_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEsborrarTasca_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNovaEntrada_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEsborrarEntrada_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditarEntrada_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

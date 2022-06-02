using DB_MySQL;
using Microsoft.Toolkit.Uwp.UI.Controls;
using ModelGestioProjectes;
using System;
using System.Collections.Generic;
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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace GestioDeProjectesITasques.View
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class GestioProjectesPage : Page
    {
        ComponentDB componentDB = new ComponentDB();
        EnumEstats estat = EnumEstats.VIEW;

        public GestioProjectesPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dgrProjectes.ItemsSource = componentDB.GetLlistaProjectes();
            canviEstat(EnumEstats.VIEW);
        }

        private void canviEstat (EnumEstats estatNou)
        {
            estat = estatNou;

            if (estat == EnumEstats.VIEW)
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
        }

        private void dgrProjectes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrProjectes.SelectedItem != null)
            {
                Projecte proj = (Projecte)dgrProjectes.SelectedItem;
                dgrUsuaris.ItemsSource = componentDB.GetLlistaUsuarisAssignatsAmbRol(proj.Id);
                dgrTasques.ItemsSource = componentDB.GetLlistaTasques(proj.Id);
                dgrEntrades.ItemsSource = null;
                canviEstat(EnumEstats.VIEW);
            }
        }

        private void dgrUsuaris_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrUsuaris.SelectedItem != null)
            {
                dgrTasques.SelectedIndex = -1;
                dgrEntrades.ItemsSource = null;
                canviEstat(EnumEstats.VIEW);
            }
            
        }

        private void dgrTasques_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrTasques.SelectedItem != null && dgrProjectes.SelectedItem != null)
            {
                dgrUsuaris.SelectedIndex = -1;
                Tasca tasc = (Tasca)dgrTasques.SelectedItem;
                dgrEntrades.ItemsSource = componentDB.GetLlistaEntrades(tasc.Id);
                canviEstat(EnumEstats.VIEW);
            }
        }

        private void dgrEntrades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrEntrades.SelectedItem != null)
            {
                canviEstat(EnumEstats.VIEW);
            }
        }

        private async void btnNouProjecte_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog cpt = new ContentDialogProjecte
            {
                ElProjecte = null,
                ElEstat = EnumEstats.ALTA_PROJECTE
            };
            ContentDialogResult result = await cpt.ShowAsync();
            dgrProjectes.ItemsSource = componentDB.GetLlistaProjectes();
            canviEstat(EnumEstats.VIEW);
        }

        private void btnEsborrarProjecte_Click(object sender, RoutedEventArgs e)
        {
            Projecte proj = (Projecte)dgrProjectes.SelectedItem;
            componentDB.deleteProjecte(proj.Id);
            dgrProjectes.ItemsSource = componentDB.GetLlistaProjectes();
            canviEstat(EnumEstats.VIEW);
        }

        private async void btnEditarProjecte_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog cpt = new ContentDialogProjecte
            {
                ElProjecte = (Projecte)dgrProjectes.SelectedItem,
                ElEstat = EnumEstats.MODIFICACIO_PROJECTE
            };
            ContentDialogResult result = await cpt.ShowAsync();
            dgrProjectes.ItemsSource = componentDB.GetLlistaProjectes();
            canviEstat(EnumEstats.VIEW);

        }

        private async void btnAssignarUsuari_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog cpu = new ContentDialogUsuari
            {
                IdProjecte = ((Projecte)dgrProjectes.SelectedItem).Id,
                ElEstat = EnumEstats.MODIFICACIO_PROJECTE
            };
            ContentDialogResult result = await cpu.ShowAsync();
            dgrUsuaris.ItemsSource = componentDB.GetLlistaUsuarisAssignatsAmbRol(((Projecte)dgrProjectes.SelectedItem).Id);
            canviEstat(EnumEstats.VIEW);
        }

        private void btnDesassignarUsuari_Click(object sender, RoutedEventArgs e)
        {
            Projecte proj = (Projecte)dgrProjectes.SelectedItem;
            Usuari usu = ((ProjecteUsuariRol)dgrUsuaris.SelectedItem).Usuari;
            componentDB.desassignarUsuari(proj.Id, usu.Id);
            dgrUsuaris.ItemsSource = componentDB.GetLlistaUsuarisAssignatsAmbRol(((Projecte)dgrProjectes.SelectedItem).Id);
            canviEstat(EnumEstats.VIEW);
        }

        private async void btnNovaTasca_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog cpt = new ContentDialogTasca
            {
                IdProjecte = ((Projecte)dgrProjectes.SelectedItem).Id,
                LaTasca = null,
                ElEstat = EnumEstats.ALTA_TASCA
            };
            ContentDialogResult result = await cpt.ShowAsync();
            dgrTasques.ItemsSource = componentDB.GetLlistaTasques(((Projecte)dgrProjectes.SelectedItem).Id);
            dgrEntrades.ItemsSource = null;
            canviEstat(EnumEstats.VIEW);
        }

        private async void btnEsborrarTasca_Click(object sender, RoutedEventArgs e)
        {
            if (componentDB.GetLlistaEntrades(((Projecte)dgrProjectes.SelectedItem).Id).Count > 0)
            {
                ContentDialog cpte = new ContentDialogTascaEsborrar
                {
                    IdTasca = ((Tasca)dgrTasques.SelectedItem).Id
                };
                ContentDialogResult result = await cpte.ShowAsync();
            } 
            else
            {
                componentDB.deleteTasca(((Projecte)dgrProjectes.SelectedItem).Id);
            }

            dgrTasques.ItemsSource = componentDB.GetLlistaTasques(((Projecte)dgrProjectes.SelectedItem).Id);
            dgrEntrades.ItemsSource = null;
            canviEstat(EnumEstats.VIEW);
        }

        private async void btnEditarTasca_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog cpt = new ContentDialogTasca
            {
                IdProjecte = ((Projecte)dgrProjectes.SelectedItem).Id,
                LaTasca = (Tasca)dgrTasques.SelectedItem,
                ElEstat = EnumEstats.MODIFICACIO_TASCA
            };
            ContentDialogResult result = await cpt.ShowAsync();
            dgrTasques.ItemsSource = componentDB.GetLlistaTasques(((Projecte)dgrProjectes.SelectedItem).Id);
            dgrEntrades.ItemsSource = null;
            canviEstat(EnumEstats.VIEW);
        }

        private async void btnNovaEntrada_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog cpe = new ContentDialogEntrada
            {
                IdTasca = ((Tasca)dgrTasques.SelectedItem).Id,
                LaEntrada = null,
                ElEstat = EnumEstats.ALTA_ENTRADA
            };
            ContentDialogResult result = await cpe.ShowAsync();
            dgrEntrades.ItemsSource = componentDB.GetLlistaEntrades(((Tasca)dgrTasques.SelectedItem).Id);
            canviEstat(EnumEstats.VIEW);
        }

        private void btnEsborrarEntrada_Click(object sender, RoutedEventArgs e)
        {
            componentDB.deleteEntrada(((Tasca)dgrTasques.SelectedItem).Id, ((Entrada)dgrEntrades.SelectedItem).Numero);
            dgrEntrades.ItemsSource = componentDB.GetLlistaEntrades(((Tasca)dgrTasques.SelectedItem).Id);
            canviEstat(EnumEstats.VIEW);
        }

        private async void btnEditarEntrada_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog cpe = new ContentDialogEntrada
            {
                IdTasca = ((Tasca)dgrTasques.SelectedItem).Id,
                LaEntrada = (Entrada)dgrEntrades.SelectedItem,
                ElEstat = EnumEstats.MODIFICACIO_ENTRADA
            };
            ContentDialogResult result = await cpe.ShowAsync();
            dgrEntrades.ItemsSource = componentDB.GetLlistaEntrades(((Tasca)dgrTasques.SelectedItem).Id);
            canviEstat(EnumEstats.VIEW);
        }

        
    }
}

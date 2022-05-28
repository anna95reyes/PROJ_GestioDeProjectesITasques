using ModelGestioProjectes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GestorProjectes
{
    public interface IGestioProjectes
    {
        ObservableCollection<Projecte> GetLlistaProjectes();
        Projecte GetProjecte(int idProjecte);
        ObservableCollection<Usuari> GetLlistaUsuarisAssignats(int idProjecte);
        ObservableCollection<Usuari> GetLlistaUsuarisNoAssignats(int idProjecte);
        Usuari GetUsuari(int idUsuari);
        ObservableCollection<Tasca> GetLlistaTasques(int idProjecte);
        Tasca GetTasca(int idTasca);
        ObservableCollection<Entrada> GetLlistaEntrades(int idTasca);
        Entrada GetEntrada(int idTasca, int idNumeracio);
        void addProjecte(Projecte projecte);
        void updateProjecte(Projecte projecte);
        void deleteProjecte(int idProjecte);
        void assignarUsuari(int idUsuari);
        void desassignarUsuari(int idUsuari);
        void addTasca(Tasca tasca);
        void updateTasca(Tasca tasca);
        void deleteTasca(int idTasca);
        void addTEntrada(int idTasca, Entrada entrada);
        void updateEntrada(int idTasca, Entrada entrada);
        void deleteEntrada(int idEntrada);


    }
}

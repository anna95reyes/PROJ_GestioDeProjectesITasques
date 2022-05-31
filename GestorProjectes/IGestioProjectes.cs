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
        ObservableCollection<Usuari> GetLlistaUsuaris();
        ObservableCollection<ProjecteUsuariRol> GetLlistaUsuarisAssignatsAmbRol(int idProjecte);
        ObservableCollection<Usuari> GetLlistaUsuarisAssignats(int idProjecte);
        ObservableCollection<Usuari> GetLlistaUsuarisNoAssignats(int idProjecte);
        Usuari GetUsuari(int idUsuari);
        ObservableCollection<Tasca> GetLlistaTasques(int idProjecte);
        Tasca GetTasca(int idTasca);
        ObservableCollection<Entrada> GetLlistaEntrades(int idTasca);
        Entrada GetEntrada(int idTasca, int idNumeracio);
        ObservableCollection<Estat> GetLlistaEstats();
        Estat GetEstat(int idEstat);
        ObservableCollection<Rol> GetLlistaRols();
        Rol GetRol(int idRol);
        void addProjecte(Projecte projecte);
        void updateProjecte(Projecte projecte);
        void deleteProjecte(int idProjecte);
        void assignarUsuari(int idProjecte, int idUsuari, int idRol);
        void desassignarUsuari(int idProjecte, int idUsuari);
        void addTasca(Tasca tasca, int idProjecte);
        void updateTasca(Tasca tasca);
        void deleteTasca(int idTasca);
        void addEntrada(int idTasca, Entrada entrada);
        void updateEntrada(int idTasca, Entrada entrada);
        void deleteEntrada(int idTasca, int numeroEntrada);


    }
}

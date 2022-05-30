using GestioProjectes_DB;
using GestorProjectes;
using ModelGestioProjectes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DB_MySQL
{
    public class ComponentDB : IGestioProjectes
    {
        public ObservableCollection<Projecte> GetLlistaProjectes()
        {
            return ProjecteDB.GetLlistaProjectes();
        }
        public Projecte GetProjecte(int idProjecte)
        {
            return ProjecteDB.GetProjecte(idProjecte);
        }


        public ObservableCollection<Usuari> GetLlistaUsuaris()
        {
            return UsuariDB.GetLlistaUsuaris();
        }

        public ObservableCollection<Usuari> GetLlistaUsuarisAssignats(int idProjecte)
        {
            return UsuariDB.GetLlistaUsuarisAssignats(idProjecte);
        }

        public ObservableCollection<Usuari> GetLlistaUsuarisNoAssignats(int idProjecte)
        {
            return UsuariDB.GetLlistaUsuarisNoAssignats(idProjecte);
        }

        public Usuari GetUsuari(int idUsuari)
        {
            return UsuariDB.GetUsuari(idUsuari);
        }

        public ObservableCollection<Tasca> GetLlistaTasques(int idProjecte)
        {
            return TascaDB.GetLlistaTasques(idProjecte);
        }

        public Tasca GetTasca(int idTasca)
        {
            return TascaDB.GetTasca(idTasca);
        }

        public ObservableCollection<Entrada> GetLlistaEntrades(int idTasca)
        {
            return EntradaDB.GetLlistaEntrades(idTasca);
        }

        public Entrada GetEntrada(int idTasca, int idNumeracio)
        {
            return EntradaDB.GetEntrada(idTasca, idNumeracio);
        }

        public Estat GetEstat(int idEstat)
        {
            return EstatDB.GetEstat(idEstat);
        }

        public void addProjecte(Projecte projecte)
        {
            ProjecteDB.addProjecte(projecte);
        }

        public void updateProjecte(Projecte projecte)
        {
            ProjecteDB.updateProjecte(projecte);
        }

        public void deleteProjecte(int idProjecte)
        {
            ProjecteDB.deleteProjecte(idProjecte);
        }

        public void assignarUsuari(int idProjecte, int idUsuari, int idRol)
        {
            ProjecteUsuariRolDB.assignarUsuari(idProjecte, idUsuari, idRol);
        }

        public void desassignarUsuari(int idProjecte, int idUsuari)
        {
            ProjecteUsuariRolDB.desassignarUsuari(idProjecte, idUsuari);
        }

        public void addTasca(Tasca tasca, int idProjecte)
        {
            TascaDB.addTasca(tasca, idProjecte);
        }

        public void updateTasca(Tasca tasca)
        {
            TascaDB.updateTasca(tasca);
        }

        public void deleteTasca(int idTasca)
        {
            TascaDB.deleteTasca(idTasca);
        }

        public void addEntrada(int idTasca, Entrada entrada)
        {
            EntradaDB.addEntrada(idTasca, entrada);
        }

        public void updateEntrada(int idTasca, Entrada entrada)
        {
            EntradaDB.updateEntrada(idTasca, entrada);
        }

        public void deleteEntrada(int idTasca, int numeroEntrada)
        {
            EntradaDB.deleteEntrada(idTasca, numeroEntrada);
        }

    }
}

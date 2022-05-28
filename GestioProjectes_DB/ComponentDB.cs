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
            throw new NotImplementedException();
        }

        public Tasca GetTasca(int idTasca)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Entrada> GetLlistaEntrades(int idTasca)
        {
            throw new NotImplementedException();
        }

        public Entrada GetEntrada(int idTasca, int idNumeracio)
        {
            throw new NotImplementedException();
        }

        public void addProjecte(Projecte projecte)
        {
            throw new NotImplementedException();
        }

        public void updateProjecte(Projecte projecte)
        {
            throw new NotImplementedException();
        }

        public void deleteProjecte(int idProjecte)
        {
            throw new NotImplementedException();
        }

        public void assignarUsuari(int idUsuari)
        {
            throw new NotImplementedException();
        }

        public void desassignarUsuari(int idUsuari)
        {
            throw new NotImplementedException();
        }

        public void addTasca(Tasca tasca)
        {
            throw new NotImplementedException();
        }

        public void updateTasca(Tasca tasca)
        {
            throw new NotImplementedException();
        }

        public void deleteTasca(int idTasca)
        {
            throw new NotImplementedException();
        }

        public void addTEntrada(int idTasca, Entrada entrada)
        {
            throw new NotImplementedException();
        }

        public void updateEntrada(int idTasca, Entrada entrada)
        {
            throw new NotImplementedException();
        }

        public void deleteEntrada(int idEntrada)
        {
            throw new NotImplementedException();
        }

    }
}

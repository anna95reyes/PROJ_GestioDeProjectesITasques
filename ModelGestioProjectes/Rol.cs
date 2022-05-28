using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ModelGestioProjectes
{
    public class Rol
    {
        private int id;
        private String nom;

        private ObservableCollection<ProjecteUsuariRol> usuarisProjectes = new ObservableCollection<ProjecteUsuariRol>();

        public Rol(int id, string nom)
        {
            Id = id;
            Nom = nom;
        }

        public int Id {
            get
            {
                return id;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("La id es positiva");
                }
                id = value;
            }
        }
        public string Nom {
            get
            {
                return nom;
            }
            set
            {
                if (value == null || value.Length <= 0)
                {
                    throw new Exception("El nom es obligatori i no buit");
                }
                nom = value;
            }
        }


        public ObservableCollection<ProjecteUsuariRol> getUsuarisProjectes()
        {
            return usuarisProjectes;
        }

        public void addUsuariProjectes(ProjecteUsuariRol usuariRol)
        {
            if (usuariRol == null)
            {
                throw new Exception("Intent d'afegir un usuariRol null");
            }
            if (usuariRol.Rol == null)
            {
                if (!this.usuarisProjectes.Contains(usuariRol))
                {
                    usuarisProjectes.Add(usuariRol);
                }
            }
        }

        public void removeUsuariProjectes(ProjecteUsuariRol usuariRol)
        {
            if (usuarisProjectes.Contains(usuariRol))
            {
                usuarisProjectes.Remove(usuariRol);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Rol rol &&
                   Id == rol.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return "Rol{" + "id=" + id + ", nom=" + nom + ", usuarisProjectes=" + usuarisProjectes + '}';
        }
    }
}

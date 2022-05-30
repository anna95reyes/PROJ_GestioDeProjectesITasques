using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ModelGestioProjectes
{
    public class Projecte
    {
        private int id;
        private String nom;
        private String descripcio;

        private Usuari capProjecte;
        private ObservableCollection<Tasca> tasques = new ObservableCollection<Tasca>();
        private ObservableCollection<ProjecteUsuariRol> usuarisRol = new ObservableCollection<ProjecteUsuariRol>();

        public Projecte(int id, string nom, Usuari capProjecte)
        {
            Id = id;
            Nom = nom;
            CapProjecte = capProjecte;
        }

        public Projecte(int id, string nom, string descripcio, Usuari capProjecte)
        {
            Id = id;
            Nom = nom;
            Descripcio = descripcio;
            CapProjecte = capProjecte;
        }

        public Projecte(int id, string nom, Usuari capProjecte, ObservableCollection<Tasca> tasques, ObservableCollection<ProjecteUsuariRol> usuarisRol)
        {
            Id = id;
            Nom = nom;
            CapProjecte = capProjecte;
            this.tasques = tasques;
            this.usuarisRol = usuarisRol;
        }

        public Projecte(int id, string nom, string descripcio, Usuari capProjecte, ObservableCollection<Tasca> tasques, ObservableCollection<ProjecteUsuariRol> usuarisRol)
        {
            this.id = id;
            this.nom = nom;
            this.descripcio = descripcio;
            this.capProjecte = capProjecte;
            this.tasques = tasques;
            this.usuarisRol = usuarisRol;
        }

        public int Id {
            get
            {
                return id;
            }
            set
            {
                if (!validaId(value))
                {
                    throw new Exception("La id es positiva");
                }
                id = value;
            }
        }

        public static bool validaId(int value)
        {
            return !(value <= 0);
        }

        public string Nom {
            get
            {
                return nom;
            }
            set
            {
                if (!validaNom(value))
                {
                    throw new Exception("El nom es obligatori i no buit");
                }
                nom = value;
            }
        }

        public static bool validaNom(string value)
        {
            return !(value.Length <= 0);
        }

        public string Descripcio {
            get
            {
                return descripcio;
            }
            set
            {
                if (!validaDescripcio(value))
                {
                    throw new Exception("La descripcio es nula o amb contingut");
                }
                descripcio = value;
            }
        }

        public static bool validaDescripcio(string value)
        {
            return !(value != null && value.Length <= 0);
        }

        public Usuari CapProjecte {
            get
            {
                return capProjecte;
            }
            set
            {
                if (!validaCapProjecte(value))
                {
                    throw new Exception("El cap del projecte es obligatori");
                }
                capProjecte = value;
            }
        }

        public static bool validaCapProjecte(Usuari value)
        {
            return value != null;
        }

        public String NomCapProjecte
        {
            get
            {
                return CapProjecte.Cognom1 + (CapProjecte.Cognom2 != null ? " " + CapProjecte.Cognom2 : "") + ", " + CapProjecte.Nom;
            }
        }
        public ObservableCollection<Tasca> getTasques()
        {
            return tasques;
        }

        public void addTasca(Tasca tasca)
        {
            if (tasca == null)
            {
                throw new Exception("Intent d'afegir una tasca nulla");
            }
            tasques.Add(tasca);
        }

        public void removeTasca(Tasca tasca)
        {
            if (tasques.Contains(tasca))
            {
                tasques.Remove(tasca);
            }
        }

        public ObservableCollection<ProjecteUsuariRol> getUsuarisRol()
        {
            return usuarisRol;
        }

        public void addUsuariRol(ProjecteUsuariRol usuariRol)
        {
            if (usuariRol == null)
            {
                throw new Exception("Intent d'afegir un usuariRol null");
            }
            if (usuariRol.Projecte == null)
            {
                if (!this.usuarisRol.Contains(usuariRol))
                {
                    usuarisRol.Add(usuariRol);
                }
            }
        }

        public void removeUsuariRol(ProjecteUsuariRol usuariRol)
        {
            if (usuarisRol.Contains(usuariRol))
            {
                usuarisRol.Remove(usuariRol);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Projecte projecte &&
                   Id == projecte.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return "Projecte{" + "id=" + id + ", nom=" + nom + ", descripcio=" + descripcio + ", capProjecte=" + capProjecte + ", tasques=" + tasques + ", usuaris=" + usuarisRol + '}';
        }
    }
}

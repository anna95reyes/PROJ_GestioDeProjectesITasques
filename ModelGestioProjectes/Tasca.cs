using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ModelGestioProjectes
{
    public class Tasca
    {
        private int id;
        private DateTime dataCreacio;
        private String nom;
        private String descripcio;
        private DateTime dataLimit;

        private Usuari propietari;
        private Usuari responsable;
        private Estat estat;
        private ObservableCollection<Entrada> entrades = new ObservableCollection<Entrada>();

        public Tasca(int id, DateTime dataCreacio, string nom, Usuari propietari, Estat estat)
        {
            Id = id;
            DataCreacio = dataCreacio;
            Nom = nom;
            Propietari = propietari;
            this.estat = estat;
        }

        public Tasca(int id, DateTime dataCreacio, string nom, string descripcio, DateTime dataLimit, Usuari propietari, Usuari responsable, Estat estat)
        {
            Id = id;
            DataCreacio = dataCreacio;
            Nom = nom;
            Descripcio = descripcio;
            DataLimit = dataLimit;
            Propietari = propietari;
            Responsable = responsable;
            this.estat = estat;
        }

        public Tasca(int id, DateTime dataCreacio, string nom, string descripcio, DateTime dataLimit, Usuari propietari, Usuari responsable, Estat estat, ObservableCollection<Entrada> entrades)
        {
            Id = id;
            DataCreacio = dataCreacio;
            Nom = nom;
            Descripcio = descripcio;
            DataLimit = dataLimit;
            Propietari = propietari;
            Responsable = responsable;
            this.estat = estat;
            this.entrades = entrades;
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
        public DateTime DataCreacio {
            get
            {
                return dataCreacio;
            }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new Exception("La data de creacio ha de ser anterior a la data actual");
                }
                dataCreacio = value;
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
        public string Descripcio {
            get
            {
                return descripcio;
            }
            set
            {
                if (value != null && value.Length <= 0)
                {
                    throw new Exception("La descripcio es nula o amb contingut");
                }
                descripcio = value;
            }
        }
        public DateTime DataLimit {
            get
            {
                return dataLimit;
            }

            set
            {
                if (value != null && value < dataCreacio)
                {
                    throw new Exception("La data limit es nula o posterior a la data de creacio");
                }
                dataLimit = value;
            }
        }
        public Usuari Propietari {
            get
            {
                return propietari;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("L'usuari que ha creat la tasca es obligatori");
                }
                propietari = value;
            }
        }
        public Usuari Responsable {
            get
            {
                return responsable;
            }
            set
            {
                responsable = value;
            }
        }

        /*
            No faig el set perque considero que l'estat s'ha d'entrar desde el constructor,
            i en cas de que es volgues cambiar d'estat, s'ha de generar una nova entrada per fer-ho,
            per tant, no s'ha de poder cambiar l'estat desde la tasca.
        */
        public Estat Estat {
            get
            {
                return estat;
            }
        }

        public ObservableCollection<Entrada> getEntrades()
        {
            return entrades;
        }

        public void addEntrada(Entrada entrada)
        {
            if (entrades == null)
            {
                throw new Exception("Intent d'afegir un projecte null");
            }
            if (entrada.NouEstat != null)
            {
                if (entrada.NouEstat.Equals(estat))
                {
                    throw new Exception("El nou estat no pot ser el mateix que l'estat actual");
                }
            }

            entrades.Add(entrada);
        }

        public void removeEntrada(Entrada entrada)
        {
            if (entrades.Contains(entrada))
            {
                entrades.Remove(entrada);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Tasca tasca &&
                   Id == tasca.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return "Tasca{" + "id=" + id + ", dataCreacio=" + dataCreacio + ", nom=" + nom + ", descripcio=" + descripcio + ", dataLimit=" + dataLimit + ", propietari=" + propietari + ", responsable=" + responsable + ", estat=" + estat + ", entrades=" + entrades + '}';
        }
    }
}

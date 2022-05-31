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
        private DateTime? dataLimit;

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

        public Tasca(int id, DateTime dataCreacio, string nom, string descripcio, DateTime? dataLimit, Usuari propietari, Usuari responsable, Estat estat)
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

        public Tasca(int id, DateTime dataCreacio, string nom, string descripcio, DateTime? dataLimit, Usuari propietari, Usuari responsable, Estat estat, ObservableCollection<Entrada> entrades)
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

        public DateTime DataCreacio {
            get
            {
                return dataCreacio;
            }
            set
            {
                if (!validaDataCreacio(value))
                {
                    throw new Exception("La data de creacio ha de ser anterior a la data actual");
                }
                dataCreacio = value;
            }
        }

        public static bool validaDataCreacio(DateTime value)
        {
            return !(value > DateTime.Now);
        }

        public String DataCreacioFormatada
        {
            get
            {
                return DataCreacio.ToString("dd/MM/yyyy");
            }
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
            return !(value == null || value.Length <= 0);
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

        public DateTime? DataLimit {
            get
            {
                return dataLimit;
            }

            set
            {
                if (!validaDataLimit(value))
                {
                    throw new Exception("La data limit es nula o posterior a la data de creacio");
                }
                dataLimit = value;
            }
        }

        public bool validaDataLimit(DateTime? value)
        {
            return !(value != null && value < dataCreacio);
        }

        public String DataLimitFormatada
        {
            get
            {
                return DataLimit != null? ((DateTime)DataLimit).ToString("dd/MM/yyyy") : "";
            }
        }
        public Usuari Propietari {
            get
            {
                return propietari;
            }
            set
            {
                if (!validaPropietari(value))
                {
                    throw new Exception("L'usuari que ha creat la tasca es obligatori");
                }
                propietari = value;
            }
        }

        public static bool validaPropietari(Usuari value)
        {
            return value != null;
        }

        public String NomPropietari
        {
            get
            {
                return Propietari.Cognom1 + (Propietari.Cognom2 != null ? " " + Propietari.Cognom2 : "") + ", " + Propietari.Nom;
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
        public String NomResponsable
        {
            get
            {
                return Responsable != null? Responsable.Cognom1 + (Responsable.Cognom2 != null ? " " + Responsable.Cognom2 : "") + ", " + Responsable.Nom : "";
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
            set
            {
                estat = value;
            }
        }
        public String EstatFormatat
        {
            get
            {
                return Estat != null ? Estat.Nom : "";
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

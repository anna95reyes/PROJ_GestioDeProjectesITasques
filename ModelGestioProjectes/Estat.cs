using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGestioProjectes
{
    public class Estat
    {
        private int id;
        private String nom;

        public Estat(int id, string nom)
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

        public override bool Equals(object obj)
        {
            return obj is Estat estat &&
                   Id == estat.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}

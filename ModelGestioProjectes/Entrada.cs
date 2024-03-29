﻿using System;

namespace ModelGestioProjectes
{
    public class Entrada
    {
        private int numero;
        private DateTime data;
        private String entrada;

        private Usuari escriptor;
        private Usuari novaAssignacio;
        private Estat nouEstat;

        public Entrada(int numero, DateTime data, string entrada, Usuari escriptor)
        {
            Numero = numero;
            Data = data;
            _Entrada = entrada;
            Escriptor = escriptor;
        }

        public Entrada(int numero, DateTime data, string entrada, Usuari escriptor, Usuari novaAssignacio, Estat nouEstat)
        {
            Numero = numero;
            Data = data;
            _Entrada = entrada;
            Escriptor = escriptor;
            NovaAssignacio = novaAssignacio;
            NouEstat = nouEstat;
        }

        public int Numero {
            get
            {
                return numero;
            }
            set
            {
                if (!validaNumero(value))
                {
                    throw new Exception("El numero es positiu");
                }
                numero = value;
            }
        }

        public static bool validaNumero(int value)
        {
            return !(value <= 0);
        }

        public DateTime Data {
            get
            {
                return data;
            }
            set
            {
                if (!validaData(value))
                {
                    throw new Exception("La data ha de ser anterior a la data actual");
                }
                data = value;
            }
        }

        private static bool validaData(DateTime value)
        {
            return !(value > DateTime.Now);
        }

        public String DataFormatada
        {
            get
            {
                return Data.ToString("dd/MM/yyyy");
            }
        }

        public String _Entrada {
            get
            {
                return entrada;
            }
            set
            {
                if (!validaEntrada(value))
                {
                    throw new Exception("La entrada es obligatoria i no nula");
                }
                entrada = value;
            }
        }

        public static bool validaEntrada(string value)
        {
            return !(value == null || value.Length <= 0);
        }

        public Usuari Escriptor {
            get
            {
                return escriptor;
            }
            set
            {
                if (!validaEscriptor(value))
                {
                    throw new Exception("L'escriptor es obligatori");
                }
                escriptor = value;
            }
        }

        public static bool validaEscriptor(Usuari value)
        {
            return value != null;
        }

        public String NomEscriptor
        {
            get
            {
                return Escriptor.Cognom1 + (Escriptor.Cognom2 != null ? " " + Escriptor.Cognom2 : "") + ", " + Escriptor.Nom;
            }
        }
        public Usuari NovaAssignacio {
            get
            {
                return novaAssignacio;
            }
            set
            {
                if (!validaNovaAssignacio(value))
                {
                    throw new Exception("No es pot tornar a assignar al mateix usuari");
                }
                novaAssignacio = value;
            }
        }

        public bool validaNovaAssignacio(Usuari value)
        {
            return !(value != null && value.Equals(escriptor));
        }

        public String NomNovaAssignacio
        {
            get
            {
                return NovaAssignacio != null? NovaAssignacio.Cognom1 + (NovaAssignacio.Cognom2 != null ? " " + NovaAssignacio.Cognom2 : "") + ", " + NovaAssignacio.Nom : "";
            }
        }
        public Estat NouEstat {
            get
            {
                return nouEstat;
            }
            set
            {
                nouEstat = value;
            }
        }
        public String NouEstatFormatat
        {
            get
            {
                return NouEstat != null ? NouEstat.Nom : "";
            }
        }
        public override bool Equals(object obj)
        {
            return obj is Entrada entrada &&
                   Numero == entrada.Numero;
        }

        public override int GetHashCode()
        {
            return -1449941195 + Numero.GetHashCode();
        }

        public override string ToString()
        {
            return "Entrada{" + "numero=" + numero + ", data=" + data + ", entrada=" + entrada + ", escriptor=" + escriptor + ", novaAssignacio=" + novaAssignacio + ", nouEstat=" + nouEstat + '}';
        }

    }
}

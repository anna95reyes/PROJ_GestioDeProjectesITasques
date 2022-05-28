using System;

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
                if (value <= 0)
                {
                    throw new Exception("El numero es positiu");
                }
                numero = value;
            }
        }
        public DateTime Data {
            get
            {
                return data;
            }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new Exception("La data ha de ser anterior a la data actual");
                }
                data = value;
            }
        }
        public String _Entrada {
            get
            {
                return entrada;
            }
            set
            {
                if (value == null || value.Length <= 0)
                {
                    throw new Exception("La entrada es obligatoria i no nula");
                }
                entrada = value;
            }
        }
        public Usuari Escriptor {
            get
            {
                return escriptor;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("L'escriptor es obligatori");
                }
                escriptor = value;
            } 
        }
        public Usuari NovaAssignacio {
            get
            {
                return novaAssignacio;
            }
            set
            {
                if (value != null && value.Equals(escriptor))
                {
                    throw new Exception("No es pot tornar a assignar al mateix usuari");
                }
                novaAssignacio = value;
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

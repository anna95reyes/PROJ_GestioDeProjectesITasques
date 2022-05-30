using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ModelGestioProjectes
{
    public class Usuari
    {
        private int id;
        private String nom;
        private String cognom1;
        private String cognom2;
        private DateTime dataNaixement;
        private String login;
        private String passwordHash;

        private ObservableCollection<ProjecteUsuariRol> projectesRol = new ObservableCollection<ProjecteUsuariRol>();
        private ObservableCollection<Tasca> tasquesAssignades = new ObservableCollection<Tasca>();

        public Usuari(int id, string nom, string cognom1, DateTime dataNaixement, string login, string passwordHash)
        {
            Id = id;
            Nom = nom;
            Cognom1 = cognom1;
            DataNaixement = dataNaixement;
            Login = login;
            PasswordHash = passwordHash;
        }

        public Usuari(int id, string nom, string cognom1, string cognom2, DateTime dataNaixement, string login, string passwordHash)
        {
            Id = id;
            Nom = nom;
            Cognom1 = cognom1;
            Cognom2 = cognom2;
            DataNaixement = dataNaixement;
            Login = login;
            PasswordHash = passwordHash;
        }

        public Usuari(int id, string nom, string cognom1, string cognom2, DateTime dataNaixement, string login, string passwordHash, ObservableCollection<ProjecteUsuariRol> projectesRol, ObservableCollection<Tasca> tasquesAssignades)
        {
            Id = id;
            Nom = nom;
            Cognom1 = cognom1;
            Cognom2 = cognom2;
            DataNaixement = dataNaixement;
            Login = login;
            PasswordHash = passwordHash;
            this.projectesRol = projectesRol;
            this.tasquesAssignades = tasquesAssignades;
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
                if (!comprobarDadesObligatories(value))
                {
                    throw new Exception("El nom es obligatori i no buit");
                }
                nom = value;
            }
        }
        public string Cognom1 {
            get
            {
                return cognom1;
            }
            set
            {
                if (!comprobarDadesObligatories(value))
                {
                    throw new Exception("El cognom1 es obligatori i no buit");
                }
                cognom1 = value;
            }
        }
        public string Cognom2 {
            get
            {
                return cognom2;
            }
            set
            {
                if (!comprobarDadesOpcionals(value))
                {
                    throw new Exception("El cognom2 es null o amb contingut");
                }
                cognom2 = value;
            }
        }
        public DateTime DataNaixement {
            get
            {
                return dataNaixement;
            }
            set
            {
                if (comprobarDataNaixement(value))
                {
                    throw new Exception("La data de naixement ha de ser anterior a la data actual");
                }
                dataNaixement = value;
            }
        }
        public String DataNaixementFormatada
        {
            get
            {
                return DataNaixement.ToString("dd/MM/yyyy");
            }
        }
        public string Login {
            get
            {
                return login;
            }
            set
            {
                if (!comprobarDadesObligatories(value))
                {
                    throw new Exception("El login es obligatori i no buit");
                }
                login = value;
            }
        }
        public string PasswordHash {
            get
            {
                return passwordHash;
            }
            set
            {
                if (!comprobarDadesObligatories(value))
                {
                    throw new Exception("El passwordHash es obligatori i no buit");
                }
                passwordHash = value;
            }
        }

        public static Boolean comprobarDadesObligatories(String dada)
        {
            return !(dada == null || dada.Length <= 0);
        }

        public static Boolean comprobarDataNaixement(DateTime data)
        {
            return data > DateTime.Now;
        }

        public static Boolean comprobarDadesOpcionals(String dada)
        {
            return !(dada != null && dada.Length <= 0);
        }

        public override bool Equals(object obj)
        {
            return obj is Usuari usuari &&
                   Id == usuari.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return "Usuari{" + "id=" + id + ", nom=" + nom + ", cognom1=" + cognom1 + ", cognom2=" + cognom2 + ", dataNaixement=" + dataNaixement + ", login=" + login + ", passwordHash=" + passwordHash + ", projectes=" + projectesRol + ", tasques=" + tasquesAssignades + '}';
        }

    }
}

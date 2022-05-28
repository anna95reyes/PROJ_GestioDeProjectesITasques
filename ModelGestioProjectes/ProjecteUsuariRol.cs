using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGestioProjectes
{
    public class ProjecteUsuariRol
    {
        private Projecte projecte;
        private Usuari usuari;
        private Rol rol;

        public ProjecteUsuariRol(Projecte projecte, Usuari usuari, Rol rol)
        {
            Projecte = projecte;
            Usuari = usuari;
            Rol = rol;
        }

        public Projecte Projecte {
            get
            {
                return projecte;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("El projecte es obligatori");
                }
                projecte = value;
            }
        }
        public Usuari Usuari {
            get
            {
                return usuari;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("L'usuari es obligatori");
                }
                usuari = value;
            }
        }
        public Rol Rol {
            get
            {
                return rol;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("El rol es obligatori");
                }
                rol = value;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is ProjecteUsuariRol rol &&
                   EqualityComparer<Projecte>.Default.Equals(Projecte, rol.Projecte) &&
                   EqualityComparer<Usuari>.Default.Equals(Usuari, rol.Usuari);
        }

        public override int GetHashCode()
        {
            int hashCode = 998945955;
            hashCode = hashCode * -1521134295 + EqualityComparer<Projecte>.Default.GetHashCode(Projecte);
            hashCode = hashCode * -1521134295 + EqualityComparer<Usuari>.Default.GetHashCode(Usuari);
            return hashCode;
        }


        public override string ToString()
        {
            return "ProjecteUsuari{" + "projecte=" + projecte + ", usuari=" + usuari + ", rol=" + rol + '}';
        }
    }




}

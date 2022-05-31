using Microsoft.EntityFrameworkCore;
using ModelGestioProjectes;
using NBA_BD.db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DB_MySQL
{
    public class RolDB
    {
        public static ObservableCollection<Rol> GetLlistaRols()
        {
            ObservableCollection<Rol> rols = new ObservableCollection<Rol>();

            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {
                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    using (DbCommand consulta = connection.CreateCommand())
                    {

                        consulta.CommandText = $@"select rol_id, rol_nom from rol";
                        DbDataReader reader = consulta.ExecuteReader(); //per cuan pot retorna mes d'una fila

                        Dictionary<string, int> ordinals = new Dictionary<string, int>();
                        string[] cols = { "rol_id", "rol_nom"};
                        foreach (string c in cols)
                        {
                            ordinals[c] = reader.GetOrdinal(c);
                        }

                        while (reader.Read()) //llegeix la fila seguent, retorna true si ha pogut llegir la fila, retorna false si no hi ha mes dades per lleguir
                        {
                            int rol_id = reader.GetInt32(ordinals["rol_id"]);
                            string rol_nom = reader.GetString(ordinals["rol_nom"]);

                            Rol rol = new Rol(rol_id, rol_nom);
                            rols.Add(rol);
                        }

                    }
                }
            }
            return rols;
        }

        public static Rol GetRol(int idRol)
        {
            Rol rol = null;

            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {

                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        DBUtil.crearParametre(consulta, "@rol_id", idRol, DbType.Int32);

                        consulta.CommandText = $@"select rol_id, rol_nom from rol where rol_id = @rol_id";

                        DbDataReader reader = consulta.ExecuteReader(); //per cuan pot retorna mes d'una fila

                        Dictionary<string, int> ordinals = new Dictionary<string, int>();
                        string[] cols = { "rol_id", "rol_nom" };
                        foreach (string c in cols)
                        {
                            ordinals[c] = reader.GetOrdinal(c);
                        }

                        while (reader.Read()) //llegeix la fila seguent, retorna true si ha pogut llegir la fila, retorna false si no hi ha mes dades per lleguir
                        {
                            int rol_id = reader.GetInt32(ordinals["rol_id"]);
                            string rol_nom = reader.GetString(ordinals["rol_nom"]);

                            rol = new Rol(rol_id, rol_nom);
                        }


                    }
                }
            }

            return rol;
        }
    }
}

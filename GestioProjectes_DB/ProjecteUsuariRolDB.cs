using GestioProjectes_DB;
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
    public class ProjecteUsuariRolDB
    {
        public static void assignarUsuari(int idProjecte, int idUsuari, int idRol)
        {
            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {
                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    DbTransaction transaccio = connection.BeginTransaction(); //Creacio d'una transaccio

                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        consulta.Transaction = transaccio; // marques la consulta dins de la transacció

                        DBUtil.crearParametre(consulta, "@proj_id", idProjecte, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@usu_id", idUsuari, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@rol_id", idRol, DbType.Int32);

                        consulta.CommandText = $@"insert into projecte_usuari_rol (proj_id, usu_id, rol_id)
                                                  values (@proj_id, @usu_id, @rol_id)";

                        int numeroDeFiles = consulta.ExecuteNonQuery(); //per fer un update o un delete
                        if (numeroDeFiles != 1)
                        {
                            transaccio.Rollback();
                        }
                        else
                        {
                            transaccio.Commit();
                        }

                    }

                }
            }
        }

        public static ObservableCollection<ProjecteUsuariRol> GetLlistaUsuarisAssignatsAmbRol(int idProjecte)
        {
            ObservableCollection<ProjecteUsuariRol> projecteUsuariRol = new ObservableCollection<ProjecteUsuariRol>();

            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {
                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        DBUtil.crearParametre(consulta, "@proj_id", idProjecte, DbType.Int32);

                        consulta.CommandText = $@"select u.usu_id, u.usu_nom, u.usu_cognom_1, u.usu_cognom_2, u.usu_data_naixement, 
                                                         u.usu_login, u.usu_password_hash, pur.rol_id
                                                  from projecte_usuari_rol pur join usuari u on pur.usu_id = u.usu_id
                                                  where pur.proj_id = @proj_id";
                        DbDataReader reader = consulta.ExecuteReader(); //per cuan pot retorna mes d'una fila

                        Dictionary<string, int> ordinals = new Dictionary<string, int>();
                        string[] cols = { "usu_id", "usu_nom", "usu_cognom_1", "usu_cognom_2", "usu_data_naixement",
                                           "usu_login", "usu_password_hash", "rol_id"};
                        foreach (string c in cols)
                        {
                            ordinals[c] = reader.GetOrdinal(c);
                        }

                        while (reader.Read()) //llegeix la fila seguent, retorna true si ha pogut llegir la fila, retorna false si no hi ha mes dades per lleguir
                        {
                            
                            int usu_id = reader.GetInt32(ordinals["usu_id"]);
                            string usu_nom = reader.GetString(ordinals["usu_nom"]);
                            string usu_cognom_1 = reader.GetString(ordinals["usu_cognom_1"]);
                            string usu_cognom_2 = readerStringOrNull(reader, ordinals["usu_cognom_2"], null);
                            DateTime usu_data_naixement = reader.GetDateTime(ordinals["usu_data_naixement"]);
                            string usu_login = reader.GetString(ordinals["usu_login"]);
                            string usu_password_hash = reader.GetString(ordinals["usu_password_hash"]);
                            int rol_id = reader.GetInt32(ordinals["rol_id"]);

                            Usuari usu = new Usuari(usu_id, usu_nom, usu_cognom_1, usu_cognom_2, usu_data_naixement,
                                              usu_login, usu_password_hash);
                            Rol rol = RolDB.GetRol(rol_id);
                            Projecte proj = ProjecteDB.GetProjecte(idProjecte);
                            projecteUsuariRol.Add(new ProjecteUsuariRol(proj, usu, rol));
                        }

                    }
                }
            }
            return projecteUsuariRol;
        }

        public static bool desassignarUsuari(int idProjecte, int idUsuari)
        {
            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {
                bool haAnatBe = true;

                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    DbTransaction transaccio = connection.BeginTransaction(); //Creacio d'una transaccio

                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        consulta.Transaction = transaccio; // marques la consulta dins de la transacció


                        DBUtil.crearParametre(consulta, "@proj_id", idProjecte, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@usu_id", idUsuari, DbType.Int32);

                        consulta.CommandText = "select count(1) from projecte_usuari_rol where proj_id = @proj_id and usu_id = @usu_id";
                        long numProjectes = (long)consulta.ExecuteScalar();

                        if (numProjectes != 1) return false;

                        consulta.CommandText = "delete from projecte_usuari_rol where proj_id = @proj_id and usu_id = @usu_id";

                        int numDeleted = consulta.ExecuteNonQuery();

                        if (numDeleted != 1)
                        {
                            transaccio.Rollback();
                            haAnatBe = false;
                        }
                        transaccio.Commit();
                        return haAnatBe;

                    }

                }

            }
        }

        private static string readerStringOrNull(DbDataReader reader, int ordinal, String valorPerDefecte)
        {
            string value = valorPerDefecte;
            if (!reader.IsDBNull(ordinal))
            {
                value = reader.GetString(ordinal);
            }
            return value;
        }

    }
}

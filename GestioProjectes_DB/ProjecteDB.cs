using DB_MySQL;
using GestorProjectes;
using Microsoft.EntityFrameworkCore;
using ModelGestioProjectes;
using NBA_BD.db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;

namespace GestioProjectes_DB
{
    public class ProjecteDB
    {
        public static ObservableCollection<Projecte> GetLlistaProjectes()
        {
            ObservableCollection<Projecte> projectes = new ObservableCollection<Projecte>();

            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {
                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        consulta.CommandText = $@"select proj_id, proj_nom, proj_descripcio, usu_cap_projecte from projecte";
                        DbDataReader reader = consulta.ExecuteReader(); //per cuan pot retorna mes d'una fila

                        Dictionary<string, int> ordinals = new Dictionary<string, int>();
                        string[] cols = { "proj_id", "proj_nom", "proj_descripcio", "usu_cap_projecte"};
                        foreach (string c in cols)
                        {
                            ordinals[c] = reader.GetOrdinal(c);
                        }

                        while (reader.Read()) //llegeix la fila seguent, retorna true si ha pogut llegir la fila, retorna false si no hi ha mes dades per lleguir
                        {
                            int proj_id = reader.GetInt32(ordinals["proj_id"]);
                            string proj_nom = reader.GetString(ordinals["proj_nom"]);
                            string proj_descripcio = readerStringOrNull(reader, ordinals["proj_descripcio"], null);
                            int usu_cap_projecte = reader.GetInt32(ordinals["usu_cap_projecte"]);

                            Projecte proj = new Projecte(proj_id, proj_nom, proj_descripcio, UsuariDB.GetUsuari(usu_cap_projecte));
                            projectes.Add(proj);
                        }

                    }
                }
            }
            return projectes;
        }

        public static Projecte GetProjecte(int idProjecte)
        {
            Projecte projecte = null;

            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {

                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        DBUtil.crearParametre(consulta, "@proj_id", idProjecte, DbType.Int32);

                        consulta.CommandText = $@"select proj_id, proj_nom, proj_descripcio, usu_cap_projecte 
                                                  from projecte 
                                                  where proj_id = @proj_id";

                        DbDataReader reader = consulta.ExecuteReader(); //per cuan pot retorna mes d'una fila

                        Dictionary<string, int> ordinals = new Dictionary<string, int>();
                        string[] cols = { "proj_id", "proj_nom", "proj_descripcio", "usu_cap_projecte" };

                        foreach (string c in cols)
                        {
                            ordinals[c] = reader.GetOrdinal(c);
                        }

                        while (reader.Read()) //llegeix la fila seguent, retorna true si ha pogut llegir la fila, retorna false si no hi ha mes dades per lleguir
                        {
                            int proj_id = reader.GetInt32(ordinals["proj_id"]);
                            string proj_nom = reader.GetString(ordinals["proj_nom"]);
                            string proj_descripcio = readerStringOrNull(reader, ordinals["proj_descripcio"], null);
                            int usu_cap_projecte = reader.GetInt32(ordinals["usu_cap_projecte"]);

                            projecte = new Projecte(proj_id, proj_nom, proj_descripcio, UsuariDB.GetUsuari(usu_cap_projecte));
                        }


                    }
                }
            }

            return projecte;
        }

        public static void addProjecte(Projecte proj)
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

                        consulta.CommandText = "select max(proj_id)+1 from projecte";
                        int nextProjecteId = (int)(Int64)consulta.ExecuteScalar();

                        DBUtil.crearParametre(consulta, "@proj_id", nextProjecteId, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@proj_nom", proj.Nom, DbType.String);
                        DBUtil.crearParametre(consulta, "@proj_descripcio", proj.Descripcio, DbType.String);
                        DBUtil.crearParametre(consulta, "@usu_cap_projecte", proj.CapProjecte.Id, DbType.Int32);

                        consulta.CommandText = $@"insert into projecte (proj_id, proj_nom, proj_descripcio, usu_cap_projecte)
                                                  values (@proj_id, @proj_nom, @proj_descripcio, @usu_cap_projecte)";

                        int numeroDeFiles = consulta.ExecuteNonQuery(); //per fer un update o un delete
                        if (numeroDeFiles != 1)
                        {
                            transaccio.Rollback();
                        }
                        else
                        {
                            proj.Id = (int)nextProjecteId;
                            transaccio.Commit();
                        }

                    }

                }
            }
        }

        public static void updateProjecte(Projecte proj)
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


                        DBUtil.crearParametre(consulta, "@proj_id", proj.Id, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@proj_nom", proj.Nom, DbType.String);
                        DBUtil.crearParametre(consulta, "@proj_descripcio", proj.Descripcio, DbType.String);
                        DBUtil.crearParametre(consulta, "@usu_cap_projecte", proj.CapProjecte.Id, DbType.Int32);

                        consulta.CommandText = $@"update projecte set proj_nom = @proj_nom,
					                                              proj_descripcio = @proj_descripcio,
                                                                  usu_cap_projecte = @usu_cap_projecte
		                                          where proj_id = @proj_id";

                        int numeroDeFiles = consulta.ExecuteNonQuery(); //per fer un update o un delete
                        if (numeroDeFiles != 1)
                        {
                            //shit happens
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

        public static bool deleteProjecte(int projId)
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


                        DBUtil.crearParametre(consulta, "@proj_id", projId, DbType.Int32);
                        consulta.CommandText = "select count(1) from projecte where proj_id = @proj_id";
                        long numProjectes = (long)consulta.ExecuteScalar();

                        if (numProjectes != 1) return false;

                        consulta.CommandText = "delete from projecte where proj_id = @proj_id";

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

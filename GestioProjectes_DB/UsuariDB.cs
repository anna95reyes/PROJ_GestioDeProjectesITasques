using Microsoft.EntityFrameworkCore;
using ModelGestioProjectes;
using NBA_BD.db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Text;

namespace DB_MySQL
{
    public class UsuariDB
    {
        public static ObservableCollection<Usuari> GetLlistaUsuarisAssignats(int idProjecte)
        {
            ObservableCollection<Usuari> usuaris = new ObservableCollection<Usuari>();

            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {
                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        DBUtil.crearParametre(consulta, "@proj_id", idProjecte, DbType.Int32);

                        consulta.CommandText = $@"select u.usu_id, u.usu_nom, u.usu_cognom_1, u.usu_cognom_2, u.usu_data_naixement, u.usu_login, u.usu_password_hash
                                                  from projecte_usuari_rol pur join usuari u on pur.usu_id = u.usu_id
                                                  where pur.proj_id = @proj_id";
                        DbDataReader reader = consulta.ExecuteReader(); //per cuan pot retorna mes d'una fila

                        Dictionary<string, int> ordinals = new Dictionary<string, int>();
                        string[] cols = { "usu_id", "usu_nom", "usu_cognom_1", "usu_cognom_2", "usu_data_naixement",
                                           "usu_login", "usu_password_hash" };
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

                            Usuari usuari = new Usuari(usu_id, usu_nom, usu_cognom_1, usu_cognom_2, usu_data_naixement,
                                              usu_login, usu_password_hash);
                            usuaris.Add(usuari);
                        }

                    }
                }
            }
            return usuaris;
        }

        public static ObservableCollection<Usuari> GetLlistaUsuarisNoAssignats(int idProjecte)
        {
            ObservableCollection<Usuari> usuaris = new ObservableCollection<Usuari>();

            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {
                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        DBUtil.crearParametre(consulta, "@proj_id", idProjecte, DbType.Int32);

                        consulta.CommandText = $@"select distinct u.usu_id, u.usu_nom, u.usu_cognom_1, u.usu_cognom_2, u.usu_data_naixement, u.usu_login, u.usu_password_hash
                                                  from projecte_usuari_rol pur right join usuari u on pur.usu_id = u.usu_id
                                                  where pur.proj_id is null or u.usu_id not in (select u.usu_id
											                                                    from projecte_usuari_rol pur join usuari u on pur.usu_id = u.usu_id
											                                                    where pur.proj_id = @proj_id)";
                        DbDataReader reader = consulta.ExecuteReader(); //per cuan pot retorna mes d'una fila

                        Dictionary<string, int> ordinals = new Dictionary<string, int>();
                        string[] cols = { "usu_id", "usu_nom", "usu_cognom_1", "usu_cognom_2", "usu_data_naixement",
                                           "usu_login", "usu_password_hash" };
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

                            Usuari usuari = new Usuari(usu_id, usu_nom, usu_cognom_1, usu_cognom_2, usu_data_naixement,
                                              usu_login, usu_password_hash);
                            usuaris.Add(usuari);
                        }

                    }
                }
            }
            return usuaris;
        }





        public static Usuari GetUsuari(int idUsuari)
        {
            Usuari usuari = null;
            
            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {

                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        DBUtil.crearParametre(consulta, "@usu_id", idUsuari, DbType.Int32);

                        consulta.CommandText = $@"select usu_id, usu_nom, usu_cognom_1, usu_cognom_2, usu_data_naixement, 
                                                         usu_login, usu_password_hash 
                                                  from usuari
                                                  where usu_id = @usu_id";

                        DbDataReader reader = consulta.ExecuteReader(); //per cuan pot retorna mes d'una fila

                        Dictionary<string, int> ordinals = new Dictionary<string, int>();
                        string[] cols = { "usu_id", "usu_nom", "usu_cognom_1", "usu_cognom_2", "usu_data_naixement",
                                           "usu_login", "usu_password_hash" };
                        foreach (string c in cols)
                        {
                            ordinals[c] = reader.GetOrdinal(c);
                        }

                        while (reader.Read()) //llegeix la fila seguent, retorna true si ha pogut llegir la fila, retorna false si no hi ha mes dades per lleguir
                        {
                            int usu_id = reader.GetInt32(ordinals["usu_id"]);
                            string usu_nom = reader.GetString(ordinals["usu_nom"]);
                            string usu_cognom_1 = reader.GetString(ordinals["usu_cognom_1"]);
                            string usu_cognom_2 = readerStringOrNull(reader,ordinals["usu_cognom_2"],null);
                            DateTime usu_data_naixement = reader.GetDateTime(ordinals["usu_data_naixement"]);
                            string usu_login = reader.GetString(ordinals["usu_login"]);
                            string usu_password_hash = reader.GetString(ordinals["usu_password_hash"]);

                            usuari =  new Usuari(usu_id, usu_nom, usu_cognom_1, usu_cognom_2, usu_data_naixement,
                                              usu_login, usu_password_hash);
                        }


                    }
                }
            }

            return usuari;
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

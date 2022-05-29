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
    public class TascaDB
    {
        public static ObservableCollection<Tasca> GetLlistaTasques(int idProjecte)
        {
            ObservableCollection<Tasca> tasques = new ObservableCollection<Tasca>();

            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {
                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        DBUtil.crearParametre(consulta, "@proj_id", idProjecte, DbType.Int32);

                        consulta.CommandText = $@"select tasc_id, tasc_data_creacio, tasc_nom, tasc_descripcio, tasc_data_limit, 
                                                         proj_id, usu_creada_per, usu_assignada_a, stat_id 
                                                  from tasca
                                                  where proj_id = @proj_id";
                        DbDataReader reader = consulta.ExecuteReader(); //per cuan pot retorna mes d'una fila

                        Dictionary<string, int> ordinals = new Dictionary<string, int>();
                        string[] cols = { "tasc_id", "tasc_data_creacio", "tasc_nom", "tasc_descripcio", "tasc_data_limit",
                                           "proj_id", "usu_creada_per", "usu_assignada_a", "stat_id" };
                        foreach (string c in cols)
                        {
                            ordinals[c] = reader.GetOrdinal(c);
                        }

                        while (reader.Read()) //llegeix la fila seguent, retorna true si ha pogut llegir la fila, retorna false si no hi ha mes dades per lleguir
                        {
                            int tasc_id = reader.GetInt32(ordinals["tasc_id"]);
                            DateTime tasc_data_creacio = reader.GetDateTime(ordinals["tasc_data_creacio"]);
                            string tasc_nom = reader.GetString(ordinals["tasc_nom"]);
                            string tasc_descripcio = readerStringOrNull(reader, ordinals["tasc_descripcio"], null);
                            DateTime? tasc_data_limit = readerDateTimeOrNull(reader, ordinals["tasc_data_limit"], null);
                            int proj_id = reader.GetInt32(ordinals["proj_id"]);
                            int usu_creada_per = reader.GetInt32(ordinals["usu_creada_per"]);
                            int? usu_assignada_a = readerIntegerOrNull(reader, ordinals["usu_assignada_a"], null);
                            int stat_id = reader.GetInt32(ordinals["stat_id"]);

                            Tasca tasca = new Tasca(tasc_id, tasc_data_creacio, tasc_nom, tasc_descripcio, tasc_data_limit,
                                                    UsuariDB.GetUsuari(usu_creada_per), 
                                                    (usu_assignada_a!=null)? UsuariDB.GetUsuari((int)usu_assignada_a):null,
                                                    EstatDB.GetEstat(stat_id));
                            tasques.Add(tasca);
                        }

                    }
                }
            }
            return tasques;
        }


        public static Tasca GetTasca(int idTasca)
        {
            Tasca tasca = null;

            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {

                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        DBUtil.crearParametre(consulta, "@idTasca", idTasca, DbType.Int32);

                        consulta.CommandText = $@"select tasc_id, tasc_data_creacio, tasc_nom, tasc_descripcio, tasc_data_limit, 
                                                         proj_id, usu_creada_per, usu_assignada_a, stat_id 
                                                  from tasca
                                                  where tasc_id = @idTasca";

                        DbDataReader reader = consulta.ExecuteReader(); //per cuan pot retorna mes d'una fila

                        Dictionary<string, int> ordinals = new Dictionary<string, int>();
                        string[] cols = { "tasc_id", "tasc_data_creacio", "tasc_nom", "tasc_descripcio", "tasc_data_limit",
                                           "proj_id", "usu_creada_per", "usu_assignada_a", "stat_id" };
                        foreach (string c in cols)
                        {
                            ordinals[c] = reader.GetOrdinal(c);
                        }

                        while (reader.Read()) //llegeix la fila seguent, retorna true si ha pogut llegir la fila, retorna false si no hi ha mes dades per lleguir
                        {
                            int tasc_id = reader.GetInt32(ordinals["tasc_id"]);
                            DateTime tasc_data_creacio = reader.GetDateTime(ordinals["tasc_data_creacio"]);
                            string tasc_nom = reader.GetString(ordinals["tasc_nom"]);
                            string tasc_descripcio = readerStringOrNull(reader, ordinals["tasc_descripcio"], null);
                            DateTime? tasc_data_limit = readerDateTimeOrNull(reader, ordinals["tasc_data_limit"], null);
                            int proj_id = reader.GetInt32(ordinals["proj_id"]);
                            int usu_creada_per = reader.GetInt32(ordinals["usu_creada_per"]);
                            int? usu_assignada_a = readerIntegerOrNull(reader, ordinals["usu_assignada_a"], null);
                            int stat_id = reader.GetInt32(ordinals["stat_id"]);

                            tasca = new Tasca(tasc_id, tasc_data_creacio, tasc_nom, tasc_descripcio, tasc_data_limit,
                                                    UsuariDB.GetUsuari(usu_creada_per),
                                                    (usu_assignada_a != null) ? UsuariDB.GetUsuari((int)usu_assignada_a) : null,
                                                    EstatDB.GetEstat(stat_id));
                        }


                    }
                }
            }

            return tasca;
        }


        private static int? readerIntegerOrNull(DbDataReader reader, int ordinal, int? valorPerDefecte)
        {
            int? value = valorPerDefecte;
            if (!reader.IsDBNull(ordinal))
            {
                value = reader.GetInt32(ordinal);
            }
            return value;
        }

        private static DateTime? readerDateTimeOrNull(DbDataReader reader, int ordinal, DateTime? valorPerDefecte)
        {
            DateTime? value = valorPerDefecte;
            if (!reader.IsDBNull(ordinal))
            {
                value = reader.GetDateTime(ordinal);
            }
            return value;
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

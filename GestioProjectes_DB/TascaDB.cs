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
                                                  where proj_id = = @proj_id";
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
                            /*int usu_id = reader.GetInt32(ordinals["usu_id"]);
                            string usu_nom = reader.GetString(ordinals["usu_nom"]);
                            string usu_cognom_1 = reader.GetString(ordinals["usu_cognom_1"]);
                            string usu_cognom_2 = readerStringOrNull(reader, ordinals["usu_cognom_2"], null);
                            DateTime usu_data_naixement = reader.GetDateTime(ordinals["usu_data_naixement"]);
                            string usu_login = reader.GetString(ordinals["usu_login"]);
                            string usu_password_hash = reader.GetString(ordinals["usu_password_hash"]);*/

                            Tasca taca = new Tasca();
                            tasques.Add(tasca);
                        }

                    }
                }
            }
            return tasques;
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

using Microsoft.EntityFrameworkCore;
using ModelGestioProjectes;
using NBA_BD.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DB_MySQL
{
    public class EstatDB
    {
        public static Estat GetEstat(int idEstat)
        {
            Estat estat = null;

            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {

                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        DBUtil.crearParametre(consulta, "@stat_id", idEstat, DbType.Int32);

                        consulta.CommandText = $@"select stat_id, stat_nom from estat where stat_id = @stat_id";

                        DbDataReader reader = consulta.ExecuteReader(); //per cuan pot retorna mes d'una fila

                        Dictionary<string, int> ordinals = new Dictionary<string, int>();
                        string[] cols = { "stat_id", "stat_nom" };
                        foreach (string c in cols)
                        {
                            ordinals[c] = reader.GetOrdinal(c);
                        }

                        while (reader.Read()) //llegeix la fila seguent, retorna true si ha pogut llegir la fila, retorna false si no hi ha mes dades per lleguir
                        {
                            int stat_id = reader.GetInt32(ordinals["stat_id"]);
                            string stat_nom = reader.GetString(ordinals["stat_nom"]);

                            estat = new Estat(stat_id, stat_nom);
                        }


                    }
                }
            }

            return estat;
        }

    }
}

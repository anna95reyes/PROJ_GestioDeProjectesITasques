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

        public static void addTasca(Tasca tasca, int idProjecte)
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

                        consulta.CommandText = "select max(tasc_id)+1 from tasca";
                        int nextTascaId = (int)(Int64)consulta.ExecuteScalar();

                        DBUtil.crearParametre(consulta, "@tasc_id", nextTascaId, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@tasc_data_creacio", tasca.DataCreacio, DbType.DateTime);
                        DBUtil.crearParametre(consulta, "@tasc_nom", tasca.Nom, DbType.String);
                        DBUtil.crearParametre(consulta, "@tasc_descripcio", tasca.Descripcio, DbType.String);
                        DBUtil.crearParametre(consulta, "@tasc_data_limit", tasca.DataLimit, DbType.DateTime);
                        DBUtil.crearParametre(consulta, "@proj_id", idProjecte, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@usu_creada_per", tasca.Propietari.Id, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@stat_id", tasca.Estat.Id, DbType.Int32);

                        if (tasca.Responsable != null)
                        {
                            DBUtil.crearParametre(consulta, "@usu_assignada_a", tasca.Responsable.Id, DbType.Int32);

                            consulta.CommandText = $@"insert into tasca (tasc_id, tasc_data_creacio, tasc_nom, tasc_descripcio, 
                                                                     tasc_data_limit, proj_id, usu_creada_per, usu_assignada_a, stat_id)
                                                  values (@tasc_id, @tasc_data_creacio, @tasc_nom, @tasc_descripcio , @tasc_data_limit, 
                                                          @proj_id, @usu_creada_per, @usu_assignada_a, @stat_id)";

                        } 
                        else
                        {
                            consulta.CommandText = $@"insert into tasca (tasc_id, tasc_data_creacio, tasc_nom, tasc_descripcio, 
                                                                     tasc_data_limit, proj_id, usu_creada_per, usu_assignada_a, stat_id)
                                                  values (@tasc_id, @tasc_data_creacio, @tasc_nom, @tasc_descripcio , @tasc_data_limit, 
                                                          @proj_id, @usu_creada_per, null, @stat_id)";
                        }

                        int numeroDeFiles = consulta.ExecuteNonQuery(); //per fer un update o un delete
                        if (numeroDeFiles != 1)
                        {
                            transaccio.Rollback();
                        }
                        else
                        {
                            tasca.Id = (int)nextTascaId;
                            transaccio.Commit();
                        }

                    }

                }
            }
        }

        public static void updateTasca(Tasca tasca)
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


                        DBUtil.crearParametre(consulta, "@tasc_id", tasca.Id, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@tasc_nom", tasca.Nom, DbType.String);
                        DBUtil.crearParametre(consulta, "@tasc_descripcio", tasca.Descripcio, DbType.String);
                        DBUtil.crearParametre(consulta, "@tasc_data_limit", tasca.DataLimit, DbType.DateTime);
                        DBUtil.crearParametre(consulta, "@usu_creada_per", tasca.Propietari.Id, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@usu_assignada_a", tasca.Responsable.Id, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@stat_id", tasca.Estat.Id, DbType.Int32);

                        consulta.CommandText = $@"update tasca set tasc_nom = @tasc_nom,
                                                                   tasc_descripcio = @tasc_descripcio,
                                                                   tasc_data_limit = @tasc_data_limit,
                                                                   usu_creada_per = @usu_creada_per,
                                                                   usu_assignada_a = @usu_assignada_a,
                                                                   stat_id = @stat_id
		                                          where tasc_id = @tasc_id";

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

        public static bool deleteTasca(int idTasca)
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


                        DBUtil.crearParametre(consulta, "@tasc_id", idTasca, DbType.Int32);
                        consulta.CommandText = "select count(1) from tasca where tasc_id = @tasc_id";
                        long numProjectes = (long)consulta.ExecuteScalar();

                        if (numProjectes != 1) return false;

                        consulta.CommandText = "delete from tasca where tasc_id = @tasc_id";

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

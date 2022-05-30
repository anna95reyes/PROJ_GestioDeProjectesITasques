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
    public class EntradaDB
    {
        public static ObservableCollection<Entrada> GetLlistaEntrades(int idTasca)
        {
            ObservableCollection<Entrada> entrades = new ObservableCollection<Entrada>();

            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {
                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        DBUtil.crearParametre(consulta, "@tasc_id", idTasca, DbType.Int32);

                        consulta.CommandText = $@"select entr_numeracio, entr_data, entr_entrada, usu_escrita_per, 
                                                         usu_nova_assignacio, stat_id
                                                  from entrada
                                                  where tasc_id = @tasc_id";
                        DbDataReader reader = consulta.ExecuteReader(); //per cuan pot retorna mes d'una fila

                        Dictionary<string, int> ordinals = new Dictionary<string, int>();
                        string[] cols = { "entr_numeracio", "entr_data", "entr_entrada", "usu_escrita_per", "usu_nova_assignacio",
                                           "stat_id"};
                        foreach (string c in cols)
                        {
                            ordinals[c] = reader.GetOrdinal(c);
                        }

                        while (reader.Read()) //llegeix la fila seguent, retorna true si ha pogut llegir la fila, retorna false si no hi ha mes dades per lleguir
                        {
                            int entr_numeracio = reader.GetInt32(ordinals["entr_numeracio"]);
                            DateTime entr_data = reader.GetDateTime(ordinals["entr_data"]);
                            string entr_entrada = reader.GetString(ordinals["entr_entrada"]);
                            int usu_escrita_per = reader.GetInt32(ordinals["usu_escrita_per"]);
                            int? usu_nova_assignacio = readerIntegerOrNull(reader, ordinals["usu_nova_assignacio"], null);
                            int? stat_id = readerIntegerOrNull(reader, ordinals["stat_id"], null);

                            Entrada entrada = new Entrada(entr_numeracio, entr_data, entr_entrada, 
                                UsuariDB.GetUsuari((int)usu_escrita_per),
                                (usu_nova_assignacio != null)? UsuariDB.GetUsuari((int)usu_nova_assignacio) : null,
                                (stat_id != null)? EstatDB.GetEstat((int)stat_id) : null);
                            entrades.Add(entrada);
                        }

                    }
                }
            }
            return entrades;
        }

        public static Entrada GetEntrada (int idTasca, int idNumeracio)
        {
            Entrada entrada = null;

            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {

                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        DBUtil.crearParametre(consulta, "@tasc_id", idTasca, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@entr_numeracio", idNumeracio, DbType.Int32);

                        consulta.CommandText = $@"select entr_numeracio, entr_data, entr_entrada, usu_escrita_per, 
                                                         usu_nova_assignacio, stat_id
                                                  from entrada
                                                  where tasc_id = @tasc_id and entr_numeracio = @entr_numeracio";

                        DbDataReader reader = consulta.ExecuteReader(); //per cuan pot retorna mes d'una fila

                        Dictionary<string, int> ordinals = new Dictionary<string, int>();
                        string[] cols = { "entr_numeracio", "entr_data", "entr_entrada", "usu_escrita_per", "usu_nova_assignacio",
                                           "stat_id"};
                        foreach (string c in cols)
                        {
                            ordinals[c] = reader.GetOrdinal(c);
                        }

                        while (reader.Read()) //llegeix la fila seguent, retorna true si ha pogut llegir la fila, retorna false si no hi ha mes dades per lleguir
                        {
                            int entr_numeracio = reader.GetInt32(ordinals["entr_numeracio"]);
                            DateTime entr_data = reader.GetDateTime(ordinals["entr_data"]);
                            string entr_entrada = reader.GetString(ordinals["entr_entrada"]);
                            int usu_escrita_per = reader.GetInt32(ordinals["usu_escrita_per"]);
                            int? usu_nova_assignacio = readerIntegerOrNull(reader, ordinals["usu_nova_assignacio"], null);
                            int? stat_id = readerIntegerOrNull(reader, ordinals["stat_id"], null);

                            entrada = new Entrada(entr_numeracio, entr_data, entr_entrada,
                                UsuariDB.GetUsuari((int)usu_escrita_per),
                                (usu_nova_assignacio != null) ? UsuariDB.GetUsuari((int)usu_nova_assignacio) : null,
                                (stat_id != null) ? EstatDB.GetEstat((int)stat_id) : null);
                        }


                    }
                }
            }

            return entrada;
        }

        public static void addEntrada(int idTasca, Entrada entrada)
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

                        DBUtil.crearParametre(consulta, "@tasc_id", idTasca, DbType.Int32);

                        consulta.CommandText = "select max(entr_numeracio)+1 from entrada where tasc_id = @idTasca";
                        int nextNumeracioId = (int)(Int64)consulta.ExecuteScalar();

                        
                        DBUtil.crearParametre(consulta, "@entr_numeracio", nextNumeracioId, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@entr_data", entrada.Data, DbType.DateTime);
                        DBUtil.crearParametre(consulta, "@entr_entrada", entrada._Entrada, DbType.String);
                        DBUtil.crearParametre(consulta, "@usu_escrita_per", entrada.Escriptor.Id, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@usu_nova_assignacio", entrada.NovaAssignacio.Id, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@stat_id", entrada.NouEstat.Id, DbType.Int32);

                        consulta.CommandText = $@"insert into entrada (tasc_id, entr_numeracio, entr_data, entr_entrada, 
                                                                       usu_escrita_per, usu_nova_assignacio, stat_id)
                                                  values (@tasc_id, @entr_numeracio, @entr_data, @entr_entrada, @usu_escrita_per, 
                                                          @usu_nova_assignacio, @stat_id)";

                        int numeroDeFiles = consulta.ExecuteNonQuery(); //per fer un update o un delete
                        if (numeroDeFiles != 1)
                        {
                            transaccio.Rollback();
                        }
                        else
                        {
                            entrada.Numero = (int)nextNumeracioId;
                            transaccio.Commit();
                        }

                    }

                }
            }
        }

        public static void updateEntrada(int idTasca, Entrada entrada)
        {
            //TODO: fer l'update
            using (MySqlDBContext context = new MySqlDBContext()) //crea el contexte de la base de dades
            {
                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    DbTransaction transaccio = connection.BeginTransaction(); //Creacio d'una transaccio

                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        consulta.Transaction = transaccio; // marques la consulta dins de la transacció

                        DBUtil.crearParametre(consulta, "@tasc_id", idTasca, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@entr_numeracio", entrada.Numero, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@entr_data", entrada.Data, DbType.DateTime);
                        DBUtil.crearParametre(consulta, "@entr_entrada", entrada._Entrada, DbType.String);
                        DBUtil.crearParametre(consulta, "@usu_escrita_per", entrada.Escriptor.Id, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@usu_nova_assignacio", entrada.NovaAssignacio.Id, DbType.Int32);
                        DBUtil.crearParametre(consulta, "@stat_id", entrada.NouEstat.Id, DbType.Int32);

                        consulta.CommandText = $@"update entrada set entr_data = @entr_data,
                                                                   entr_entrada = @entr_entrada,
                                                                   usu_escrita_per = @usu_escrita_per,
                                                                   usu_nova_assignacio = @usu_nova_assignacio,
                                                                   stat_id = @stat_id
		                                          where tasc_id = @tasc_id and entr_numeracio = @entr_numeracio";

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

        public static bool deleteEntrada(int idTasca, int numeroEntrada)
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
                        DBUtil.crearParametre(consulta, "@entr_numeracio", numeroEntrada, DbType.Int32);
                        consulta.CommandText = "select count(1) from entrada where tasc_id = @tasc_id and entr_numeracio = @entrada_numeracio";
                        long numProjectes = (long)consulta.ExecuteScalar();

                        if (numProjectes != 1) return false;

                        consulta.CommandText = "delete from entrada where tasc_id = @tasc_id and entr_numeracio = @entrada_numeracio";

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


    }
}

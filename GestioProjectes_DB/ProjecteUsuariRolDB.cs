using Microsoft.EntityFrameworkCore;
using NBA_BD.db;
using System;
using System.Collections.Generic;
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
                                                  values (proj_id, usu_id, rol_id)";

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


    }
}

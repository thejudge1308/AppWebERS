using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AppWebERS.Models{
    /*
     * Matías Parra
     */
    public class ConectorBD{
        private static string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        
        /**
         * Método para ejecutar una query 
         **/

        public DataSet ejecutarQuery(MySqlCommand command) {
            var ds = new DataSet();
            using (var conn = new MySqlConnection(connStr)) {
                conn.Open();
                var sqlTran = conn.BeginTransaction();
                try {
                    command.Connection = conn;
                    command.Transaction = sqlTran;
                    command.CommandType = CommandType.StoredProcedure;
                    var sqlda = new MySqlDataAdapter(command);
                    sqlda.SelectCommand.Transaction = sqlTran;
                    sqlda.Fill(ds);
                    sqlTran.Commit();
                }
                catch (Exception ex) {
                    sqlTran.Rollback();
                    Console.WriteLine(ex.ToString());
                    command = null;
                    throw ex;
                }
                finally {
                    conn.Close();
                }
            }
            return ds;
        }

        /**
         * Método para ejecutar una query 
         * <returns>Retorna un DataSet</returns>
         **/

        public MySqlCommand ejecutarNoQuery(MySqlCommand command) {
            using (var conn = new MySqlConnection(connStr)) {
                conn.Open();
                var sqlTran = conn.BeginTransaction();
                try {
                    command.Connection = conn;
                    command.Transaction = sqlTran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    sqlTran.Commit();
                }
                catch (Exception ex) {
                    sqlTran.Rollback();
                    Console.WriteLine(ex.ToString());
                    command = null;
                    throw ex;
                }
                finally {
                    conn.Close();
                }
            }
            return command;
        }
        
    }
}
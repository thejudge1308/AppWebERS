using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebERS.Models
{
    public class Usuario
    {
        public string rut { get; set; }
        public string nombre { get; set; }
        public string contrasenia { get; set; }
        public string correoElectronico { get; set; }
        public string tipo { get; set; }


          
        public bool modificar()
        {
            using (var conn = new
            MySqlConnection(ConfigurationManager.ConnectionStrings["appers"].ConnectionString))
            {
                conn.Open();
                var sqlTran = conn.BeginTransaction();
                try
                {
                    var command = new MySqlCommand()
                    {
                        CommandText = "modificarUsuario",
                        CommandType = CommandType.StoredProcedure
                    };
                    //Setea el valor de los atributos del SP (procedimiento almacenado)
                    command.Parameters.AddWithValue("rut", this.rut);
                    command.Parameters.AddWithValue("nombre", this.nombre);
                    command.Parameters.AddWithValue("contrasenia", this.contrasenia);
                    command.Parameters.AddWithValue("correoElectronico", this.correoElectronico);
                    command.Parameters.AddWithValue("tipo", this.tipo);
                    command.Connection = conn;
                    command.Transaction = sqlTran;
                    command.ExecuteNonQuery();
                    sqlTran.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    sqlTran.Rollback();
                }
            }
            return true;
        }

        /*
         * Juan Abello
         * Cambia es estado de deshabilitado a habilitado
         * rut
         */ 
        public void deshabilitarUsuario(string rut)
        {
            this.seleccionar(rut);
            if(this.estado.Equals("1"))
            {
                using (var conn = new
            MySqlConnection(ConfigurationManager.ConnectionStrings["appers"].ConnectionString))
                {
                    conn.Open();
                    var sqlTran = conn.BeginTransaction();
                    try
                    {
                        var command = new MySqlCommand()
                        {
                            CommandText = "deshabilitarUsuario",
                            CommandType = CommandType.StoredProcedure
                        };
                        
                        command.Parameters.AddWithValue("estado","0");
                        command.Connection = conn;
                        command.Transaction = sqlTran;
                        command.ExecuteNonQuery();
                        sqlTran.Commit();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        sqlTran.Rollback();
                    }
                }
            }
        }
    }
}
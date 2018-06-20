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
        public string correo_electronico { get; set; }
        public string contrasenia { get; set; }
        public string tipo { get; set; }
        public string estado { get; set; }

        public bool modificar()
        {
            using (var conn = new
            MySqlConnection(ConfigurationManager.ConnectionStrings["appers"].ConnectionString))
            {
                conn.Open();
                var sqlTran = conn.BeginTransaction();
                {
                try
                    {
                    var command = new MySqlCommand()
                        CommandText = "modificarUsuario",
                        CommandType = CommandType.StoredProcedure
                    //Setea el valor de los atributos del SP (procedimiento almacenado)
                    };
                    command.Parameters.AddWithValue("rut", this.rut);
                    command.Parameters.AddWithValue("nombre", this.nombre);
                    command.Parameters.AddWithValue("contrasenia", this.contrasenia);
                    command.Parameters.AddWithValue("correoElectronico", this.correoElectronico);
                    command.Connection = conn;
                    command.Parameters.AddWithValue("tipo", this.tipo);
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
                        var command = new MySqlCommand()
                    {
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
        /*
         * Autor: Nicolás Hervias
         * Carga los datos de un usuario especigficado con su rut. Este método llama al método CargarDatos, donde
         * los datos cargados desde la base de datos son asignados a las variables de la clase. Si no encuentra al usuario
         * llena los datos con "Not Found".
         * Parámetros: rut (es el rut del usuario que se desea modificar)
         * Retorna: vacío
         */
        {
        public void seleccionar(string rut)
            try
            {
                var dataSet = new DataSet();
                using (var conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["appers"].ConnectionString))
                {
                    var command = new MySqlCommand() { CommandText = "getRutUsuario", CommandType = CommandType.StoredProcedure };
                    command.Parameters.AddWithValue("rut", rut);
                    conexion.Open();
                    var sqlda = new MySqlDataAdapter(command);
                    command.Connection = conexion;
                    sqlda.Fill(dataSet);
                    conexion.Close();
                }
                {
                if (dataSet.Tables[0].Rows.Count > 0)
                    this.CargarDatos(dataSet.Tables[0].Rows[0]);
                    return;
                }
                else
                {
                    this.nombre = "Not Found";
                    this.correo_electronico = "Not Found";
                    this.contrasenia = "Not Found";
                    this.estado = "Not Found";
                    this.tipo = "Not Found";
                    //this.rut = -1;
                }
            }
            catch (Exception ex)
            {
                //notificar administrador
                throw ex;
            }
        }

        /*
         * Autor: Nicolás Hervias
         * Convierte los datos cargados desde la base de datos al tipo de la variable de la clase para ser asignados
         * Parámetros: dr (una fila de la tabla de la base de datos)
         * Retorna: vacío
         */
        public void cargarDatos(DataRow dr)
        {
            this.rut = dr["rut"].ToString();
            this.correo_electronico = dr["correo_electronico"].ToString();
            this.nombre = dr["nombre"].ToString();
            this.contrasenia = dr["contrasenia"].ToString();
            this.estado = dr["estado"].ToString();
            this.tipo = dr["tipo"].ToString();
        }

        /*
         * Autor: Nicolás Hervias
         * Cambia el estado de habilitado a deshabilitado (suponiendo que 0 es deshabilitado y 1 es habilitado)
         * Retorna: vacío
         * Parámetros: rut (el rut del usuario)
         */
        public void habilitarUsuario(string rut)
        {
            this.seleccionar(rut);
            if (this.estado.Equals("0"))
            {
                using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["appers"].ConnectionString))
                {
                    conn.Open();
                    var sqlTran = conn.BeginTransaction();
                    try
                    {
                        var command = new MySqlCommand() { CommandText = "habilitarUsuario", CommandType = CommandType.StoredProcedure };
                        command.Parameters.AddWithValue("estado", "1");
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
            //else { }
        }
    }
}
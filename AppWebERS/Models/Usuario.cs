using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AppWebERS.Models{
    /*
     * Matías Parra
     */
    public class Usuario{
        /*
         * Constructor vacío de la clase usuario (se agrega para cualquier otro uso que se le de en un futuro).
         * 
         */
        public Usuario(){

        }
        /*
         * Constructor de la clase usuario.
         * 
         * <param name="rut">El rut del usuario.</param>
         * <param name="nombre">El nombre del usuario.</param>
         * <param name="correoElectronico">El correo electrónico del usuario.</param>
         * <param name="contrasenia">La contraseña del usuario.</param>
         * <param name="tipo">El tipo del usuario (administrador, jefe de proyecto y usuario normal).</param>
         * <param name="tipo">El estado del usuario (true para habilitado, false para deshabilitado).</param>
         * 
         */
        public Usuario(string rut, string nombre, string correoElectronico, string contrasenia, string tipo, bool estado){
            this.Rut = rut;
            this.Nombre = nombre;
            this.CorreoElectronico = correoElectronico;
            this.Contrasenia = contrasenia;
            this.Tipo = tipo;
            this.Estado = estado;
        }

        /*
         * Setter y getter de rut del usuario.
         * 
         * <param name="rut">El rut del usuario.</param>
         * 
         * <returns>Retorna el valor string del rut.</returns>
         * 
         */
        public string Rut {get; set;}

        /*
         * Setter y getter de nombre del usuario.
         * 
         * <param name="nombre">El nombre del usuario.</param>
         * 
         * <returns>Retorna el valor string del rut.</returns>
         * 
         */
        public string Nombre {get; set;}

        /*
         * Setter y getter de correo electrónico del usuario.
         * 
         * <param name="correoElectronico">El correo electrónico del usuario.</param>
         * 
         * <returns>Retorna el valor string del correo electrónico.</returns>
         * 
         */
        public string CorreoElectronico {get; set;}

        /*
         * Setter y getter de contraseña del usuario.
         * 
         * <param name="contrasenia">La contraseña del usuario.</param>
         * 
         * <returns>Retorna el valor string de la contraseña.</returns>
         * 
         */
        public string Contrasenia {get; set;}

        /*
         * Setter y getter de tipo del usuario.
         * 
         * <param name="tipo">El tipo del usuario (administrador, jefe de proyecto y usuario normal).</param>
         * 
         * <returns>Retorna el valor string del tipo.</returns>
         * 
         */
        public string Tipo {get; set;}

        /*
         * Setter y getter de estado del usuario.
         * 
         * <param name="estado">El estado del usuari(true es habilitado, false deshabilitado).</param>
         * 
         * <returns>Retorna el valor bool del estado.</returns>
         * 
         */
        public bool Estado { get; set; }

        /**
         * Método para listar todos los usuarios existentes
         **/

        public void ListarTodos() {
        }

        /**
         * Método para listar un usuario específico
         * <returns>Retorna un usuario específico.</returns>
         **/

       public void ListarEspecifico(Usuario usuario) {
            
        }
        

        /**
         * Método para Crear un Usuario
         * <returns>Retorna un boolean que indica la correcta creación del usuario.</returns>
         **/

        public bool Crear() {
            return true;
        }

        /*
         * Juan Abello
         * Envia los datos modificados a la consulta para ser cambiados en la base de datos
         * return true
         */ 
        public bool modificar()
        {
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["appers"].ConnectionString))
            {
                conn.Open();
                var sqlTran = conn.BeginTransaction();
                try
                {
                    var command = new MySqlCommand() { CommandText = "modificarUsuario", CommandType = CommandType.StoredProcedure };
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
            if (this.estado.Equals("1"))
            {
                using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["appers"].ConnectionString))
                {
                    conn.Open();
                    var sqlTran = conn.BeginTransaction();
                    try
                    {
                        var command = new MySqlCommand() { CommandText = "deshabilitarUsuario", CommandType = CommandType.StoredProcedure };
                        command.Parameters.AddWithValue("estado", "0");
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
        public void seleccionar(string rut)
        {
            try
            {
                var dataSet = new DataSet();
                using (var conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["appers"].ConnectionString))
                {
                    var command = new MySqlCommand() { CommandText = "getRutUsuario", CommandType = CommandType.StoredProcedure };
                    command.Parameters.AddWithValue("rut", rut);
                    conexion.Open();
                    command.Connection = conexion;
                    var sqlda = new MySqlDataAdapter(command);
                    sqlda.Fill(dataSet);
                    conexion.Close();
                }
                if (dataSet.Tables[0].Rows.Count > 0)
                {
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
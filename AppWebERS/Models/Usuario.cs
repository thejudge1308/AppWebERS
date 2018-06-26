using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        //privatw conector solo para testing.
        private ConectorBD conector = ConectorBD.Instance;

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

        public List<Usuario> ListarTodos() {

            List<Usuario> listaUsuarios = new List<Usuario>();

            string consulta = "SELECT * FROM usuario";
            MySqlDataReader reader = this.conector.RealizarConsulta(consulta);
            if(reader == null)
            {
                this.conector.CerrarConexion();
                return null;
            }
            else
            {
               while (reader.Read())
                {
                    string rut = reader.GetString(0);
                    string nombre = reader.GetString(1);
                    string correo = reader.GetString(2);
                    string contrasenia = reader.GetString(3);
                    string tipo = reader.GetString(4);
                    bool estado = reader.GetBoolean(5);
                    string estadoConvert = (estado) ? "Habilitado" : "Deshabilitado";
                    listaUsuarios.Add(new Usuario (rut,nombre,correo,contrasenia,tipo,estadoConvert) );
                }

                this.conector.CerrarConexion();
                return listaUsuarios;
            }
  
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


        public bool ModificarUsuario(string rutAModificar, string nombre, string correo,
            string contrasenia,string tipo,bool estado)
        {
            string consulta = "UPDATE usuario SET nombre =" + nombre + "," + "correo_electronico =" +
            correo + "," + "contrasenia =" + contrasenia + "," + "tipo =" + tipo + "," + "estado =" + estado +
            "WHERE rut =" + rutAModificar;
             MySqlDataReader reader = this.conector.RealizarConsulta(consulta);
            if (reader ==null)
            {
                this.conector.CerrarConexion();
                return false;
            }
            else
            {
                this.conector.CerrarConexion();
                return true;
            }
                
        }

        /*
         * Juan Abello
         * Envia los datos modificados a la consulta para ser cambiados en la base de datos
         * return true
         */ 
        public bool Modificar()
        {
            using (var Conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["appers"].ConnectionString))
            {
                Conn.Open();
                var SqlTran = Conn.BeginTransaction();
                

                try
                {
                    var Command = new MySqlCommand() { CommandText = "modificarUsuario", CommandType = CommandType.StoredProcedure };
                    //Setea el valor de los atributos del SP (procedimiento almacenado)
                    Command.Parameters.AddWithValue("rut", this.Rut);
                    Command.Parameters.AddWithValue("nombre", this.Nombre);
                    Command.Parameters.AddWithValue("contrasenia", this.Contrasenia);
                    Command.Parameters.AddWithValue("correoElectronico", this.CorreoElectronico);
                    Command.Parameters.AddWithValue("tipo", this.Tipo);
                    Command.Connection = Conn;
                    Command.Transaction = SqlTran;
                    Command.ExecuteNonQuery();
                    SqlTran.Commit();
                    Conn.Close();
                }
                catch (Exception ex)
                {
                    SqlTran.Rollback();
                }
            }
            return true;
        }

        /*
         * Juan Abello
         * Cambia es estado de deshabilitado a habilitado
         * rut
         */
        public void DeshabilitarUsuario(string rut)
        {
            this.Seleccionar(rut);
            if (this.Estado.Equals("1"))
            {
                using (var Conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["appers"].ConnectionString))
                {
                    Conn.Open();
                    var SqlTran = Conn.BeginTransaction();
                    try
                    {
                        var Command = new MySqlCommand() { CommandText = "deshabilitarUsuario", CommandType = CommandType.StoredProcedure };
                        Command.Parameters.AddWithValue("estado", "0");
                        Command.Connection = Conn;
                        Command.Transaction = SqlTran;
                        Command.ExecuteNonQuery();
                        SqlTran.Commit();
                        Conn.Close();
                    }
                    catch (Exception ex)
                    {
                        SqlTran.Rollback();
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
        public void Seleccionar(string Rut)
        {
            try
            {
                var DataSet = new DataSet();
                using (var Conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["appers"].ConnectionString))
                {
                    var Command = new MySqlCommand() { CommandText = "getRutUsuario", CommandType = CommandType.StoredProcedure };
                    Command.Parameters.AddWithValue("rut", Rut);
                    Conexion.Open();
                    Command.Connection = Conexion;
                    var Sqlda = new MySqlDataAdapter(Command);
                    Sqlda.Fill(DataSet);
                    Conexion.Close();
                }
                if (DataSet.Tables[0].Rows.Count > 0)
                {
                    this.CargarDatos(DataSet.Tables[0].Rows[0]);
                    return;
                }
                else
                {
                    this.Nombre = "Not Found";
                    this.CorreoElectronico = "Not Found";
                    this.Contrasenia = "Not Found";
                    this.Estado = false;
                    this.Tipo = "Not Found";
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
        public void CargarDatos(DataRow Dr)
        {
            this.Rut = Dr["rut"].ToString();
            this.CorreoElectronico = Dr["correo_electronico"].ToString();
            this.Nombre = Dr["nombre"].ToString();
            this.Contrasenia = Dr["contrasenia"].ToString();
            if(Dr["estado"].ToString() == "1")
            {
                this.Estado = true;
            }
            else
            {
                this.Estado = false;
            }
            this.Tipo = Dr["tipo"].ToString();
        }

        /*
         * Autor: Nicolás Hervias
         * Cambia el estado de habilitado a deshabilitado (suponiendo que 0 es deshabilitado y 1 es habilitado)
         * Retorna: vacío
         * Parámetros: rut (el rut del usuario)
         */
        public void HabilitarUsuario(string rut)
        {
            this.Seleccionar(rut);
            if (this.Estado.Equals("0"))
            {
                using (var Conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["appers"].ConnectionString))
                {
                    Conn.Open();
                    var SqlTran = Conn.BeginTransaction();
                    try
                    {
                        var Command = new MySqlCommand() { CommandText = "habilitarUsuario", CommandType = CommandType.StoredProcedure };
                        Command.Parameters.AddWithValue("estado", "1");
                        Command.Connection = Conn;
                        Command.Transaction = SqlTran;
                        Command.ExecuteNonQuery();
                        SqlTran.Commit();
                        Conn.Close();
                    }
                    catch (Exception ex)
                    {
                        SqlTran.Rollback();
                    }
                }
            }
            //else { }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
 * autor: Nicolás Hervias
 */ 

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

        /*
         * Autor: Nicolás Hervias
         * carga los datos de un usuario especigficado con su rut. Este método llama al método CargarDatos, donde
         * los datos cargados desde la base de datos son asignados a las variables de la clase.
         * Parámetros: rut (es el rut del usuario que se desea modificar)
         * Retorna: vacío
         */
        public void Seleccionar(string rut)
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
                    this.tipo = "Not Found";
                    this.estado = "Not Found";
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
         * autor: Nicolás Hervias
         * convierte los datos cargados desde la base de datos al tipo de la variable de la clase para ser asignados
         * parámetros: dr (una fila de la tabla de la base de datos)
         * retorna: vacío
         */
        public void CargarDatos(DataRow dr)
        {
            this.rut = dr["rut"].ToString();
            this.nombre = dr["nombre"].ToString();
            this.correo_electronico = dr["correo_electronico"].ToString();
            this.contrasenia = dr["contrasenia"].ToString();
            this.estado = dr["estado"].ToString();
            this.tipo = dr["tipo"].ToString();
        }
    }
}
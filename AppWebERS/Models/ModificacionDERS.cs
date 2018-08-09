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
    public class ModificacionDERS
    { 
        public int IdModificacion { get; set; }
        public double Version { get; set; }
        public int Ref_proyecto { get; set; }
        public DateTime Fecha { get; set; }
        public string RefUsuario { get; set; }
        public string Descripcion { get; set; }
        private ConectorBD Conector = ConectorBD.Instance;

        /*
         * Constructor vacío de la clase modificacionesDERS (se agrega para cualquier otro uso que se le de en un futuro).
         * 
         */
        public ModificacionDERS(){

        }

        /*
         * Constructor de la clase ModificacionDERS.
         * 
         * <param name="idModificacion">El id de modificación.</param>
         * <param name="version">La versión de la modificación.</param>
         * <param name="fecha">La fecha de la modificación.</param>
         * <param name="refUsuario">La referencia entre el usuario y la modificación.</param>
         * 
         */
        public ModificacionDERS(int idModificacion, double version, int ref_proyecto, DateTime fecha, string refUsuario, string descripcion)
        {
            this.IdModificacion = idModificacion;
            this.Version = version;
            this.Ref_proyecto = ref_proyecto;
            this.Fecha = fecha;
            this.RefUsuario = refUsuario;
            this.Descripcion = descripcion;
        }

        /**
         * Método para Crear una Modificacion
         * <returns>Retorna un boolean que indica el correcto registro de la modificacion.</returns>
         **/

        public bool Crear(int idProyecto)
        {
            string Values = "'" + this.IdModificacion + "','" + this.Version + "','" + idProyecto + "','" + this.Fecha + "','" + this.RefUsuario + "','" + this.Descripcion + "'";
            string Consulta = "INSERT INTO Modificacion_DERS (id_modificacion, version, ref_proyecto, fecha, ref_autor_modivicacion, descripcion) VALUES (" + Values + ");";

            if(this.Conector.RealizarConsultaNoQuery(Consulta))
            {
                this.Conector.CerrarConexion();
                return true;
            }
            else
            {
                this.Conector.CerrarConexion();
                return false;
            }
        }


        public List<ModificacionDERS> ListarHistorial(int id)
        {
            List<ModificacionDERS> Historial = new List<ModificacionDERS>();

            string Consulta = "BEGIN TRANSACTION; " +
                "SELECT Modificacion_DERS.id_modificacion, Modificacion_DERS.version, Modificacion_DERS.ref_proyecto, Modificacion_DERS.fecha, users.UserName, Modificacion_DERS.descripcion" +
                "FROM Modificacion_DERS, users " +
                "WHERE Modificacion_DERS.ref_autor_modificacion = users.Id " +
                "AND Modificacion_DERS.ref_proyecto = '" + id + "'; " +
                "COMMIT;";
            MySqlDataReader Reader = this.Conector.RealizarConsulta(Consulta);
            if(Reader == null)
            {
                this.Conector.CerrarConexion();
                return null;
            }
            else
            {
                while(Reader.Read())
                {
                    int id_modificacion = Reader.GetInt16(0);
                    double version = Reader.GetDouble(1);
                    int ref_proyecto = Reader.GetInt16(2);
                    DateTime fecha = Reader.GetDateTime(3);
                    string user_name = Reader.GetString(5);
                    string descripcion = Reader.GetString(6);
                    Historial.Add(new ModificacionDERS(id_modificacion, version, ref_proyecto, fecha, user_name, descripcion));
                }
                this.Conector.CerrarConexion();
                return Historial;
            }
        }

        /**
         * Método para listar una modificación específica
         * <returns>Retorna una modificación específica.</returns>
         **/

        public void ListarEspecifico(Proyecto proyecto) {
          
        }

        /**
         * Método para seleccionar una modificación 
         **/

        public void Seleccionar(int id) {

        }

        /**
         * Método para cargar datos
         **/

        public void CargarDatos(DataRow dr) {

        }
    }
}
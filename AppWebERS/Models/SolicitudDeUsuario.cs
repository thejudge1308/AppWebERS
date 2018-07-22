using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebERS.Models
{
    public class SolicitudDeUsuario
    {

        private ApplicationDbContext conexion = ApplicationDbContext.Create();

        public SolicitudDeUsuario()
        {

        }

        /**
         * Getter y Setter del nombre de un proyecto.
         * <param name = "Proyecto" > string con el nombre de un proyecto.</param>
         * <returns>Retorna el valor string del nombre.</returns>
         **/
        public string Proyecto { get; set; }
        /**
         * Getter y Setter del id de un proyecto.
         * <param name = "IdProyecto" > string con el id de un proyecto.</param>
         * <returns>Retorna el valor string del id de un proyecto.</returns>
         **/
        public string IdProyecto { get; set; }
        /**
         * Getter y Setter del nombre de un jefe proyecto.
         * <param name = "JefeProyecto" > string con el nombre de un jefe de proyecto.</param>
         * <returns>Retorna el valor string del nombre de jefe de proyecto.</returns>
         **/
        public string JefeProyecto { get; set; }

        /**
         * <author>Roberto Ureta</author>
         * Constructor de Solicitud de usuario.
         * 
         * <param name = "Proyecto" > string con el nombre de un proyecto.</param>
         * <param name = "IdProyecto" > string con el id de un proyecto.</param>
         * <param name = "JefeProyecto" > string con el nombre de un jefe de proyecto.</param>
         **/
        public SolicitudDeUsuario(string proyecto, string idProyecto, string jefeProyecto) {
            this.Proyecto = proyecto;
            this.IdProyecto = idProyecto;
            this.JefeProyecto = jefeProyecto;
        }


        /**
        * <author>Roberto Ureta</author>
        * <summary>
        * Obtiene una lista con las solicitudes de union a un proyecto que tiene un usuario.
        * </summary>
        * <param name="idUsuario">id del usuario para recuperar las solicitudes.</param>
        * <returns> lista con las solicitudes de un usuario. </returns>
        */
        public List<SolicitudDeUsuario> ObtenerSolicitudesDeUsuario(string idUsuario) {
            List<SolicitudDeUsuario> listaSolicitudes = new List<SolicitudDeUsuario>();
            string consulta = "SELECT solicitud_jefeproyecto_usuario.ref_proyecto FROM solicitud_jefeproyecto_usuario WHERE ref_destinario = '"+idUsuario+"' AND estado = 0;";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader != null)
            {
                while (reader.Read())
                {
                    string idProyecto = reader["ref_proyecto"].ToString();
                    string proyecto = ObtenerNombreProyecto(idProyecto);
                    string jefeProyecto = ObtenerNombreJefeProyecto(idProyecto);
                    listaSolicitudes.Add(new SolicitudDeUsuario() {Proyecto = proyecto , IdProyecto = idProyecto, JefeProyecto = jefeProyecto });
                }
            }
            this.conexion.EnsureConnectionClosed();
            return listaSolicitudes;
        }

        /**
        * <author>Roberto Ureta</author>
        * <summary>
        * Obtiene el nombre de un proyecto de acuerdo a su id.
        * </summary>
        * <param name="id">id del proyecto.</param>
        * <returns> string con el nombre del proyecto. </returns>
        */
        public string ObtenerNombreProyecto(string id) {
            ApplicationDbContext conexion1 = ApplicationDbContext.Create();
            string nombre = String.Empty;
            string consulta = "SELECT proyecto.nombre FROM proyecto WHERE id_proyecto = '" + id + "';";
            MySqlDataReader reader = conexion1.RealizarConsulta(consulta);
            if (reader != null)
            {
                reader.Read();
                nombre = reader["nombre"].ToString();
            }
            conexion1.EnsureConnectionClosed();
            return nombre;
        }

        /**
        * <author>Roberto Ureta</author>
        * <summary>
        * Obtiene el nombre del jefe de un proyecto de acuerdo al id de un proyecto.
        * </summary>
        * <param name="id">id del proyecto.</param>
        * <returns> string que contiene el nombre del jefe de proyecto asociado a un proyecto. </returns>
        */
        public string ObtenerNombreJefeProyecto(string id) {
            ApplicationDbContext conexion1 = ApplicationDbContext.Create();
            string nombre = String.Empty;
            string consulta = "SELECT users.UserName FROM users,(SELECT vinculo_usuario_proyecto.ref_usuario FROM vinculo_usuario_proyecto WHERE ref_proyecto = '"+id+"' AND rol = 'JEFEPROYECTO') AS T1 WHERE Id = T1.ref_usuario;";
            MySqlDataReader reader = conexion1.RealizarConsulta(consulta);
            if (reader != null)
            {
                reader.Read();
                nombre = reader["UserName"].ToString();
            }
            conexion1.EnsureConnectionClosed();
            return nombre;
        }

        public bool AceptarSolicitud(int idProyecto, string idUsuario)
        {
            string consultaUpdate = "UPDATE Solicitud_jefeproyecto_usuario SET estado = 1 WHERE ref_destinario='"+idUsuario+"' AND ref_proyecto="+idProyecto+" AND estado=0;";
            if (this.conexion.RealizarConsultaNoQuery(consultaUpdate))
            {
                string consultaUpdate2 = "INSERT INTO vinculo_usuario_proyecto (ref_usuario,ref_proyecto,rol) VALUES ('"+idUsuario+"',"+idProyecto+",'USUARIO');";
                if (this.conexion.RealizarConsultaNoQuery(consultaUpdate2))
                {
                    return true;
                }
            }
            return false;
        }
        
        public bool RechazarSolicitud(int idProyecto, string idUsuario)
        {
            string consultaUpdate = "UPDATE Solicitud_jefeproyecto_usuario SET estado = 2 WHERE ref_destinario='" + idUsuario + "' AND ref_proyecto='" + idProyecto + "' AND estado=0;";
            if (this.conexion.RealizarConsultaNoQuery(consultaUpdate))
            {
                return true;
            }
            return false;
        }
    }
}
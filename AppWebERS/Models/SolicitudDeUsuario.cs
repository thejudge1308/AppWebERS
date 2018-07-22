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


        public string Proyecto { get; set; }
        public string IdProyecto { get; set; }
        public string JefeProyecto { get; set; }
 

        public SolicitudDeUsuario(string proyecto, string idProyecto, string jefeProyecto) {
            this.Proyecto = proyecto;
            this.IdProyecto = idProyecto;
            this.JefeProyecto = jefeProyecto;
        }



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

        /**
         * 
         * <author>Diego Iturriaga</author>
         * <summary>
         * Metodo para registrar en la base de datos la solicitud aceptada por un usuario y crear
         * el vinculo con el respectivo proyecto al que este se ha unido.
         * </summary>
         * <param name="idProyecto">id del proyecto al cual se unira el usuario logeado en el sistema.</param>
         * <param name="idUsuario">id del usuario que acepto la solicitud para unirse a un proyecto.</param>
         * <returns>El metodo retorna True si se registran los cambios exitosamente. Retorna False si se 
         * genera algun error en el registro.</returns>
         * 
         **/
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

        /**
         * 
         * <author>Diego Iturriaga</author>
         * <summary>
         * Metodo para registrar en la base de datos la solicitud rechazada por un usuario.
         * </summary>
         * <param name="idProyecto">id del proyecto cuya solicitud fue rechazada por el usuario logeado en el sistema.</param>
         * <param name="idUsuario">id del usuario que rechaza la solicitud paraunirse  a un proyecto.</param>
         * <returns>El metodo retorna True si se registran los cambios exitosamente. Retorna False si se 
         * genera algun error en el registro.</returns>
         * 
         **/
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
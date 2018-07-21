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
            string nombre = String.Empty;
            string consulta = "SELECT proyecto.nombre FROM proyecto WHERE id_proyecto = '" + id + "';";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader != null)
            {
                reader.Read();
                nombre = reader["nombre"].ToString();
            }
            this.conexion.EnsureConnectionClosed();
            return nombre;
        }

        public string ObtenerNombreJefeProyecto(string id) {
            string nombre = String.Empty;
            string consulta = "SELECT users.UserName FROM users,(SELECT vinculo_usuario_proyecto.ref_usuario FROM vinculo_usuario_proyecto WHERE ref_proyecto = '"+id+"' AND rol = 'JEFEPROYECTO') AS T1 WHERE Id = T1.ref_usuario;";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader != null)
            {
                reader.Read();
                nombre = reader["UserName"].ToString();
            }
            this.conexion.EnsureConnectionClosed();
            return nombre;
        }
    }
}
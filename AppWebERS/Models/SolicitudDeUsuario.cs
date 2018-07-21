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
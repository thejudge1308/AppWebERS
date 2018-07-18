using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace AppWebERS.Controllers
{
    public class SolicitudDeProyectoController : Controller
    {

        //privatw conector solo para testing.
        private ConectorBD conector = ConectorBD.Instance;

        // GET: SolicitudDeProyecto
        public ActionResult Index()
        {
            return View();
        }

        /**
        * <author>Jose Nunnez, Manuel Gonzalez</author>
        * <summary>Es el action del boton Aceptar de cada solicitud listada</summary>
        * <param name="idUsuario"> El string con el id del usuario solicitante </param>
        * <param name="idProyecto"> El string con el id del proyecto al cual se desea unir</param>
        * <returns> </returns>
        */
        public ActionResult Aceptar( String idUsuario, String idProyecto) {
            if (verificarExistencia(idUsuario,idProyecto))
            {
                String insert = "INSERT INTO vinculo_usuario_proyecto(ref_usuario, ref_proyecto, rol) VALUES('" + @idUsuario + "', '" + @idProyecto + "', 'USUARIO')";
                conector.RealizarConsultaNoQuery(insert);
                String delete = "DELETE FROM solicitud_vinculacion_proyecto WHERE ref_solicitante='" + idUsuario + "' AND ref_proyecto='" + idProyecto + "'";
                conector.RealizarConsultaNoQuery(delete);
                return RedirectToAction("SolicitudDeProyecto", "JefeProyecto");
            }
            else
            {
                return RedirectToAction("SolicitudDeProyecto", "JefeProyecto");
            }
        }

        /**
        * <author>Jose Nunnez, Manuel Gonzalez</author>
        * <summary> Es el action del boton rechazar de cada solicitud listada</summary>
        * <param name="idUsuario"> El string con el id del usuario solicitante</param>
        * <param name="idProyecto"> El string con el id del proyecto al cual se desea unir</param>
        * <returns></returns>
        */
        public ActionResult Rechazar(String idUsuario, String idProyecto)
        {
            String consulta = "START TRANSACTION; " +
                "DELETE FROM solicitud_vinculacion_proyecto WHERE ref_solicitante='" + idUsuario + "' AND ref_proyecto='" + idProyecto + "';"
                +"COMMIT;";
            conector.RealizarConsultaNoQuery(consulta);
            return RedirectToAction("SolicitudDeProyecto", "JefeProyecto"); //Lo deje asi por mientras
        }

        /*
         *NOTA: despues de cada una de las acciones hay que eliminar la solicitud 
         * 
         */
        public ActionResult Volver() {
            return RedirectToAction("Detalles", "Proyecto", new { id = 2 });
        }

        private Boolean verificarExistencia(String idUsuario, String idProyecto)
        {
            String consulta = "SELECT FROM vinculo_usuario_proyecto WHERE ref_solicitante = '" +idUsuario+"'AND ref_proyecto= '"+idProyecto +"';";

            MySqlDataReader reader = this.conector.RealizarConsulta(consulta);

            if(reader!= null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
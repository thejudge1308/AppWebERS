using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using AppWebERS.Utilidades;

namespace AppWebERS.Controllers
{
    public class SolicitudDeProyectoController : Controller
    {

        //privatw conector solo para testing.
        private ApplicationDbContext conector = ApplicationDbContext.Create();

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
        public ActionResult Aceptar(String idUsuario, String idProyecto) {

            try { 
                String insert = "START TRANSACTION;" +
                    "INSERT INTO vinculo_usuario_proyecto(ref_usuario, ref_proyecto, rol) VALUES('" + idUsuario + "', '" + idProyecto + "', 'USUARIO');" +
                    "COMMIT;";
                conector.RealizarConsultaNoQuery(insert);

                String delete = "START TRANSACTION;" +
                "DELETE FROM solicitud_vinculacion_proyecto WHERE ref_solicitante='" + idUsuario + "' AND ref_proyecto='" + idProyecto + "';" +
                "COMMIT;";
                conector.RealizarConsultaNoQuery(delete);
                conector.EnsureConnectionClosed();
                this.EliminarSolicitudesPendientes(idProyecto,idUsuario);
                TempData["alerta"] = new Alerta("Se ha aceptado la solicitud", TipoAlerta.SUCCESS);

                return RedirectToAction("ListaUsuarios", "Proyecto", new { id = idProyecto });
             }catch(Exception e){
                TempData["alerta"] = new Alerta("ERROR: No se ha podido aceptar la solicitud", TipoAlerta.ERROR);
                return RedirectToAction("ListaUsuarios", "Proyecto", new { id = idProyecto });
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
            try
            {
                String consulta = "START TRANSACTION; " +
                    "DELETE FROM solicitud_vinculacion_proyecto WHERE ref_solicitante='" + idUsuario + "' AND ref_proyecto='" + idProyecto + "';"
                    + "COMMIT;";
                conector.RealizarConsultaNoQuery(consulta);
                conector.EnsureConnectionClosed();
                TempData["alerta"] = new Alerta("Se ha rechazado la solicitud", TipoAlerta.SUCCESS);
                return RedirectToAction("ListaUsuarios", "Proyecto", new { id = idProyecto }); 
            }
            catch (Exception e) {
                TempData["alerta"] = new Alerta("ERROR: No se ha podido rechazar la solicitud", TipoAlerta.ERROR);
                return RedirectToAction("ListaUsuarios", "Proyecto", new { id = idProyecto });
            }
        }

        /*
         *NOTA: despues de cada una de las acciones hay que eliminar la solicitud 
         * 
         */
        public ActionResult Volver(string idProyecto) {
            
            int value = Int32.Parse(idProyecto);           
            return RedirectToAction("Detalles", "Proyecto", new { id = value });
        }

        private Boolean verificarExistencia(String idUsuario, String idProyecto)
        {
            String consulta = "SELECT * FROM solicitud_vinculacion_proyecto WHERE ref_solicitante = '" +idUsuario+"'AND ref_proyecto= '"+idProyecto +"';";

            MySqlDataReader reader = this.conector.RealizarConsulta(consulta);

            if(reader!= null)
            {
                conector.EnsureConnectionClosed();
                return true;
            }
            else
            {
                conector.EnsureConnectionClosed();
                return false;
            }
        }
        /**
        * <author>Roberto Ureta-Ariel Cornejo-Diego Iturriaga</author>
        * <summary>
        * Elimina solicitudes de un usuario determinado en un proyecto determinado.
        * </summary>
        * <param name="idProyecto">Contiene un int con el id de un proyecto.</param>
        * <param name="idUsuario">Contiene un string que tiene el id de un usuario.</param>
        * <returns> true si se ejecuto la consulta, false en caso contrario.</returns>
        */
        public Boolean EliminarSolicitudesPendientes(string idProyecto, string idUsuario)
        {
            String consulta = "DELETE FROM solicitud_jefeproyecto_usuario WHERE ref_proyecto = " + idProyecto + " AND ref_destinario='" + idUsuario + "';" +
                                " DELETE FROM solicitud_vinculacion_proyecto WHERE ref_proyecto = " + idProyecto + " AND ref_solicitante = '" + idUsuario + "';";
            bool resultado = this.conector.RealizarConsultaNoQuery(consulta);
            return resultado;
        }
    }
}
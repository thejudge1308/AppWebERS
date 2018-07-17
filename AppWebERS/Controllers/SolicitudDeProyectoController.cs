using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWebERS.Controllers
{
    public class SolicitudDeProyectoController : Controller
    {
        // GET: SolicitudDeProyecto
        public ActionResult Index()
        {
            return View();
        }

        /**
        * <author>Jose Nunnez</author>
        * <summary>Es el action del boton Aceptar de cada solicitud listada</summary>
        * <param name="nombre"> El string con el id del usuario solicitante </param>
        * <returns> </returns>
        */
        public ActionResult Aceptar( String nombre) {
            return null; //lo deje asi nomas
        }

        /**
        * <author>Jose Nunnez</author>
        * <summary> Es el action del boton rechazar de cada solicitud listada</summary>
        * <param name="nombre"> El string con el id del usuario solicitante</param>
        * <returns></returns>
        */
        public ActionResult Rechazar(String nombre)
        {
            return null; //Lo deje asi por mientras
        }

        /*
         *NOTA: despues de cada una de las acciones hay que eliminar la solicitud 
         * 
         */
    }
}
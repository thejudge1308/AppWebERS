using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWebERS.Controllers
{
    public class ProyectoController : Controller
    {
       

        // GET: Proyecto/Detalles/5
        public ActionResult Detalles(int id)
        {        
            string UsuarioSolicitante = System.Web.HttpContext.Current.User.Identity.Name;
            return View();
        }

        [HttpPost]
        public ActionResult Detalles(FormCollection datos) {

            return View();
        }


        
    }
}

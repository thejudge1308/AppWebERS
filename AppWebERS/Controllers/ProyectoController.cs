using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AppWebERS.Controllers
{
    public class ProyectoController : Controller
    {
        [HttpGet]
        public ActionResult CrearProyecto() {
            return View();

        }
        [HttpPost]
        public ActionResult CrearProyecto(string nombre) {
            if (ModelState.IsValid) {
                Proyecto proyecto = new Proyecto();
                proyecto.Nombre = nombre;
                if(proyecto.RegistrarProyectoEnBd(proyecto))
                    ViewBag.Message = "Exito";
                else
                {
                    ViewBag.Message = "Error";
                }
                
            }

            return View();
        }

        /**public Proyecto crearProyecto() {
            
        }**/

    }
}

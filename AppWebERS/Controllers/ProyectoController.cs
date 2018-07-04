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
            ViewBag.Message = "Crear Proyecto";
            return View();

        }
        [HttpPost]
        public ActionResult CrearProyecto(string nombre) {
            if (ModelState.IsValid) {
                Proyecto proyectoTest = new Proyecto();
                proyectoTest.Nombre = nombre;
                if(proyectoTest.RegistrarProyectoEnBd(proyectoTest))
                    ViewBag.Message = "Exito";
                ViewBag.Message = "Error";
            }

            return View();
        }

        /**public Proyecto crearProyecto() {
            
        }**/

    }
}

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
        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Action GET que retorna una vista para la creacion de un proyecto.
         * </summary>
         * <returns> la vista cshtml asociada a CrearProyecto </returns>
         */
        [HttpGet]
        public ActionResult CrearProyecto() {
            return View();
        }
        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Action POST que retorna una vista luego de apretar el boton Crear Proyecto de la vista CrearProyecto.
         * </summary>
         * <param name="nombre">parametro ingresado desde la vista CrearProyecto</param>
         * <returns> la vista con el correspondiente mensaje de retroalimentacion. </returns>
         */
        [HttpPost]
        public ActionResult CrearProyecto(string nombre) {
            if (ModelState.IsValid) {
                Proyecto proyecto = new Proyecto();
                proyecto.Nombre = nombre;
                Proyecto proyectoNuevo = proyecto.CrearProyecto(0, nombre, String.Empty, String.Empty,
                                                String.Empty, String.Empty, String.Empty, String.Empty,
                                                String.Empty, String.Empty, String.Empty);
                if (proyectoNuevo != null)
                {
                    if (proyecto.RegistrarProyectoEnBd(proyectoNuevo))
                        ViewBag.Message1 = "Exito al crear Proyecto";
                    else
                        ViewBag.Message = "Error al crear proyecto";
                }
                else
                    ViewBag.Message = "Este nombre ya esta asociado a un proyecto";             
            }
            else
                ViewBag.Message = "Modelo no valido";
            return View();
        }

    }
}

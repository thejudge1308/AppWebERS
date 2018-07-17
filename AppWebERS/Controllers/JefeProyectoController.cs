using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWebERS.Controllers{
    public class JefeProyectoController : Controller{
        // GET: JefeProyecto
        public ActionResult Index(){
            return View();
        }

        public ActionResult SolicitudDeProyecto()
        {
            var idJefeProyecto = "Juan Perez";//NO SE COMO OBTENER EL NOMBRE DEL JEFE LOGEADO AQUI
            var sol = new SolicitudDeProyecto(idJefeProyecto);
            var modelo = sol.ListarTodos();
            return View(modelo);
        }

        public void ListaProyecto() {

        }

        public void AgregarUsuarioAProyecto(Proyecto proyecto) {

        }

        public void GenerarDERS(Proyecto proyecto) {

        }

    }
}
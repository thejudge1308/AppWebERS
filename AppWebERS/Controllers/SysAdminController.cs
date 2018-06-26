using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWebERS.Controllers
{
    public class SysAdminController : Controller
    {
        // GET: SysAdmin
        public ActionResult Index()
        {
            return View();
        }

        public void ListarProyectos() {

        }

        public void ListarUsuarios() {

        }

        public void CrearProyecto() {

        }

        public ActionResult CrearUsuario() {
            return View();
        }

        public void VerDetalleDelProyecto(Proyecto proyecto) {

        }

        public void AsignarJefeAProyecto(Proyecto proyecto, Usuario usuario) {

        }
    }
}
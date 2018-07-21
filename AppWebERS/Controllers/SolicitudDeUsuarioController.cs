using AppWebERS.Models;
using AspNet.Identity.MySQL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWebERS.Controllers
{
    public class SolicitudDeUsuarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListadoSolicitudUsuario()
        {
            SolicitudDeUsuario solicitud = new SolicitudDeUsuario();
            String id;
            using (var db = ApplicationDbContext.Create())
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                string s = User.Identity.GetUserId();
                ApplicationUser user = userManager.FindByIdAsync(s).Result;
                id = user.Id;

            }
            var list = solicitud.ObtenerSolicitudesDeUsuario(id);
            ViewBag.MiListadoSolicitudes = list;
            if (list.Count == 0)
            {
                ViewBag.vacio = true;
                return View();
            }
            ViewBag.vacio = false;
            return View();
        }

        [HttpGet]
        public ActionResult Aceptar(int idProyecto)
        {
            return null;
        }

        [HttpGet]
        public ActionResult Rechazar(int idProyecto)
        {
            return null;
        }

    }
}
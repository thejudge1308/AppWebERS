using AppWebERS.Models;
using AppWebERS.Utilidades;
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

        public ActionResult Aceptar(int idProyecto)
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
            if (solicitud.AceptarSolicitud(idProyecto,id))
            {
                TempData["alerta"] = new Alerta("Solicitud Aceptada Exitosamente", TipoAlerta.SUCCESS);
            }
            else
            {
                TempData["alerta"] = new Alerta("ERROR al Aceptar Solicitud", TipoAlerta.ERROR);
                
            }
            return RedirectToAction("ListadoSolicitudUsuario", "SolicitudDeUsuario");
        }

        public ActionResult Rechazar(int idProyecto)
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
            if (solicitud.RechazarSolicitud(idProyecto, id))
            {
                TempData["alerta"] = new Alerta("Solicitud Rechazada Exitosamente", TipoAlerta.SUCCESS);
            }
            else
            {
                TempData["alerta"] = new Alerta("ERROR al Rechazar Solicitud", TipoAlerta.ERROR);
            }
            return RedirectToAction("ListadoSolicitudUsuario", "SolicitudDeUsuario");
        }

    }
}
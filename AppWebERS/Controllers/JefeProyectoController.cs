using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNet.Identity.MySQL;
using Microsoft.AspNet.Identity;

namespace AppWebERS.Controllers{
    public class JefeProyectoController : Controller{
        // GET: JefeProyecto
        public ActionResult Index(){
            return View();
        }

        public ActionResult SolicitudDeProyecto()
        {
            string s;
            using (var db = ApplicationDbContext.Create())
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                s = User.Identity.GetUserId();
                ApplicationUser user = userManager.FindByIdAsync(s).Result;
                String rut = user.Rut;

            }
           
            var sol = new SolicitudDeProyecto(s);
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
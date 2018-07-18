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

        public ActionResult SolicitudDeProyecto(string id)
        {
            string s;
            using (var db = ApplicationDbContext.Create())
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                s = User.Identity.GetUserId();
                ApplicationUser user = userManager.FindByIdAsync(s).Result;
                String rut = user.Rut;

            }
            
            string idProyecto = id;
            SolicitudDeProyecto sol = new SolicitudDeProyecto(s,id);
            sol.ListarTodos();
            
            return View("SolicitudDeProyecto",sol);
        }

        public void ListaProyecto() {

        }

        public void AgregarUsuarioAProyecto(Proyecto proyecto) {

        }

        public void GenerarDERS(Proyecto proyecto) {

        }

    }
}
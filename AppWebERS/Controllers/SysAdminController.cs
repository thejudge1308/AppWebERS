using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace AppWebERS.Controllers
{
    public class SysAdminController : Controller
    {
        // GET: SysAdmin



        public ActionResult Index()
        {
            return View();
        }

        public void ListarProyectos()
        {

        }

        public void ListarUsuarios()
        {

        }

        public void CrearProyecto()
        {

        }

        [HttpGet]
        public ActionResult CrearUsuario()
        {

            RegisterViewModel modeloUsuario = new RegisterViewModel();

            return View(modeloUsuario);
        }
/*
        [HttpPost]
        public ActionResult CrearUsuario(RegisterViewModel modeloUsuario)
        {

            ModelState.Clear();
            string rut = modeloUsuario.Rut;
            string nombre = modeloUsuario.Nombre;
            string correo = modeloUsuario.Email;
            string contrasena = modeloUsuario.ConfirmPassword;
            contrasena = this.encriptarClave(contrasena);
            Usuario nuevoUsuario = new Usuario(rut, nombre, correo, contrasena, "USUARIO", true);

            if (nuevoUsuario.Crear()) {
                ViewBag.SuccessMessage = "Registro exitoso.";
                return View("CrearUsuario", new RegisterViewModel());
            }
            else{
                ViewBag.SuccessMessage = "No se pudo completar la solicitud.";
                return View("CrearUsuario", modeloUsuario);
            }


            
        }

*/

        public void VerDetalleDelProyecto(Proyecto proyecto)
        {

        }

        public void AsignarJefeAProyecto(Proyecto proyecto, Usuario usuario)
        {

        }

        private string encriptarClave(string original) {

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(original));
                return Convert.ToBase64String(data);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register() {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model) {
            if (ModelState.IsValid) {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                /*var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    return RedirectToAction("Index", "Home");
                }*/
                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
 

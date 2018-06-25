using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        [HttpPost]
        public ActionResult CrearUsuario(RegisterViewModel modeloUsuario)
        {

            ModelState.Clear();
            string rut = modeloUsuario.Rut;
            string nombre = modeloUsuario.Nombre;
            string correo = modeloUsuario.Email;
            string contrasena = modeloUsuario.ConfirmPassword;
            contrasena = this.encriptarClave(contrasena);
            Usuario nuevoUsuario = new Usuario(rut, nombre, correo, contrasena, "Default");
            
            nuevoUsuario.Crear();

            ViewBag.SuccessMessage = "Registro exitoso";
            return View("CrearUsuario", new RegisterViewModel());
        }



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
    }
}
 

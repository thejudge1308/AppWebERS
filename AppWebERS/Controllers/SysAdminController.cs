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
using MySql.Data.MySqlClient;
using System.Diagnostics;
using AppWebERS.Utilidades;
using System.Diagnostics;

namespace AppWebERS.Controllers
{
    public class SysAdminController : Controller
    {

        private int idProyecto;
        private ConectorBD conector = ConectorBD.Instance;
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

        public ActionResult AgregarUsuarioProyecto(int idProyecto)
        {
            Proyecto proyecto = new Proyecto().ObtenerProyectoPorID(idProyecto);
            this.idProyecto = idProyecto;
            List<Usuario> usuarios = this.listaDeUsuarios(idProyecto);
            ViewData["proyecto"] = proyecto;
            ViewData["usuarios"] = usuarios;
            Debug.WriteLine("iDpROY" + proyecto.IdProyecto);
            return View();
        }

        /*
       * Autor Fabian Oyarce
        * Metodo encargado de vincular un usuario a un proyecto
        * <param String rut>
        * <param String idProyecto>
       */
        private ConectorBD Conector = ConectorBD.Instance;
        [HttpGet]
        public ActionResult VincularUsuarioProyecto(String rut, String idProyecto)
        {

            string idUsuario = this.BuscaIdUsuarioPorRut(rut);
            string rol = "USUARIO";
            string consulta = "INSERT INTO vinculo_usuario_proyecto VALUES('" + idUsuario + "','" + idProyecto + "','" + rol + "')";
            Debug.Write(consulta);
            MySqlDataReader reader = this.Conector.RealizarConsulta(consulta);
            if (reader == null)
            {
                this.Conector.CerrarConexion();

            }
            else
            {
                TempData["alerta"] = new Alerta("Usuario vinculado", TipoAlerta.SUCCESS);
                while (reader.Read())
                {


                }

                this.Conector.CerrarConexion();
            }

            return RedirectToAction("ListarProyectos", "Proyecto");
           
        }

        /*
     * Autor Fabian Oyarce
      * Metodo encargado de buscar el id de un usuario dado un rut
      * <param String rut>
     */
        public string BuscaIdUsuarioPorRut(string rut)
        {

            string consulta = "SELECT users.Id FROM users WHERE users.Rut ='" + rut + "'";
            string idUsuario = null;

            MySqlDataReader reader = this.Conector.RealizarConsulta(consulta);
            if (reader == null)
            {
                this.Conector.CerrarConexion();

            }
            else
            {
               
                while (reader.Read())
                {
                    idUsuario = reader.GetString(0);

                }

                this.Conector.CerrarConexion();
            }
          
            return idUsuario;
        }

        public List<Usuario> listaDeUsuarios(int idProyecto)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            string consulta = "SELECT UserName, Rut, Email, Tipo FROM users WHERE Rut NOT IN (SELECT Rut FROM users, vinculo_usuario_proyecto, proyecto " +
                "WHERE vinculo_usuario_proyecto.ref_proyecto = '" + idProyecto + "' AND vinculo_usuario_proyecto.ref_usuario = users.Id) " +
                "AND NOT users.Tipo ='SYSADMIN' ";
            MySqlDataReader reader = this.conector.RealizarConsulta(consulta);

            if (reader == null)
            {
                this.conector.CerrarConexion();
                return null;
            }
            else
            {
                while (reader.Read())
                {
                    string nombre = reader.GetString(0);
                    string rut = reader.GetString(1);
                    string correo = reader.GetString(2);
                    string tipo = reader.GetString(3);

                    listaUsuarios.Add(new Usuario(rut, nombre, correo, tipo));
                }

                this.conector.CerrarConexion();
                return listaUsuarios;
            }

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
 

using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
namespace AppWebERS.Controllers {
    public class UsuarioController : Controller {
        /*
         * Juan Abello
         * Obtiene los datos de la vista del formulario para modificar la cuenta
         * rutUsuario
         * retorna el usuario con los datos modificados al modelo de usuario
         */

        [HttpGet]
        public ActionResult modificarCuenta(string rutUsuario)
        {
            Usuario u = new Usuario();
            u.rutUsuario = rutUsuario;
            return View(u);
        }

        /*
         * Juan Abello
         * llama a la funcion de modificar la cuenta de un usuario en el modelo de este
         * usuario
         * return RedirectToAction
         */

        [HttPost]
        public ActionResult modificarCuenta(Usuario usuario)
        {
            usuario.modificarUsuario();
            return RedirectToAction("Index");
        }



    }
        public ActionResult Index() {
            return View();
        }

        public void Crear() {

        }

        public void EditarProyecto() {

        }

        public void CrearRequisito() {

        }


        public void CrearCasoDeUso() {
        }

        public void CreaeActor(Proyecto  proyecto) {

        }

       
}
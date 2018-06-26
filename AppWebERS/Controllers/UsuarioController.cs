using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
namespace AppWebERS.Controllers
{
    public class UsuarioController : Controller
    {
        /*
         * Juan Abello
         * Obtiene los datos de la vista del formulario para modificar la cuenta
         * rutUsuario
         * retorna el usuario con los datos modificados al modelo de usuario
         */

        [HttpGet]
        public ActionResult ModificarCuenta(string rutUsuario,string nombre,string correoElectronico,bool estado,string contrasenia,string tipo)
        {
            Usuario u = new Usuario(rutUsuario,nombre,correoElectronico,contrasenia,tipo,estado);
            u.Rut = rutUsuario;
            return View(u);
        }

        /*
         * Juan Abello
         * llama a la funcion de modificar la cuenta de un usuario en el modelo de este
         * usuario
         * return RedirectToAction
         */

        [HttpPost]
        public ActionResult ModificarCuenta(Usuario usuario)
        {
            usuario.Modificar();
            return RedirectToAction("Index");
        }




        public ActionResult Index()
        {
            return View();
        }

        public void Crear()
        {

        }

        public void EditarProyecto()
        {

        }

        public void CrearRequisito()
        {

        }


        public void CrearCasoDeUso()
        {
        }

        public void CreaeActor(Proyecto proyecto)
        {

        }


    }
}


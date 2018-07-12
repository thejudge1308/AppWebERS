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


        [HttpPost]
        public ActionResult Modificar2(string rutUsuario, string nombre, string correoElectronico, bool estado, string contrasenia, string tipo)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Deshabilitar2(string rutUsuario, string nombre, string correoElectronico, bool estado, string contrasenia, string tipo)
        {
            return View();
        }
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


         /* <autor>Diego Matus</autor>
         * <summary>Metodo encargado de enviar la lista de usarios a la vista ListarUsuarios y mostrarla dicha
         * lista</summary>
         * <param void>
         * <returns> 
         * Retorna la vista correspodiente (ListarUsuarios).
         * </returns>
         * 
         */
        public ActionResult ListarUsuarios()
        {
            return View();
        }


        public ActionResult VistaUsuario()
        {
            return View();
        }






    }
}
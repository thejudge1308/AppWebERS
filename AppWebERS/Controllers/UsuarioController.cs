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
            System.Console.WriteLine("Rut Usuario: {0} - NombreUsuario: {1} - Correo Electrónico: {2} - estado {3} -  contraseña: {4} - tipo: {5}", rutUsuario, nombre, correoElectronico, estado, contrasenia, tipo);
            Usuario u = new Usuario(rutUsuario,nombre,correoElectronico,contrasenia,tipo,estado);
            u.Rut = rutUsuario;
            return View(u);
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
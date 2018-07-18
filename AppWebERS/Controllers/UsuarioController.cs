using AppWebERS.Models;
using AspNet.Identity.MySQL;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

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

        public void CrearActor(Proyecto proyecto)
        {

        }

        public ActionResult VistaUsuario()
        {
            return View();
        }

    }
}
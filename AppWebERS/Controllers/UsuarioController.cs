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
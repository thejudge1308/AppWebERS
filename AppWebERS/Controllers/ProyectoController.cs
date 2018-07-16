using AppWebERS.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace AppWebERS.Controllers
{
    public class ProyectoController : Controller
    {
        private ConectorBD conexion;

        // GET: Proyecto/Detalles/5
        [Authorize]
        public ActionResult Detalles(int id)
        {
            Proyecto proyecto = this.GetProyecto(id);
            //string UsuarioActual = System.Web.HttpContext.Current.User.Identity.Name; // pregunta el usuario actual
            var UsuarioActual = User.Identity.GetUserId();
           // Debug.WriteLine("Usuario actual: " + UsuarioActual);
           // Debug.WriteLine("Proyecto actual: " + proyecto);
           // Debug.WriteLine("Permiso: " + TipoDePermiso());
            ViewData["proyecto"] = proyecto;
            ViewData["permiso"] = TipoDePermiso(id);

            return View();
        }

        // POST: Proyecto/Detalles/5
        [HttpPost]
        [Authorize]
        public ActionResult Detalles(FormCollection datos) {
            //Captura de datos -> debe ser coherente al nombramiento del modelo

            Proyecto proyecto = new Proyecto();
            var idProyecto = datos["Id del Proyecto"];
            var nombre = datos["Nombre"];
            var proposito = datos["Propósito"];
            var alcance = datos["Alcance"];
            var contexto = datos["Contexto"];
            var definiciones = datos["Definición"];
            var acronimos = datos["Acrónimo"];
            var abreviaturas = datos["Abreviatura"];
            var referencias = datos["Referencia"];
            var ambienteOperacional = datos["Ambiente operacional"];
            var relacionProyectos = datos["Relación proyectos"];
            proyecto.ActualizarDatosProyecto(Int32.Parse(idProyecto), nombre, proposito, alcance, contexto, definiciones, acronimos, abreviaturas, referencias, ambienteOperacional, relacionProyectos);
            return View();
        }


        // GET: Proyecto/ListaUsuarios/5
        public ActionResult ListaUsuarios(int id) {
            Proyecto proyecto = this.GetProyecto(id);

            List<Usuario> usuarios = new Proyecto().GetListaUsuarios(id);

            //Debug.WriteLine("Permiso: " + TipoDePermiso());
            ViewData["proyecto"] = proyecto;
            ViewData["usuarios"] = usuarios;
            Debug.WriteLine("Lista de usuarios" + usuarios);
            ViewData["permiso"] = TipoDePermiso(id);
            return View();
        }

        // POST: Proyecto/ListaUsuarios/5
        [HttpPost]
        public ActionResult ListaUsuarios(FormCollection datos) {

            return View();
        }

        /**
        * Autor: Patricio Quezada
        * <param name = "id" > Id del proyecto.</param>
        * <returns>El proyecto con todos sus datos</returns>
        * 
        **/
        private Proyecto GetProyecto(int id) {
            return new Proyecto().ObtenerProyectoPorID(id);
        }


        /**
       * Autor: Patricio Quezada
       * <param name = "id" > id del proyecto</param>
       * <returns>El permiso para el tipo de usuario que ve el contenido</returns>
       * 
       **/
        private int TipoDePermiso(int id) {
            //Obtiene id del usuario de la sesion
            var UsuarioActual = User.Identity.GetUserId();
            int ModoVista = new Proyecto().ObtenerRolDelUsuario(UsuarioActual.ToString(),id);
            Debug.WriteLine(ModoVista + "jaskdjakdaksdjakdjakdj");
            return ModoVista;
        }
            
        }

    }


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
        private int id_proyecto;

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
        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Action GET que retorna una vista para la creacion de un proyecto.
         * </summary>
         * <returns> la vista cshtml asociada a CrearProyecto </returns>
         */
        [HttpGet]
        public ActionResult CrearProyecto() {
            Proyecto proyecto = new Proyecto();
            List<SelectListItem> lista = proyecto.ObtenerUsuarios();
            ViewBag.MiListadoUsuarios = lista;
            if (lista.Count == 0)
            {
                ViewBag.BoolLista = false;
            }
            else
                ViewBag.BoolLista = true;
            return View();
        }
        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Action POST que retorna una vista luego de apretar el boton Crear Proyecto de la vista CrearProyecto.
         * </summary>
         * <param name="nombre">parametro ingresado desde la vista CrearProyecto</param>
         * <returns> la vista con el correspondiente mensaje de retroalimentacion. </returns>
         */
        [HttpPost]
        public ActionResult CrearProyecto(string nombre,string usuario) {
            if (ModelState.IsValid) {
                Proyecto proyecto = new Proyecto();
                List<SelectListItem> lista = proyecto.ObtenerUsuarios();
                ViewBag.MiListadoUsuarios = lista;
                if (lista.Count == 0)
                {
                    ViewBag.BoolLista = false;
                }
                else
                    ViewBag.BoolLista = true;
                proyecto.Nombre = nombre;
                Proyecto proyectoNuevo = proyecto.CrearProyecto(0, nombre, String.Empty, String.Empty,
                                                String.Empty, String.Empty, String.Empty, String.Empty,
                                                String.Empty, String.Empty, String.Empty);
                if (proyectoNuevo != null)
                {
                    if (proyecto.RegistrarProyectoEnBd(proyectoNuevo))
                    {
                        if (proyecto.AsignarJefeProyecto(usuario, nombre))
                        {
                            ViewBag.Message1 = "Exito al crear Proyecto";
                        }
                        else {
                            ViewBag.Message1 = "";
                            ViewBag.Message = "Error al crear proyecto";
                        }

                    }
                    else
                    {
                        ViewBag.Message1 = "";
                        ViewBag.Message = "Error al crear proyecto";
                    }
                }
                else
                    ViewBag.Message = "Este nombre ya esta asociado a un proyecto";             
            }
            else
                ViewBag.Message = "Modelo no valido";
            return View();
        }
        /**
       * <author>Ariel Cornejo</author>
       * <summary>
       * Action GET que retorna una vista una vez se carga la pagina de asignar el jefe de proyecto.
       * </summary>
       * 
       * <returns> la vista con los dropDownList y en caso de que alguna de las listas este vacia se retornara un mensaje de error junto con deshabulutar el boton</returns>
       */
        [HttpGet]
        public ActionResult AsignarJefeProyecto()
        {
            Proyecto proyecto = new Proyecto();
            var list = proyecto.ObtenerProyectosSinJefe();
            var list2 = proyecto.ObtenerUsuarios();
            ViewBag.MiListadoProyectos = list;
            ViewBag.MiListadoUsuarios = list2;
            if (list.Count == 0)
            {
                ViewBag.listaVacia = true;
                ViewBag.MessageErrorProyectos = "No hay proyectos disponibles";
                return View();
            }
            if (list2.Count == 0)
            {
                ViewBag.listaVacia = true;
                ViewBag.MessageErrorProyectos = "No hay usuarios disponibles";
                return View();
            }
            ViewBag.listaVacia = false;
            return View();
        }
        /**
         * <author>Ariel Cornejo</author>
         * <summary>
         * Action POST que retorna una vista una vez se presiona el boton de la vista anterior.
         * </summary>
         *  <param name="DropDownListProyectos">parametro importado desde el dropdownList de proyectos, contiene el valor string seleccionado en este</param>
         *  <param name="DropDownListUsuarios">parametro importado desde el dropdownList de usuarios, contiene el valor string seleccionado en este</param>
         * <returns> la vista con los dropDownList y en caso de que alguna de las listas este vacia se retornara un mensaje de error junto con deshabilitar el boton</returns>
         */
        [HttpPost]
        public ActionResult AsignarJefeProyecto(String DropDownListProyectos,String DropDownListUsuarios)
        {
            Proyecto proyecto = new Proyecto();
            proyecto.AsignarJefeProyecto(DropDownListUsuarios, DropDownListProyectos);
            var list = proyecto.ObtenerProyectosSinJefe();
            var list2 = proyecto.ObtenerUsuarios();
            ViewBag.MiListadoProyectos = list;
            ViewBag.MiListadoUsuarios = list2;
            if (list.Count==0)
            {
                ViewBag.listaVacia = true;
                ViewBag.MessageErrorProyectos = "No hay proyectos disponibles";
                return View();
            }
            if (list2.Count==0)
            {
                ViewBag.listaVacia = true;
                ViewBag.MessageErrorProyectos = "No hay usuarios disponibles";
                return View();
            }
            ViewBag.listaVacia = false;
            return View();
        }
        /**
          * <author>Ariel Cornejo</author>
          * <summary>
          * Action GET que retorna una vista una vez se carga la pagina de modificar el jefe de proyecto.
          * </summary>
          * 
          * <returns> la vista con los dropDownList y en caso de que alguna de las listas este vacia se retornara un mensaje de error junto con deshabilitar el boton</returns>
          */
        [HttpGet]
        public ActionResult ModificarJefeProyecto(int id)
        {
            Proyecto proyecto = new Proyecto();
            this.id_proyecto = id;
            ViewBag.idProyecto = id;
            var list = proyecto.ObtenerUsuarios2(id);
            if (list.Count == 0)
            {
                ViewBag.MessageErrorProyectos = "No Hay Usuarios Disponibles";
                ViewBag.MiListadoUsuarios = list;
                ViewBag.listaVacia = true;
                return View();
            }
            ViewBag.MiListadoUsuarios = list;
            ViewBag.listaVacia = false;
            return View();
            
        }
        /**
          * <author>Diego Iturriaga</author>
          * <summary>
          * Action GET que retorna una redireccion a Detalles despues de ejecutar la modificacion de jefe.
          * </summary>
          * <param name="id">id del proyecto actual al cual se le modificara el jefe de proyecto</param>
          * <returns> Redireccion a la ventana Detalles</returns>
          */
        [HttpGet]
        public ActionResult ModificarJefeProyectoLogico(String rut, int id)
        {
            Proyecto proyecto = new Proyecto();
            proyecto.ModificarJefeProyecto(rut,id);
            return RedirectToAction("Detalles/"+id,"Proyecto");
        }
    }
}
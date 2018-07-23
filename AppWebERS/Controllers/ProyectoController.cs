using AppWebERS.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Microsoft.AspNet.Identity;
using AspNet.Identity.MySQL;
using Microsoft.AspNet.Identity.Owin;

namespace AppWebERS.Controllers
{
    public class ProyectoController : Controller
    {
        List<NombreProyecto> listaProyectosNombres = new List<NombreProyecto>();
        private int id_proyecto;
        private ConectorBD conexion;
        private ConectorBD Conector = ConectorBD.Instance;

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



        /**
         * <author>Matías Parra</author>
         * <summary>
         * Action POST que retorna una vista después se precionar el botón de guardar cambios en un proyecto.
         * </summary>
         * <returns> la vista de éxito. </returns>
         */
        // POST: Proyecto/Detalles/5
        public class ProyectoJsonRespuesta {
            public string id { set; get; }
            public string atributo { set; get; }
            public string valor { set; get; }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Detalles(ProyectoJsonRespuesta json) {
            //Captura de datos -> debe ser coherente al nombramiento del modelo
            Proyecto proyecto = new Proyecto();

            switch (json.atributo) {

                case "nombre":        
                    proyecto.ActualizarDatosProyecto(Int32.Parse(json.id), json.valor, json.atributo);
                    return Json(true, JsonRequestBehavior.AllowGet);
                    break;

                case "proposito":
                    proyecto.ActualizarDatosProyecto(Int32.Parse(json.id), json.valor, json.atributo);
                    return Json(true, JsonRequestBehavior.AllowGet);
                    break;

                case "alcance":
                    proyecto.ActualizarDatosProyecto(Int32.Parse(json.id), json.valor, json.atributo);
                    return Json(true, JsonRequestBehavior.AllowGet);
                    break;

                case "contexto":
                    proyecto.ActualizarDatosProyecto(Int32.Parse(json.id), json.valor, json.atributo);
                    return Json(true, JsonRequestBehavior.AllowGet);
                    break;

                case "definicion":
                    proyecto.ActualizarDatosProyecto(Int32.Parse(json.id), json.valor, json.atributo);
                    return Json(true, JsonRequestBehavior.AllowGet);
                    break;

                case "acronimo":
                    proyecto.ActualizarDatosProyecto(Int32.Parse(json.id), json.valor, json.atributo);
                    return Json(true, JsonRequestBehavior.AllowGet);
                    break;

                case "abreviatura":
                    proyecto.ActualizarDatosProyecto(Int32.Parse(json.id), json.valor, json.atributo);
                    return Json(true, JsonRequestBehavior.AllowGet);
                    break;

                case "referencia":
                    proyecto.ActualizarDatosProyecto(Int32.Parse(json.id), json.valor, json.atributo);
                    return Json(true, JsonRequestBehavior.AllowGet);
                    break;

                case "ambiente":
                    proyecto.ActualizarDatosProyecto(Int32.Parse(json.id), json.valor, json.atributo);
                    return Json(true, JsonRequestBehavior.AllowGet);
                    break;

                case "relacion":
                    proyecto.ActualizarDatosProyecto(Int32.Parse(json.id), json.valor, json.atributo);
                    return Json(true, JsonRequestBehavior.AllowGet);
                    break;
            }
            
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        
        [Authorize]
        // Get: Proyecto/infoProyecto/5
        public ActionResult infoProyecto(int id) {
            Proyecto proyecto = this.GetProyecto(id);
            return Json(proyecto, JsonRequestBehavior.AllowGet);
        }

        // GET: Proyecto/ListaUsuarios/5
        public ActionResult ListaUsuarios(int id) {
            Proyecto proyecto = this.GetProyecto(id);
            List<Usuario> usuarios = new Proyecto().GetListaUsuarios(id);
            ViewData["proyecto"] = proyecto;
            ViewData["usuarios"] = usuarios;
            ViewData["permiso"] = TipoDePermiso(id);
            return View();
        }

        // POST: Proyecto/ListaUsuarios/5
        [HttpPost]
        public ActionResult ListaUsuarios(FormCollection datos) {

            return View();
        }

        /**
         * <author>Juan Abello</author>
         * <summary>
         * LLama a la lista de proyectos y la envia a la vista.
         * </summary>
         * <returns> la vista cshtml asociada a  </returns>
         */
        public ActionResult ListarProyectos()
        {
            var model = ObtenerProyectos();
            return View(model);

        }

        public string ObtenerIdUsuarioActivo()
        {
            using (var Db = ApplicationDbContext.Create())
            {
                var UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(Db));
                string UsuarioSolicitante = base.User.Identity.GetUserId();
                ApplicationUser User = UserManager.FindByIdAsync(UsuarioSolicitante).Result;
                string IdUsuario = User.Id;
                return IdUsuario;
            }
        }

        /**
        * <author>Fabian Oyarce</author>
        * <summary>
        * obtiene los proyectos asociados y no asociados y los une a una sola lista
        * </summary>
        * <returns>lista proyectos nombres </returns>
        */
        public List<NombreProyecto> ObtenerProyectos()
        {

            ListaDeProyectosUsuario(ObtenerIdUsuarioActivo());
            ListaDeProyectoNoAsociados(ObtenerIdUsuarioActivo());
            return listaProyectosNombres;
         
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
            return ModoVista;
        }
        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Action GET que retorna una vista para la creacion de un proyecto.
         * </summary>
         * <returns> la vista cshtml asociada a NombreProyecto </returns>
         */
        [HttpGet]
        public ActionResult CrearProyecto() {
            String tipo;
            using (var db = ApplicationDbContext.Create())
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                string s = User.Identity.GetUserId();
                ApplicationUser user = userManager.FindByIdAsync(s).Result;
                tipo = user.Tipo;

            }
            if (tipo == "SYSADMIN")
            {
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
                            return RedirectToAction("ListarProyectos", "Proyecto");
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
        public ActionResult AsignarJefeProyecto(String DropDownListProyectos, String DropDownListUsuarios)
        {
            Proyecto proyecto = new Proyecto();
            proyecto.AsignarJefeProyecto(DropDownListUsuarios, DropDownListProyectos);
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

        /*
         * Autor: Nicolás Hervias
         * Envía una solicitud para unirse al proyecto seleccionado (esta se guarda en la BD)
         * Parámetros: PosProyecto. Es la posición que tiene el proyecto en la lista de proyectos
         */
        [HttpGet]
        public ActionResult AgregarUsuarioAProyecto(string proyecto1)
        {
            
            //int PosProyecto = Int32.Parse(proyecto1);
            //List<string> ListaProyectos = ListaProyectosIds();
            //string IdProyectoAUnirse = ListaProyectos[PosProyecto];
            string UsuarioSolicitanteRut = ObtenerIdUsuarioActivo();
           
            //proyecto1 = "1";
            string Values = "'" +proyecto1 + "','" + UsuarioSolicitanteRut + "'";
            string Consulta = "INSERT INTO solicitud_vinculacion_proyecto (ref_proyecto,ref_solicitante) VALUES (" + Values + ");";
            
            if (this.Conector.RealizarConsultaNoQuery(Consulta))
            {
                this.Conector.CerrarConexion();
                ViewBag.Message = "Solicitud enviada";
            }
            else
            {
                this.Conector.CerrarConexion();
            }

            return View();
        }

       

        /*
         * Autor: Nicolás Hervias
         * Crea una lista de ids de todos los proyectos
         * Parametros: N/A
         */
         [HttpGet]
        public List<string> ListaProyectosIds()
        {
            List<String> ListaProyectos = new List<String>();
            string Consulta = "SELECT id_proyecto FROM proyecto";
            MySqlDataReader reader = this.Conector.RealizarConsulta(Consulta);
            if (reader == null)
            {
                this.Conector.CerrarConexion();
                return null;
            }
            else
            {
                while (reader.Read())
                {
                    string Id_proyecto = reader.ToString();
                    ListaProyectos.Add(Id_proyecto);
                }
                this.Conector.CerrarConexion();
                return ListaProyectos;
            }
        }

        public ActionResult InterfazUsuario()
        {
            var model = ObtenerProyectos();
            return View(model);

        }

        /*
        * Autor Juan Abello
        * Metodo encargado de obtener los nombres de los proyectos en los que se encuentra un usuario,guardarlos en una lista y retornar esta.
        * <param String rut>
        * <returns> listaProyectosNombres 
        */
        public List<NombreProyecto> ListaDeProyectosUsuario(string id)
        {
            string consulta = "SELECT proyecto.nombre, proyecto.id_proyecto FROM proyecto, users, vinculo_usuario_proyecto " +
                               "WHERE users.id = '" + id + "' AND vinculo_usuario_proyecto.ref_proyecto = " +
                               "proyecto.id_proyecto AND vinculo_usuario_proyecto.ref_usuario = users.id";
            MySqlDataReader reader = this.Conector.RealizarConsulta(consulta);
            if (reader == null)
            {
                this.Conector.CerrarConexion();
                listaProyectosNombres.Add(new NombreProyecto("mitad", "id"));
                return listaProyectosNombres;
                //return null;
            }
            else
            {
                while (reader.Read())
                {

                    string Nombre = reader.GetString(0);
                    string Id = reader.GetString(1);
                    listaProyectosNombres.Add(new NombreProyecto(Nombre,Id));
                }

                this.Conector.CerrarConexion();
                listaProyectosNombres.Add(new NombreProyecto("mitad","id"));
                return listaProyectosNombres;
            }
        }

        /*
         * Autor: Nicolás Hervias
         * Obtiene el rut del usuario actual
         * Parámetros: N/A
         * Retorna: string (rut)
         */
        public string ObtenerRutUsuarioActivo()
        {
            using (var Db = ApplicationDbContext.Create())
            {
                var UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(Db));
                string UsuarioSolicitante = base.User.Identity.GetUserId();
                ApplicationUser User = UserManager.FindByIdAsync(UsuarioSolicitante).Result;
                String UsuarioSolicitanteRut = User.Rut;
                return UsuarioSolicitanteRut;
            }
        }


        /*
        * Autor Juan Abello
        * Metodo encargado de obtener los nombres de los proyectos en los que se no encuentra un usuario,guardarlos en una lista y retornar esta.
        * <param String rut>
        * <returns> listaProyectosNombres 
        */
        public List<NombreProyecto> ListaDeProyectoNoAsociados(string id)
        {
            string consulta = "SELECT Proyecto.nombre ,proyecto.id_proyecto FROM Proyecto where Proyecto.nombre NOT IN" +
                              "(SELECT Proyecto.nombre FROM Proyecto, users, vinculo_usuario_proyecto " +
                              "WHERE users.id ='"+id+"'  AND Vinculo_usuario_proyecto.ref_proyecto = Proyecto.id_proyecto AND Vinculo_usuario_proyecto.ref_usuario = users.id)";

            MySqlDataReader reader = this.Conector.RealizarConsulta(consulta);
            if (reader == null)
            {
                this.Conector.CerrarConexion();
                return null;
            }
            else
            {
                while (reader.Read())
                {

                    string Nombre = reader.GetString(0);
                    string Id = reader.GetString(1);
                    listaProyectosNombres.Add(new NombreProyecto(Nombre,Id));
                }

                this.Conector.CerrarConexion();
                return listaProyectosNombres;
            }
        }


    }
}
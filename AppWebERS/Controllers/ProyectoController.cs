﻿using AppWebERS.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using AppWebERS.Utilidades;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Microsoft.AspNet.Identity;
using AspNet.Identity.MySQL;
using Microsoft.AspNet.Identity.Owin;
using AppWebERS.Utilidades;
using System.IO;


namespace AppWebERS.Controllers
{
    public class ProyectoController : Controller
    {
        List<NombreProyecto> listaProyectosNombres = new List<NombreProyecto>();
        List<NombreProyecto> listaTodosLosProyectosNombres = new List<NombreProyecto>();
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
        public FileResult ExportarPDF(int id) {
            Proyecto proyecto = this.GetProyecto(id);

            string fecha =  DateTime.Now.ToString();
            String html = "<html> <head> <style> body { margin: 2cm; } .logo { font-size: 40px; font-weigth: bold; } .titulo { text-align: center; } .fecha { margin-left: 20px; } .espacio-izq { margin-left: 20px; } table td{ font-size: 18px; padding-bottom: 15px; } </style> </head> <body> <table> <tr> <td class=\"logo\">AppWebERS</td> <td </tr> </table> <h1 class=\"titulo\">Detalles de proyecto</h1> <hr> <p class=\"fecha\">Fecha: " + fecha +"</p> <hr> <table class=\"espacio-izq\"> <tr> <td>Nombre proyecto</td> <td>: " + proyecto.Nombre + "</td> </tr> <tr> <td>Proposito</td> <td>: " + proyecto.Proposito + "</td> </tr> <tr> <td>Alcance</td> <td>: " + proyecto.Alcance + "</td> </tr> <tr> <td>Contexto</td> <td>: " + proyecto.Contexto + "</td> </tr> <tr> <td>Definiciones</td> <td>: " + proyecto.Definiciones + "</td> </tr> <tr> <td>Acronimos</td> <td>: "+ proyecto.Acronimos + "</td> </tr> <tr> <td>Abreviaturas</td> <td>: " + proyecto.Abreviaturas + "</td> </tr> <tr> <td>Referencias</td> <td>: " + proyecto.Referencias + "</td> </tr> <tr> <td>Ambiente operacional</td> <td>: " + proyecto.AmbienteOperacional + "</td> </tr> <tr> <td>Relacion con otros proyectos</td> <td>: " + proyecto.RelacionProyectos +  "</td> </tr> </table> </body> </html>";
            String html2 = "<h1>Texto</h1> <p> de</p> <p><sup><strong>prueba</strong></sup></p> <p><em>para</em></p> <h2><s>probar</s></h2> <p><br></p> <ol> <li>el</li> </ol> <p><sub>formato</sub></p> <p><span>pdf</span></p> <p><span>es</span></p> <p><span style=\"background - color: red; \">resposive</span></p> <p><span style=\"color: yellow; background - color: green; \">porsia</span></p> <p>Fin</p>";
            

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfBytes = htmlToPdf.GeneratePdf(html);
            MemoryStream ms = new MemoryStream(pdfBytes);
           

            return File(ms, "application/pdf"); ;
        }

        


        // GET: Proyecto/ListaUsuarios/5
        public ActionResult ListaUsuarios(int id) {
            Proyecto proyecto = this.GetProyecto(id);
            List<Usuario> usuarios = new Proyecto().GetListaUsuarios(id);
            List<SolicitudDeProyecto> solicitudes = new Proyecto().GetSolicitudesProyecto(id);
            //Debug.WriteLine("Permiso: " + TipoDePermiso());
            ViewData["proyecto"] = proyecto;
            ViewData["usuarios"] = usuarios;
            ViewData["solicitudes"] = solicitudes;
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
         * <author>Juan Abello</author>
         * <summary>
         * LLama a la lista de proyectos y la envia a la vista.
         * </summary>
         * <returns> la vista cshtml asociada a  </returns>
         */
        public ActionResult ListarProyectos()
        {
            String TipoUsuario = ObtenerTipoUsuarioActivo();
            

            if (TipoUsuario.Equals(Proyecto.SysAdmin_RolBD))
            {
                var model = ListaDeTodosLosProyectos();
                ViewData["usuario_actual"] = Proyecto.SysAdmin_RolBD;
                return View(model);
            }
            else
            {
                var model = ObtenerTodosLosProyectos();
                return View(model);
            }
           
            

        }


        /**
      * <author>Fabian Oyarce</author>
      * <summary>
      * obtiene los proyectos asociados y no asociados y los une a una sola lista
      * </summary>
      * <returns>lista proyectos nombres </returns>
      */
        public List<NombreProyecto> ObtenerTodosLosProyectos()
        {

            ListaDeProyectosAsociados(ObtenerIdUsuarioActivo());
            ListaDeProyectoNoAsociados(ObtenerIdUsuarioActivo());
            return listaProyectosNombres;

        }

        /*
    * Autor Fabian Oyarce
    * Metodo encargado de obtener los nombres de todos los proyectos.
    * <param String rut>
    * <returns> listaProyectosNombres 
    */
        public List<NombreProyecto> ListaDeTodosLosProyectos()
        {

            string consulta = "SELECT proyecto.nombre, proyecto.id_proyecto, proyecto.estado " +
                                "FROM proyecto";
            int bandera = 0;
            MySqlDataReader reader = this.Conector.RealizarConsulta(consulta);
            if (reader == null)
            {
                this.Conector.CerrarConexion();
                listaTodosLosProyectosNombres.Add(new NombreProyecto("mitad", "id","estado"));
                return listaTodosLosProyectosNombres;
                //return null;
            }
            else
            {
                while (reader.Read())
                {

                    if (bandera == 0)
                    {
                        listaTodosLosProyectosNombres.Add(new NombreProyecto("SYSADMIN", "-99","estado"));
                        bandera = 1;

                    }
                    
                    string Nombre = reader.GetString(0);
                    string Id = reader.GetString(1);
                    string Estado = reader.GetString(2);
                    listaTodosLosProyectosNombres.Add(new NombreProyecto(Nombre, Id,Estado));
                }

                this.Conector.CerrarConexion();
                listaTodosLosProyectosNombres.Add(new NombreProyecto("mitad", "id","estado"));
                return listaTodosLosProyectosNombres;
            }
        }


        /*
        * Autor Juan Abello
        * Metodo encargado de obtener los nombres de los proyectos en los que se encuentra un usuario,guardarlos en una lista y retornar esta.
        * <param String rut>
        * <returns> listaProyectosNombres 
        */
        public List<NombreProyecto> ListaDeProyectosAsociados(string id)
        {

            string estado = "HABILITADO";
            string consulta = "SELECT proyecto.nombre, proyecto.id_proyecto, proyecto.estado FROM proyecto, users, vinculo_usuario_proyecto " +
                               "WHERE proyecto.estado =  '" + estado + "' AND users.id = '" + id + "' AND vinculo_usuario_proyecto.ref_proyecto = " +
                               "proyecto.id_proyecto AND vinculo_usuario_proyecto.ref_usuario = users.id";
            MySqlDataReader reader = this.Conector.RealizarConsulta(consulta);
            if (reader == null)
            {
                this.Conector.CerrarConexion();
                listaProyectosNombres.Add(new NombreProyecto("mitad", "id","estado"));
                return listaProyectosNombres;
                //return null;
            }
            else
            {
                while (reader.Read())
                {

                    string Nombre = reader.GetString(0);
                    string Id = reader.GetString(1);
                    string Estado = reader.GetString(2);
                    listaProyectosNombres.Add(new NombreProyecto(Nombre, Id,Estado));
                }

                this.Conector.CerrarConexion();
                listaProyectosNombres.Add(new NombreProyecto("mitad", "id","estado"));
                return listaProyectosNombres;
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
            string estado = "HABILITADO";
            string consulta = "SELECT Proyecto.nombre ,proyecto.id_proyecto ,proyecto.estado FROM Proyecto where  proyecto.estado = '" + estado + "' AND " +
                              "Proyecto.nombre NOT IN" +
                              "(SELECT Proyecto.nombre FROM Proyecto, users, vinculo_usuario_proyecto " +
                              "WHERE users.id ='" + id + "'  AND Vinculo_usuario_proyecto.ref_proyecto = Proyecto.id_proyecto AND Vinculo_usuario_proyecto.ref_usuario = users.id)";

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
                    string Estado = reader.GetString(2);
                    listaProyectosNombres.Add(new NombreProyecto(Nombre, Id,Estado));
                }

                this.Conector.CerrarConexion();
                return listaProyectosNombres;
            }
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

      
        public ActionResult AgregarUsuarioProyecto(int id)
        {
            return RedirectToAction("AgregarUsuarioProyecto","SysAdmin", new { idProyecto = id});
        }

        public ActionResult InvitarUsuario(int id)
        {
            return RedirectToAction("InvitarUsuario", "JefeProyecto", new { idProyecto = id });
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
                            TempData["alerta"] = new Alerta("Exito al crear Proyecto", TipoAlerta.SUCCESS);
                            return RedirectToAction("ListarProyectos", "Proyecto");
                        }
                        else {
                            TempData["alerta"] = new Alerta("Error al crear Proyecto", TipoAlerta.ERROR);
                        }

                    }
                    else
                    {
                        TempData["alerta"] = new Alerta("Error al crear Proyecto", TipoAlerta.ERROR);
                    }
                }
                else
                   TempData["alerta"] = new Alerta("Este nombre ya esta asociado a un proyecto", TipoAlerta.ERROR);
            }
            else
                TempData["alerta"] = new Alerta("Modelo no valido", TipoAlerta.ERROR);
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
        public ActionResult AgregarUsuarioAProyecto(int proyecto1)
        {

            //int PosProyecto = Int32.Parse(proyecto1);
            //List<string> ListaProyectos = ListaProyectosIds();
            //string IdProyectoAUnirse = ListaProyectos[PosProyecto];
            TempData["alerta"] = new Alerta("Solicitud enviada", TipoAlerta.SUCCESS);
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

            return RedirectToAction("ListarProyectos", "Proyecto");
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
        
       
        [HttpGet]
        public ActionResult Requisito(int id)
        {
            ViewBag.IdProyecto = id;
            Requisito requisito = new Requisito();
            return View(requisito);
        }
        //ATENCION: FORMTATO FECHA: AAAA-MM-DD
        [HttpPost]
        public ActionResult IngresarRequisito(string idRequisito, string nombre, string descripcion, string prioridad, string fuente,
            string estabilidad, string estado, string tipoUsuario, string tipoRequisito, string medida, string escala,
            string fecha, string incremento, string tipo, string idProyecto)
        {
            Requisito requisito = new Requisito(idRequisito, nombre, descripcion, prioridad, fuente, estabilidad, estado,
                tipoUsuario, tipoRequisito, medida, escala, fecha, incremento, tipo);
            int id = Int32.Parse(idProyecto);
            if (requisito.RegistrarRequisito(id))
            {
                TempData["alerta"] = new Alerta("Exito al crear Requisito", TipoAlerta.SUCCESS);
                return RedirectToAction("Detalles/" + id, "Proyecto");
                
            }
            else
            {
                TempData["alerta"] = new Alerta("ERROR al crear Requisito", TipoAlerta.ERROR);
            }
            return RedirectToAction("Requisito/" + id, "Proyecto");
        }


        /*
       * Autor Fabian Oyarce
       * Metodo encargado de cambiar el estado de un proyecto a deshabilitado
       * <param String id>
       */
        [HttpGet]
        public ActionResult DeshabilitarProyecto(string id)
        {
            //Popups estado modificado
            TempData["alerta"] = new Alerta("Estado Modificado", TipoAlerta.SUCCESS);

            string nuevoEstado = "DESHABILITADO";
            string consulta = "UPDATE proyecto SET estado = '" + nuevoEstado + "'" +
                               "WHERE (id_proyecto ='" + id + "') ";

            MySqlDataReader reader = this.Conector.RealizarConsulta(consulta);
            if (reader == null)
            {
                this.Conector.CerrarConexion();
                
            }
            else
            {
                while (reader.Read())
                {

                    
                }

                this.Conector.CerrarConexion();
            }

           return RedirectToAction( "ListarProyectos", "Proyecto");
        }


        /*
      * Autor Fabian Oyarce
      * Metodo encargado de cambiar el estado de un proyecto a habilitado
      * <param String id>
      */
        [HttpGet]
        public ActionResult HabilitarProyecto(string id)
        {
            TempData["alerta"] = new Alerta("Estado Modificado", TipoAlerta.SUCCESS);
            string nuevoEstado = "HABILITADO";
            string consulta = "UPDATE proyecto SET estado = '" + nuevoEstado + "'" +
                               "WHERE (id_proyecto ='" + id + "') ";

            MySqlDataReader reader = this.Conector.RealizarConsulta(consulta);
            if (reader == null)
            {
                this.Conector.CerrarConexion();
                //Popup error
            }
            else
            {
                while (reader.Read())
                {

                    //Popups estado modificado
                }

                this.Conector.CerrarConexion();
            }

            return RedirectToAction("ListarProyectos", "Proyecto");
        }


        /*
      * Autor Fabian Oyarce
      * Metodo encargado de obtner el rol de un usuario activo
      * <param String id>
      */
        public string ObtenerTipoUsuarioActivo()
        {
            using (var Db = ApplicationDbContext.Create())
            {
                var UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(Db));
                string UsuarioSolicitante = base.User.Identity.GetUserId();
                ApplicationUser User = UserManager.FindByIdAsync(UsuarioSolicitante).Result;
                string TipoUsuario = User.Tipo;
                return TipoUsuario;
            }
        }


        /*
        * Autor Fabian Oyarce
         * Metodo encargado de vincular un usuario a un proyecto
         * <param String id>
        */
        [HttpGet]
        public ActionResult VincularUsuarioAProyecto(string rutUsuario,int idProyecto )
        {
            string idUsuario = this.ObtenerIdPorRut(rutUsuario);

            string consulta = "START TRANSACTION;"+
                "INSERT INTO vinculo_usuario_proyecto (ref_usuario, ref_proyecto, rol) VALUES('" + idUsuario + "','" + idProyecto + "','USUARIO');"+
                "COMMIT;";

            this.Conector.RealizarConsultaNoQuery(consulta);
            this.Conector.CerrarConexion();

            return RedirectToAction("Detalles", "Proyecto", new { id = idProyecto });
        }

        


        /*
     * Autor Fabian Oyarce
      * Metodo encargado de solicitar vincular un usuario a un proyecto
      * <param String id>
     */
        [HttpGet]
        public ActionResult SolicitarVincularUsuarioAProyecto(string rutUsuario, int idProyecto)
        {

            
            string UsuarioSolicitanteRut = ObtenerIdUsuarioActivo();
            string idUsuario = this.ObtenerIdPorRut(rutUsuario);
            string Values = "'" + idProyecto + "','" + idUsuario + "'";
            string Consulta = "INSERT INTO solicitud_vinculacion_proyecto (ref_proyecto,ref_solicitante) VALUES (" + Values + ");";

            if (this.Conector.RealizarConsultaNoQuery(Consulta) == true)
            {
                this.Conector.CerrarConexion();
                ViewBag.Message = "Solicitud enviada";
                TempData["alerta"] = new Alerta("Solicitud enviada", TipoAlerta.SUCCESS);
            }
            else
            {
                this.Conector.CerrarConexion();
            }

            return RedirectToAction("ListarProyectos", "Proyecto");
        }

        public ActionResult SolicitudDeProyecto(int id)
        {
            string s;
            using (var db = ApplicationDbContext.Create())
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                s = User.Identity.GetUserId();
                ApplicationUser user = userManager.FindByIdAsync(s).Result;
                String rut = user.Rut;

            }

            
            SolicitudDeProyecto sol = new SolicitudDeProyecto(s, id);
            sol.listaSolicitudes = new Proyecto().GetSolicitudesProyecto(id);

            return View("SolicitudDeProyecto", sol);
        }

        private string ObtenerIdPorRut(string rut)
        {
            string value = "";
            string consulta = "SELECT users.Id FROM users WHERE users.Rut = '" + rut + "'";
            MySqlDataReader reader = this.Conector.RealizarConsulta(consulta);
            if(reader!= null)
            {
                while(reader.Read())
                {
                    value = reader[0].ToString();
                }
                Conector.CerrarConexion();
            }
             return value;
        }
    }

   
}
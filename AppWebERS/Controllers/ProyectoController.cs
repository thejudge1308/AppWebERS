using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.AspNet.Identity;
using AspNet.Identity.MySQL;
using Microsoft.AspNet.Identity.Owin;

namespace AppWebERS.Models
{
    public class ProyectoController : Controller
    {
        private ConectorBD Conector = ConectorBD.Instance;

        

        List<NombreProyecto> listaProyectosNombres = new List<NombreProyecto>();
        // GET: Proyecto

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

        public int ObtenerIdUsuarioActivo()
        {
            using (var Db = ApplicationDbContext.Create())
            {
                var UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(Db));
                string UsuarioSolicitante = base.User.Identity.GetUserId();
                ApplicationUser User = UserManager.FindByIdAsync(UsuarioSolicitante).Result;
                int IdUsuario = Int32.Parse(User.Id);
                return IdUsuario;
            }
        }

        public List<NombreProyecto> ObtenerProyectos()
        {

            NombreProyecto transicion = new NombreProyecto("mitad");
            List<NombreProyecto> ListaAsociados = ListaDeProyectosUsuario(ObtenerIdUsuarioActivo());
            List<NombreProyecto> ListaNoAsociados = ListaDeProyectoNoAsociados(ObtenerIdUsuarioActivo());
            List<NombreProyecto> ListaProyectosCompleta = new List<NombreProyecto>();

            for(int i=0;i<ListaAsociados.Count+1;i++)
            {
                
                if(ListaProyectosCompleta.Count<ListaAsociados.Count)
                {
                    ListaProyectosCompleta.Add(ListaAsociados[i]);
                }

                if(ListaProyectosCompleta.Count==ListaAsociados.Count)
                {
                    ListaProyectosCompleta.Add(transicion);
                }

                
                Console.Write(ListaProyectosCompleta[i]);
            }

            for(int i=0;i<ListaNoAsociados.Count;i++)
            {
                ListaProyectosCompleta.Add(ListaNoAsociados[i]);
            }
            return ListaProyectosCompleta;
        }

        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Action GET que retorna una vista para la creacion de un proyecto.
         * </summary>
         * <returns> la vista cshtml asociada a NombreProyecto </returns>
         */
        [HttpGet]
        public ActionResult CrearProyecto()
        {
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
        public ActionResult CrearProyecto(string nombre)
        {
            if (ModelState.IsValid)
            {
                Proyecto proyecto = new Proyecto();
                proyecto.Nombre = nombre;
                Proyecto proyectoNuevo = proyecto.CrearProyecto(0, nombre, String.Empty, String.Empty,
                                                String.Empty, String.Empty, String.Empty, String.Empty,
                                                String.Empty, String.Empty, String.Empty);
                if (proyectoNuevo != null)
                {
                    if (proyecto.RegistrarProyectoEnBd(proyectoNuevo))
                        ViewBag.Message1 = "Exito al crear Proyecto";
                    else
                        ViewBag.Message = "Error al crear proyecto";
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
        public ActionResult ModificarJefeProyecto()
        {
            Proyecto proyecto = new Proyecto();
            var list = proyecto.ObtenerProyectos();
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
        public ActionResult ModificarJefeProyecto(String DropDownListProyectos, String DropDownListUsuarios)
        {
            Proyecto proyecto = new Proyecto();
            proyecto.ModificarJefeProyecto(DropDownListUsuarios, DropDownListProyectos);
            var list = proyecto.ObtenerProyectos();
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

        /*
         * Autor: Nicolás Hervias
         * Envía una solicitud para unirse al proyecto seleccionado (esta se guarda en la BD)
         * Parámetros: PosProyecto. Es la posición que tiene el proyecto en la lista de proyectos
         */
        [HttpPost]
        public void AgregarUsuarioAProyecto(int PosProyecto)
        {
            List<int> ListaProyectos = ListaProyectosIds();
            int IdProyectoAUnirse = ListaProyectos[PosProyecto];
            string UsuarioSolicitante = System.Web.HttpContext.Current.User.Identity.Name; // obtiene el user logueado actualmente (rut)
        }

        /*
         * Autor Juan Abello
         * Metodo encargado de obtener los nombres de los proyectos que existen ,guardarlos en una lista y retornar esta.
         * <param void>
         * <returns> listaProyectosNombres 
         */
        public List<NombreProyecto> ListaDeProyectos()
        {
            string consulta = "SELECT nombre FROM proyecto";
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
                    listaProyectosNombres.Add(new NombreProyecto(Nombre));
                }

                this.Conector.CerrarConexion();
                return listaProyectosNombres;
            }
        }

        /*
         * Autor: Nicolás Hervias
         * Crea una lista de ids de todos los proyectos
         * Parametros: N/A
         */
        public List<int> ListaProyectosIds()
        {
            List<int> ListaProyectos = new List<int>();
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
                    int Id_proyecto = reader.GetInt16(0);
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
        public List<NombreProyecto> ListaDeProyectosUsuario(int id)
        {
            string consulta = "SELECT proyecto.nombre FROM proyecto, users, vinculo_usuario_proyecto " +
                               "WHERE users.id = " + id + " AND vinculo_usuario_proyecto.ref_proyecto = " +
                               "proyecto.id_proyecto AND vinculo_usuario_proyecto.ref_usuario = users.id";
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
                    listaProyectosNombres.Add(new NombreProyecto(Nombre));
                }

                this.Conector.CerrarConexion();
                return listaProyectosNombres;
            }
        }


        /*
        * Autor Juan Abello
        * Metodo encargado de obtener los nombres de los proyectos en los que se no encuentra un usuario,guardarlos en una lista y retornar esta.
        * <param String rut>
        * <returns> listaProyectosNombres 
        */
        public List<NombreProyecto> ListaDeProyectoNoAsociados(int id)
        {
            string consulta = "SELECT Proyecto.nombre FROM Proyecto where Proyecto.nombre NOT IN" +
                              "(SELECT Proyecto.nombre FROM Proyecto, users, vinculo_usuario_proyecto " +
                              "WHERE users.id = 1  AND Vinculo_usuario_proyecto.ref_proyecto = Proyecto.id_proyecto AND Vinculo_usuario_proyecto.ref_usuario = users.id)";

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
                    listaProyectosNombres.Add(new NombreProyecto(Nombre));
                }

                this.Conector.CerrarConexion();
                return listaProyectosNombres;
            }
        }


    }
}


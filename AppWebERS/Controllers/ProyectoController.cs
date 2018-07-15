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
            var id = datos["Id del Proyecto"];
            var nombre = datos["Nombre"];
            return View();
        }


        // GET: Proyecto/ListaUsuarios/5
        public ActionResult ListaUsuarios(int id) {
            Proyecto proyecto = this.GetProyecto(id);
            
            List<Usuario> usuarios = GetListaUsuarios(id);

            //Debug.WriteLine("Permiso: " + TipoDePermiso());
            ViewData["proyecto"] = proyecto;
            ViewData["usuarios"] = usuarios;
            ViewData["permiso"] = TipoDePermiso(id);
            return View();
        }

        // POST: Proyecto/Detalles/5
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
            return ModoVista;
        }
            /**
            * Autor: Gerardo Estrada
            * <param name = "id" > Id del proyecto.</param>
            * <returns>La lista de usuarios involucrados en el proyecto</returns>
            * 
            **/
            private List<Usuario> GetListaUsuarios(int id) {
                this.conexion = ConectorBD.Instance;

                List<Usuario> listaUsuarios = new List<Usuario>();

                string consulta = "SELECT users.Rut, users.UserName, users.Email, users.Tipo FROM users, vinculo_usuario_proyecto, proyecto WHERE id_proyecto = " + id + " AND vinculo_usuario_proyecto.ref_proyecto = id_proyecto AND vinculo_usuario_proyecto.ref_usuario = users.Rut ;";
                MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
                if(reader == null) {
                    this.conexion.CerrarConexion();
                    return null;
                } else {
                    while(reader.Read()) {
                        string rut = reader.GetString(0);
                        string nombre = reader.GetString(1);
                        string correo = reader.GetString(2);
                        string tipo = reader.GetString(3);

                        listaUsuarios.Add(new Usuario(rut, nombre, correo, tipo));
                    }

                    this.conexion.CerrarConexion();
                    return listaUsuarios;
                }


                //Proyecto proyecto = null;
                //this.conexion = ConectorBD.Instance;
                //string consulta = "SELECT users.Nombre, users.Rut, users.Email, users.Tipo FROM users, vinculo_usuario_proyecto, proyecto WHERE id_proyecto = " + id + " AND vinculo_usuario_proyecto.ref_proyecto = id_proyecto AND vinculo_usuario_proyecto.ref_usuario = users.Rut ;";
                //MySqlDataReader data = this.conexion.RealizarConsulta(consulta);
                //if (data != null) {
                //    data.Read();
                //    string nombre = data["nombre"].ToString();
                //    string rut = data["rut"].ToString();
                //    string correo_electronico = data["correo_electronico"].ToString();
                //    string tipo = data["tipo"].ToString();
                //    this.conexion.CerrarConexion();

                //}
                //return Usuario;
            }
        }

    }


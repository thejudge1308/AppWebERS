using AppWebERS.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWebERS.Controllers
{
    public class ProyectoController : Controller
    {
        private ConectorBD conexion;

        // GET: Proyecto/Detalles/5
        public ActionResult Detalles(int id)
        {
            Proyecto proyecto = this.GetProyecto(id);
            string UsuarioActual = System.Web.HttpContext.Current.User.Identity.Name; // pregunta el usuario actual
            Debug.WriteLine("Usuario actual: " + UsuarioActual);
            ViewData["proyecto"] = proyecto;
            ViewData["permiso"] = TipoDePermiso(UsuarioActual);

            return View();
        }

        // POST: Proyecto/Detalles/5
        [HttpPost]
        public ActionResult Detalles(FormCollection datos) {

            return View();
        }


        // GET: Proyecto/ListaUsuarios/5
        public ActionResult ListaUsuarios(int id) {
            Proyecto proyecto = this.GetProyecto(id);
            string UsuarioActual = System.Web.HttpContext.Current.User.Identity.Name; // pregunta el usuario actual
            //Debug.WriteLine("Usuario actual: " + UsuarioActual);
            List<Usuario> usuarios = GetListaUsuarios(id);
            ViewData["usuarios"] = TipoDePermiso(UsuarioActual);
            ViewData["proyecto"] = proyecto;
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
            Proyecto proyecto=null;
            this.conexion = ConectorBD.Instance;
            string consulta = "SELECT * FROM proyecto WHERE id_proyecto = " + id + ";";
            MySqlDataReader data = this.conexion.RealizarConsulta(consulta);
            if(data != null) {
                data.Read();
                string nombre = data["nombre"].ToString();
                string proposito = data["proposito"].ToString();
                string alcance = data["alcance"].ToString();
                string contexto = data["contexto"].ToString();
                string definiciones = data["definiciones"].ToString();
                string acronimos = data["acronimos"].ToString();
                string abreviaturas = data["abreviaturas"].ToString();
                string referencias = data["referencias"].ToString();
                string ambiente_operacional = data["ambiente_operacional"].ToString();
                string relacion_con_otros_proyectos = data["relacion_con_otros_proyectos"].ToString();

                
                proyecto = new Proyecto(id, nombre, proposito, alcance, contexto, definiciones, acronimos, abreviaturas, referencias, ambiente_operacional, relacion_con_otros_proyectos);
                //Debug.WriteLine(proyecto.Proposito);
                this.conexion.CerrarConexion();
            }
            return proyecto;
            }


        /**
       * Autor: Patricio Quezada
       * <param name = "usuario" > usuario del sistema</param>
       * <returns>El permiso para el tipo de usuario que ve el contenido</returns>
       * 
       **/
        private int TipoDePermiso(String usuario) {
            //Esto estara completado una vez que este implementado el Entity Framework
            return Proyecto.AUTH_COMO_JEFE_DE_PROYECTO;
        /**
        * Autor: Gerardo Estrada
        * <param name = "id" > Id del proyecto.</param>
        * <returns>La lista de usuarios involucrados en el proyecto</returns>
        * 
        **/
        private List<Usuario> GetListaUsuarios(int id) {
            this.conexion = ConectorBD.Instance;

            List<Usuario> listaUsuarios = new List<Usuario>();

            string consulta = "SELECT users.Rut, users.Nombre, users.Email, users.Tipo FROM users, vinculo_usuario_proyecto, proyecto WHERE id_proyecto = " + id + " AND vinculo_usuario_proyecto.ref_proyecto = id_proyecto AND vinculo_usuario_proyecto.ref_usuario = users.Rut ;";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader == null) {
                this.conexion.CerrarConexion();
                return null;
            }
            else {
                while (reader.Read()) {
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

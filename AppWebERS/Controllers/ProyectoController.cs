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
            Debug.WriteLine("Usuario actual: " + UsuarioActual);
            Debug.WriteLine("Proyecto actual: " + proyecto);
            Debug.WriteLine("Permiso: " + TipoDePermiso());
            ViewData["proyecto"] = proyecto;
            ViewData["permiso"] = TipoDePermiso();

            return View();
        }

        // POST: Proyecto/Detalles/5
        [HttpPost]
        [Authorize]
        public ActionResult Detalles(FormCollection datos) {

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
        private int TipoDePermiso() {
            //Obtiene id del usuario de la sesion
            var UsuarioActual = User.Identity.GetUserId();
            int ModoVista = Proyecto.NO_AUTH;
            this.conexion = ConectorBD.Instance;
            string consulta = "SELECT Tipo From users WHERE id='"+UsuarioActual.ToString()+"';";
            MySqlDataReader data = this.conexion.RealizarConsulta(consulta);
            if(data != null) {
                data.Read();
                string rol = data["Tipo"].ToString();
                if(rol.Equals("SYSADMIN")){
                    ModoVista = Proyecto.AUTH_COMO_SYSADMIN;
                } else {
                    //Cambiar si es jefe de proyecto o usuario normal
                    ModoVista = Proyecto.AUTH_COMO_JEFE_DE_PROYECTO;
                }
                this.conexion.CerrarConexion();
            } else {

            }

            return ModoVista;
        }

    }
}

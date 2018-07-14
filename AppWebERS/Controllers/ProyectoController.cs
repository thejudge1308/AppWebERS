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
        }

    }
}

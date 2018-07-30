using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNet.Identity.MySQL;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using AppWebERS.Utilidades;


namespace AppWebERS.Controllers{
    public class JefeProyectoController : Controller{
        // GET: JefeProyecto
        private int idProyecto;

        private ConectorBD conector = ConectorBD.Instance;

        public ActionResult Index(){
            return View();
        }

        // GET: JefeProyecto/InvitarUsuario
        public ActionResult InvitarUsuario(int idProyecto){

            Proyecto proyecto = new Proyecto().ObtenerProyectoPorID(idProyecto);
            this.idProyecto = idProyecto;
            List<Usuario> usuarios = this.listaDeUsuarios(idProyecto);
            ViewData["proyecto"] = proyecto;
            ViewData["usuarios"] = usuarios;
            Debug.WriteLine("iDpROY" + proyecto.IdProyecto);
            return View();
        }

        //Get: JefeProyecto/EnviarSolicitud
        /*
 * Autor Fabian Oyarce
  * Metodo encargado de solicitar vincular un usuario a un proyecto
  * <param String rut>
  *  * <param String idProyecto>
 */     private ConectorBD Conector = ConectorBD.Instance;
        [HttpGet]
        public ActionResult EnviarSolicitud(String rut, String idProyecto)
        {

            string idUsuario = this.BuscaIdUsuarioPorRut(rut);

            string Values = "'" + idProyecto + "','" + idUsuario + "'";
            string Consulta = "INSERT INTO solicitud_vinculacion_proyecto (ref_proyecto,ref_solicitante) VALUES (" + Values + ");";
            Debug.Write(Consulta);
            if (this.Conector.RealizarConsultaNoQuery(Consulta))
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
        /*
         * Autor Fabian Oyarce
          * Metodo encargado de buscar el id de un usuario dado un rut
          * <param String rut>
         */
        public string BuscaIdUsuarioPorRut(string rut)
        {

            string consulta = "SELECT users.Id FROM users WHERE users.Rut ='" + rut + "'";
            string idUsuario = null;
            MySqlDataReader reader = this.Conector.RealizarConsulta(consulta);
            if (reader == null)
            {
                this.Conector.CerrarConexion();

            }
            else
            {

                while (reader.Read())
                {
                    idUsuario = reader.GetString(0);

                }

                this.Conector.CerrarConexion();
            }

            return idUsuario;
        }

        public List<Usuario> listaDeUsuarios(int idProyecto)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            string consulta = "SELECT UserName, Rut, Email, Tipo FROM users WHERE Rut NOT IN (SELECT Rut FROM users, vinculo_usuario_proyecto, proyecto " +
                "WHERE vinculo_usuario_proyecto.ref_proyecto = '"+idProyecto+"' AND vinculo_usuario_proyecto.ref_usuario = users.Id) " +
                "AND NOT users.Tipo ='SYSADMIN' AND NOT users.Disponibilidad_Vinculacion = 0 ";

            MySqlDataReader reader = this.conector.RealizarConsulta(consulta);

            if (reader == null)
            {
                this.conector.CerrarConexion();
                return null;
            }
            else
            {
                while (reader.Read())
                {
                    string nombre = reader.GetString(0);
                    string rut = reader.GetString(1);
                    string correo = reader.GetString(2);
                    string tipo = reader.GetString(3);

                    listaUsuarios.Add(new Usuario(rut, nombre, correo, tipo));
                }

                this.conector.CerrarConexion();
                return listaUsuarios;
            }

        }

        public void ListaProyecto() {

        }

        public void AgregarUsuarioAProyecto(Proyecto proyecto) {

        }

        public void GenerarDERS(Proyecto proyecto) {

        }

    }
}
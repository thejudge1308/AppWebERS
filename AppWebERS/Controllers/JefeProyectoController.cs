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


namespace AppWebERS.Controllers{
    public class JefeProyectoController : Controller{
        // GET: JefeProyecto

        private ConectorBD conector = ConectorBD.Instance;

        public ActionResult Index(){
            return View();
        }

        // GET: JefeProyecto/InvitarUsuario
        public ActionResult InvitarUsuario(String idProyecto){
            List<Usuario> lista = this.listaDeUsuarios(idProyecto);

            ViewData["proyecto"] = idProyecto;

            return View(lista);
        }

        //Get: JefeProyecto/EnviarSolicitud
        public ActionResult EnviarSolicitud(String rut, String idProyecto)
        {
            return null;
        }

        public List<Usuario> listaDeUsuarios(String idProyecto)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            string consulta = "SELECT UserName, Rut, Email, Tipo FROM users";
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
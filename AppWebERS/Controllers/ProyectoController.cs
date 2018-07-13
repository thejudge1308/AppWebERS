using AppWebERS.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWebERS.Controllers
{
    public class ProyectoController : Controller
    {
        
        private ConectorBD Conector = ConectorBD.Instance;

        // GET: Proyecto
        public ActionResult Index()
        {
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

            string Values = "'" + IdProyectoAUnirse + "','" + UsuarioSolicitante + "'";
            string Consulta = "INSERT INTO Solicitud_vinculacion (ref_proyecto,ref_solicitante) VALUES (" + Values + ");";
            if (this.Conector.RealizarConsultaNoQuery(Consulta))
            {
                this.Conector.CerrarConexion();
            }
            else
            {
                this.Conector.CerrarConexion();
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

        /*
         * Autor Juan Abello
         * Metodo encargado de obtener los nombres de los proyectos que existen ,guardarlos en una lista y retornar esta.
         * <param void>
         * <returns> listaProyectosNombres 
         */
        public List<String> ListaDeProyectos()
        {
            List<string> ListaProyectosNombres = new List<string>();
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
                    ListaProyectosNombres.Add(Nombre);
                   
                }
                this.Conector.CerrarConexion();
                return ListaProyectosNombres;
            }
        }
     }
}
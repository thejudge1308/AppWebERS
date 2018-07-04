using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MySql.Data.MySqlClient;

namespace AppWebERS.Models
{
    public class ProyectoController : Controller
    {
        private ConectorBD conector = ConectorBD.Instance;
        List<String> listaProyectosNombres = new List<String>();
        // GET: Proyecto
        public ActionResult Index()
        {
            return View();
            
        }

        /*
         * Autor Juan Abello
         * Metodo encargado de obtener los nombres de los proyectos que existen ,guardarlos en una lista y retornar esta.
         * <param void>
         * <returns> listaProyectosNombres 
         */
        public List<String> ListarProyectos()
        {
            

            string consulta = "SELECT nombre FROM proyecto";
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

                  
                    string Nombre = reader.GetString(1);


                    listaProyectosNombres.Add(Nombre);
                }

                this.conector.CerrarConexion();
                return listaProyectosNombres;
            }
        }


       
     }
}
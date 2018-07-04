
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
        List<NombreProyecto> listaProyectosNombres = new List<NombreProyecto>();
        // GET: Proyecto

        public ActionResult ListarProyectos()
        {
            var model = ObtenerProyectos();
            return View(model);
            
        }

        public List<NombreProyecto> ObtenerProyectos()
        {
            return ListaDeProyectos();
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

                    string Nombre = reader.GetString(0);
                    listaProyectosNombres.Add(new NombreProyecto(Nombre));
                }

                this.conector.CerrarConexion();
                return listaProyectosNombres;
            }
        }

    }
}
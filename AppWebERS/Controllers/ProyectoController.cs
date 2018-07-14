﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using MySql.Data.MySqlClient;

namespace AppWebERS.Controllers
{
    public class ProyectoController : Controller
    {
        private ConectorBD conector = ConectorBD.Instance;
        List<String> listaProyectosNombres = new List<String>();
        // GET: Proyecto
        public ActionResult ListarProyectos()
        {
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

        /*
         * Autor Juan Abello
         * Metodo encargado de obtener los nombres de los proyectos que existen ,guardarlos en una lista y retornar esta.
         * <param void>
         * <returns> listaProyectosNombres 
         */
        public List<String> ListaDeProyectos()
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

                  
                    string Nombre = reader.GetString(1);


                    listaProyectosNombres.Add(Nombre);
                }
                this.Conector.CerrarConexion();
                return ListaProyectosNombres;
            }
        }


       
     }
}
﻿using AppWebERS.Models;
using AppWebERS.Utilidades;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using AppWebERS.Utilidades;
using System.Net;

namespace AppWebERS.Controllers{
    public class DiagramaController : Controller{
        // GET: CasoDeUso


        int idProyectO=-1;

        private ConectorBD Conector = ConectorBD.Instance;
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult SubirDiagrama(int id)
        {
            
            Proyecto proyecto = this.GetProyecto(id);
            ViewBag.IdProyecto = proyecto.IdProyecto;
            idProyectO = id;

            return View();
        }

        private Proyecto GetProyecto(int id)
        {

            return new Proyecto().ObtenerProyectoPorID(id);
        }


        [HttpPost]
        public ActionResult SubirDiagrama(HttpPostedFileBase file, string nombre, string id, int diagramaValue, int diagramaValueURL, string url, int opValue, string nombreURL)
        {

            Debug.Write(opValue + " valor de la opcion ");
            Debug.Write(diagramaValue + " valor del tipo de diagrama");
            int idProyecto = Int32.Parse(id);
           

            try
            {

                if (url.Length > 0 && opValue ==3)
                {
                    string tipoDeDiagramaURL = tipoDiagrama(diagramaValueURL);
                    System.Net.WebClient webClient = new WebClient();
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), id + nombreURL + Path.GetExtension(url)).Replace(@"\", @"/");
                    Debug.Write(" "+ path.Replace(@"\", @"/") +" ");
                    if (nombreURL.Length == 0)
                    {
                         TempData["alerta"] = new Alerta("Debe ingresar un nombre", TipoAlerta.ERROR);
                        ViewBag.Message = "Debe ingresar un nombre.";
                        return RedirectToAction("SubirDiagrama", "Diagrama", new { id = idProyecto });
                    }

                    if (this.ValidarExtencionURL(url)==false)
                    {
                        TempData["alerta"] = new Alerta("Formato no soportado. Seleccione un archivo de imagen (.jpg, .jpeg, .png, .bmp o .gif)", TipoAlerta.ERROR);
                        ViewBag.Messagw = "Formato no soportado. Seleccione un archivo de imagen (.jpg, .jpeg, .png, .bmp o .gif)";
                        return RedirectToAction("SubirDiagrama", "Diagrama", new { id = idProyecto });
                    }
                    if (this.ValidarURLNoRepetida2("/UploadedFiles/" + id + nombreURL + Path.GetExtension(path)) == false)
                    {
                        TempData["alerta"] = new Alerta("Ya existe un archivo con este nombre", TipoAlerta.ERROR);
                        ViewBag.Message = "Ya existe un archivo con este nombre.";
                        return RedirectToAction("SubirDiagrama", "Diagrama", new { id = idProyecto });
                    }

                    if (this.ValidarLargoNombre(nombreURL) == false)
                    {
                        TempData["alerta"] = new Alerta("El nombre debe tener no más de 45 caracteres", TipoAlerta.ERROR);
                        ViewBag.Message = "El nombre debe tener no más de 45 caracteres";
                        return RedirectToAction("SubirDiagrama", "Diagrama", new { id = idProyecto });
                    }


                    if (this.ValidarNombreNoRepetido(nombreURL) == false)
                    {

                        TempData["alerta"] = new Alerta("Ya existe un diagrama con este nombre", TipoAlerta.ERROR);
                        ViewBag.Message = "Ya existe un diagrama con este nombre.";
                        return RedirectToAction("SubirDiagrama", "Diagrama", new { id = idProyecto });
                    }

                   
                    webClient.DownloadFile(@url, @path);
                    //this.agregar(nombreURL, id, "../../UploadedFiles/" +id + nombreURL + Path.GetExtension(path), tipoDeDiagramaURL);
                    this.agregar(nombreURL, id, "/UploadedFiles/" +id + nombreURL + Path.GetExtension(path), tipoDeDiagramaURL);
                    TempData["alerta"] = new Alerta("Diagrama subido con éxito!!", TipoAlerta.SUCCESS);
                    ViewBag.Message = "Diagrama subido con éxito!!";
                    Conector.CerrarConexion();
                
                   return RedirectToAction("ListarDiagramas", "Proyecto", new { id = idProyecto });
                    
                    
                    

                }




                string tipoDeDiagrama = tipoDiagrama(diagramaValue);
                string _FileName = id + Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName).Replace(@"\", @"/");
                Debug.Write(" " + _path.Replace(@"\", @"/") + " ");

              
                if (nombre.Length == 0)
                {
                    nombre = "null";
                }

                if (this.ValidarExtencion(file) == false)
                {
                    TempData["alerta"] = new Alerta("Tipo de archivo no soportado. Seleccione un archivo de imagen (.jpg, .jpeg, .png, .bmp o .gif)", TipoAlerta.ERROR);
                    ViewBag.Messagw = "Tipo de archivo no soportado. Seleccione un archivo de imagen (.jpg, .jpeg, .png, .bmp o .gif)";
                    return RedirectToAction("SubirDiagrama", "Diagrama", new { id = idProyecto });
                }
                if (file.ContentLength <= 0)
                {
                    ViewBag.Message = "Falla en la subida del Diagrama!!";
                    TempData["alerta"] = new Alerta("Falla en la subida del Diagrama!!", TipoAlerta.ERROR);
                    return RedirectToAction("SubirDiagrama", "Diagrama", new { id = idProyecto });
                }

                if (this.ValidarURLNoRepetida(file, id) == false)
                {
                    TempData["alerta"] = new Alerta("Ya existe un archivo con este nombre", TipoAlerta.ERROR);
                    ViewBag.Message = "Ya existe un archivo con este nombre.";
                    return RedirectToAction("SubirDiagrama", "Diagrama", new { id = idProyecto });
                }

                if (this.ValidarLargoNombre(nombre)==false)
                {
                    TempData["alerta"] = new Alerta("El nombre debe tener no más de 45 caracteres", TipoAlerta.ERROR);
                    ViewBag.Message = "El nombre debe tener no más de 45 caracteres";
                    return RedirectToAction("SubirDiagrama", "Diagrama", new { id = idProyecto });
                }
                         
              
                if (this.ValidarNombreNoRepetido(nombre) == false)
                {

                    TempData["alerta"] = new Alerta("Ya existe un diagrama con este nombre", TipoAlerta.ERROR);
                    ViewBag.Message = "Ya existe un diagrama con este nombre.";
                    return RedirectToAction("SubirDiagrama", "Diagrama", new { id = idProyecto });
                }

                //this.agregar(nombre, id, "../../UploadedFiles/" + _FileName, tipoDeDiagrama);
                this.agregar(nombre, id, "/UploadedFiles/" + _FileName, tipoDeDiagrama);
                file.SaveAs(_path);
                TempData["alerta"] = new Alerta("Diagrama subido con éxito!!", TipoAlerta.SUCCESS);
                ViewBag.Message = "Diagrama subido con éxito!!";
                Conector.CerrarConexion();
                return RedirectToAction("ListarDiagramas", "Proyecto", new { id = idProyecto });

            }
            catch
            {
                ViewBag.Message = "Error al subir el diagrama.";
                TempData["alerta"] = new Alerta("Error al subir el diagrama.", TipoAlerta.ERROR);
                return RedirectToAction("SubirDiagrama", "Diagrama", new { id = idProyecto });
            }
        }

        public bool ValidarLargoNombre(string nombre)
        {
            if (nombre.Length > 45)
            {
                return false;
            }
            return true;
        }

        public bool ValidarNombreNoRepetido(string nombre)
        {
            try
            {
               
                string consulta = "use appers; " +
                    "SELECT diagrama.nombre " +
                    "FROM diagrama " +
                    "WHERE diagrama.nombre ='"+nombre+"';";
                Debug.Write(consulta);
              
                MySqlDataReader reader2 = this.Conector.RealizarConsulta(consulta);
               
                if (reader2 == null)
                {
                    this.Conector.CerrarConexion();
                    return true;
                }
                else
                {
                    while (reader2.Read())
                    {
                        if (reader2["nombre"].ToString()== "null")
                        {
                            this.Conector.CerrarConexion();
                            return true;
                        }

                        if (reader2["nombre"].ToString() == nombre)
                        {
                            
                            this.Conector.CerrarConexion();
                            
                            return false;
                        }
                       
                    }
                    
                    this.Conector.CerrarConexion();
                    return false;
                   
                }
            }
            catch
            {
                this.Conector.CerrarConexion();
                return true;
            }
        }

        public bool ValidarURLNoRepetida(HttpPostedFileBase file, string id)
        {
            try
            {
                string _FileName = id + Path.GetFileName(file.FileName);
                string _path = "/UploadedFiles/" + _FileName;
                string ConsultaPath = "SELECT ruta FROM Diagrama WHERE ruta = '" + _path + "';";
                MySqlDataReader reader = this.Conector.RealizarConsulta(ConsultaPath);
                if (reader == null)
                {
                    this.Conector.CerrarConexion();
                    return true;
                }
                else
                {
                    while (reader.Read())
                    {
                        this.Conector.CerrarConexion();
                        return false;
                    }

                    this.Conector.CerrarConexion();
                    return false;

                }
            }
            catch
            {
                this.Conector.CerrarConexion();
                return true;
            }
        }

        public bool ValidarURLNoRepetida2(string _path)
        {
            try
            {
       
                string ConsultaPath = "SELECT ruta FROM Diagrama WHERE ruta = '" + _path + "';";
                MySqlDataReader reader = this.Conector.RealizarConsulta(ConsultaPath);
                if (reader == null)
                {
                    this.Conector.CerrarConexion();
                    return true;
                }
                else
                {
                    while (reader.Read())
                    {
                        this.Conector.CerrarConexion();
                        return false;
                    }

                    this.Conector.CerrarConexion();
                    return false;

                }
            }
            catch
            {
                this.Conector.CerrarConexion();
                return true;
            }
        }



        public bool ValidarExtencion(HttpPostedFileBase file)
        {
            if ((string.Equals(Path.GetExtension(file.FileName), ".jpg", StringComparison.OrdinalIgnoreCase))
           
                   || (string.Equals(Path.GetExtension(file.FileName), ".jpeg", StringComparison.OrdinalIgnoreCase))
                   || (string.Equals(Path.GetExtension(file.FileName), ".png", StringComparison.OrdinalIgnoreCase))
                   || (string.Equals(Path.GetExtension(file.FileName), ".bmp", StringComparison.OrdinalIgnoreCase))
                   || (string.Equals(Path.GetExtension(file.FileName), ".gif", StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidarExtencionURL(string path)
        {

            Debug.Write(path );
            Debug.Write(path.Substring(path.Length - 4));
            if ((string.Equals(Path.GetExtension(path), ".jpg", StringComparison.OrdinalIgnoreCase))

                   || (string.Equals(Path.GetExtension(path), ".jpeg", StringComparison.OrdinalIgnoreCase))
                   || (string.Equals(Path.GetExtension(path), ".png", StringComparison.OrdinalIgnoreCase))
                   || (string.Equals(Path.GetExtension(path), ".bmp", StringComparison.OrdinalIgnoreCase))
                   || (string.Equals(Path.GetExtension(path), ".gif", StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void agregar(string nombre, string idProyecto1, string url,string tipo)
        {
            try
            {
                if (url.Length == 0)
                {
                    TempData["alerta"] = new Alerta("Debe ingresar una imagen", TipoAlerta.ERROR);

                }
                else
                {
                    int idProyecto = Int32.Parse(idProyecto1);

                    Debug.Write(nombre);
                    if (nombre == "null")
                    {
                        Debug.Write("ES NULLLLL  ");
                        string consulta = "use appers; " +
                                  "INSERT INTO diagrama( ruta, tipo,ref_proyecto) VALUES ('" + url + "','" + tipo + "'," + idProyecto + " );";
                        Debug.Write(consulta);
                        this.Conector.RealizarConsulta(consulta);
                        this.Conector.CerrarConexion();
                        this.AgregarModificacionDiagramaDERS(idProyecto, DateTime.Now.ToString("yyyy-MM-dd"), User.Identity.GetUserId(),
                            "Se ha agregado un diagrama con url"+url);
                    }
                    else
                    {
                        string consulta = "use appers; " +
                                  "INSERT INTO diagrama(nombre, ruta, tipo,ref_proyecto) VALUES ( '" + nombre + "','" + url + "','" + tipo + "'," + idProyecto + " );";

                        this.Conector.RealizarConsulta(consulta);
                        this.Conector.CerrarConexion();
                        this.AgregarModificacionDiagramaDERS(idProyecto, DateTime.Now.ToString("yyyy-MM-dd"), User.Identity.GetUserId(),
                            "Se ha agregado el diagrama del tipo "+tipo +" con nombre " + nombre);
                    }

                }


            }
            catch
            {
                TempData["alerta"] = new Alerta("Falla en la subida del Diagrama!!", TipoAlerta.ERROR);
            }
            


        }

        public string tipoDiagrama(int id)
        {
            string tipoDeDiagrama;
            if (id == 1)
            {
                tipoDeDiagrama = "CASO_DE_USO";
            }
            else if (id == 3)
            {
                tipoDeDiagrama = "ARQ_LOGICA";
            }
            else
            {
                tipoDeDiagrama = "ARQ_FISICA";
            }

            return tipoDeDiagrama;
        }
        public void Listar() {

        }

        public void Editar() {

        }

        [HttpGet]
        public ActionResult eliminarDiagrama(string url,string idProyecto)
        {
            try
            {
                String nombreDiagrama = this.obtenerNombreDiagrama(url);
                String consulta = "DELETE FROM `appers`.`diagrama` WHERE(`ruta` ='" + url + "')";
                Debug.Write(consulta);
                bool resultado = this.Conector.RealizarConsultaNoQuery(consulta);
                System.IO.File.Delete(Server.MapPath(url));

                if (resultado == true)
                {
                    int id = Int32.Parse(idProyecto);

                    if (!nombreDiagrama.Equals("")) { 
                        this.AgregarModificacionDiagramaDERS(id, DateTime.Now.ToString("yyyy-MM-dd"), User.Identity.GetUserId(),
                            "Se ha eliminado el diagrama "+nombreDiagrama);
                    }
                    else {
                        this.AgregarModificacionDiagramaDERS(id, DateTime.Now.ToString("yyyy-MM-dd"), User.Identity.GetUserId(),
                           "Se ha eliminado al diagrama con url: "+url);
                    }
                    TempData["alerta"] = new Alerta("Diagrama eliminado con éxito!!", TipoAlerta.SUCCESS);
                }
                else
                {
                    TempData["alerta"] = new Alerta("Error al eliminar diagrama", TipoAlerta.ERROR);
                }

                return RedirectToAction("ListarDiagramas", "Proyecto", new { id = idProyecto });
            }
            catch
            {
                TempData["alerta"] = new Alerta("Error al eliminar diagrama", TipoAlerta.ERROR);
                return RedirectToAction("ListarDiagramas", "Proyecto", new { id = idProyecto });
            }
            
        }
        public bool AgregarModificacionDiagramaDERS( int id, string fecha, string userId, string descripcion)
        {
            
            float versionActual = this.ObtenerVersionActual(id) + 0.01F;
            string vers = versionActual.ToString().Replace(',', '.');
            string consulta = "INSERT INTO modificacion_ders(version,ref_proyecto,fecha,ref_autor_modificacion,descripcion) " +
                "VALUES(" + vers + ", " + id + ", '" + fecha + "' , '" + userId + "' , '" + descripcion + "' ) ";
            ApplicationDbContext con = ApplicationDbContext.Create();
            if (con.RealizarConsultaNoQuery(consulta))
            {
                return true;
            }
            return false;
        }

        private string obtenerNombreDiagrama(string url) {
            string consulta = "SELECT diagrama.nombre FROM diagrama WHERE ruta='"+url+"';";
            ApplicationDbContext con = ApplicationDbContext.Create();
            MySqlDataReader reader = con.RealizarConsulta(consulta);

            if (reader != null)
            {
                reader.Read();
                return reader[0].ToString();
            }
            else
            {

                return null;
            }
        }

        private float ObtenerVersionActual(int id)
        {

            float version = 0.00F;
            string consulta = "SELECT modificacion_ders.version FROM modificacion_ders WHERE ref_proyecto = " + id +
                " ORDER BY version DESC LIMIT 1";
            ApplicationDbContext con = ApplicationDbContext.Create();
            MySqlDataReader reader = con.RealizarConsulta(consulta);

            if (reader != null)
            {
                reader.Read();
                version = float.Parse(reader[0].ToString());
                return version;
            }
            else
            {

                return 0.00F;
            }
        }
    }
}
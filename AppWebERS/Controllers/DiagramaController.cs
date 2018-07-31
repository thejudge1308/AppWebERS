using AppWebERS.Models;
using AppWebERS.Utilidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using AppWebERS.Utilidades;

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
        public ActionResult SubirDiagrama(HttpPostedFileBase file, string nombre, string id, int diagramaValue)
        {
            
            
            int idProyecto = Int32.Parse(id);
            string tipoDeDiagrama = tipoDiagrama(diagramaValue);
            Debug.Write(tipoDeDiagrama);
            string _FileName = Path.GetFileName(file.FileName);
            string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);

            try
            {
                if (this.ValidarExtencion(file))
                {
                    Debug.Write("extencion valida ");
                    if (file.ContentLength > 0)
                    {
                        Debug.Write("largo valido ");
                        if (this.ValidarURLNoRepetida(file)==true)
                        {
                            Debug.Write("url no repetida ");
                            if (this.ValidarLargoNombre(nombre)==false)
                            {
                                Debug.Write("largo mas del maximo ");
                                TempData["alerta"] = new Alerta("El nombre debe tener no más de 45 caracteres", TipoAlerta.ERROR);
                                ViewBag.Message = "El nombre debe tener no más de 45 caracteres";
                            }
                            else
                            {
                                Debug.Write("BUEN LARGO");
                                if (nombre.Length == 0)
                                {
                                    nombre = "null";
                                }

                                if (this.ValidarNombreNoRepetido(nombre)==true)
                                {
                                    Debug.Write("nombre no repedito");
                                    this.agregar(nombre, id, _path, tipoDeDiagrama);
                                    file.SaveAs(_path);
                                    TempData["alerta"] = new Alerta("Diagrama subido con éxito!!", TipoAlerta.SUCCESS);
                                    ViewBag.Message = "Diagrama subido con éxito!!";
                                }
                                else
                                {
                                    Debug.Write("Largo repetido");
                                    TempData["alerta"] = new Alerta("Ya existe un diagrama con este nombre", TipoAlerta.ERROR);
                                    ViewBag.Message = "Ya existe un diagrama con este nombre.";
                                }
                                Conector.CerrarConexion();
                            }
                        }
                        else
                        {
                            Conector.CerrarConexion();
                            TempData["alerta"] = new Alerta("Ya existe un archivo con este nombre", TipoAlerta.ERROR);
                            ViewBag.Message = "Ya existe un archivo con este nombre.";
                        }
                    }
                }
                else
                {
                    TempData["alerta"] = new Alerta("Tipo de archivo no soportado. Seleccione un archivo de imagen (.jpg, .jpeg, .png, .bmp o .gif)", TipoAlerta.ERROR);
                    ViewBag.Messagw = "Tipo de archivo no soportado. Seleccione un archivo de imagen (.jpg, .jpeg, .png, .bmp o .gif)";
                }
                return RedirectToAction("SubirDiagrama", "Diagrama", new { id = idProyecto });
            }
            catch
            {
                ViewBag.Message = "Falla en la subida del Diagrama!!";
                TempData["alerta"] = new Alerta("Falla en la subida del Diagrama!!", TipoAlerta.ERROR);
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
                string ConsultaNombre = "SELECT nombre FROM Diagrama WHERE nombre = '" + nombre + "';";
                MySqlDataReader reader = this.Conector.RealizarConsulta(ConsultaNombre);
                if (reader == null)
                {
                    this.Conector.CerrarConexion();
                    return true;
                }
                else
                {
                    while (reader.Read())
                    {
                        return false;
                    }
                  
                    this.Conector.CerrarConexion();
                    return false;
                   
                }
            }
            catch
            {
                return true;
            }
        }

        public bool ValidarURLNoRepetida(HttpPostedFileBase file)
        {
            try
            {
                string _FileName = Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
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
                        return false;
                    }

                    this.Conector.CerrarConexion();
                    return false;

                }
            }
            catch
            {
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

        public void agregar(string nombre, string idProyecto1, string url,string tipo)
        {
            int idProyecto =  Int32.Parse(idProyecto1);
            
            string consulta = "START TRANSACTION;" +
            "INSERT INTO diagrama(nombre, ruta, tipo,ref_proyecto) VALUES ( '" + nombre + "','" + url + "','" + tipo + "'," + idProyecto + " );" +
              "COMMIT;";
            this.Conector.RealizarConsultaNoQuery(consulta);
            Debug.WriteLine(consulta);
            this.Conector.CerrarConexion();


        }

        public string tipoDiagrama(int id)
        {
            string tipoDeDiagrama;
            if (id == 1)
            {
                tipoDeDiagrama = "CASO_DE_USO";
            }
            else if (id == 2)
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
    }
}
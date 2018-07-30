using AppWebERS.Models;
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

            try
            {
                // Este if-else comprueba que la extensión del archivo sea jpg, jpeg, png, bmp o gif
                if ((string.Equals(Path.GetExtension(file.FileName), ".jpg",StringComparison.OrdinalIgnoreCase))
                    || (string.Equals(Path.GetExtension(file.FileName), ".jpeg", StringComparison.OrdinalIgnoreCase))
                    || (string.Equals(Path.GetExtension(file.FileName), ".png", StringComparison.OrdinalIgnoreCase))
                    || (string.Equals(Path.GetExtension(file.FileName), ".bmp", StringComparison.OrdinalIgnoreCase))
                    || (string.Equals(Path.GetExtension(file.FileName), ".gif", StringComparison.OrdinalIgnoreCase)))
                {
                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        int largoStringNombre = nombre.Length;
                        // Este if-else comprueba que el largo del nombre del diagrama sea menor a 45 caracteres
                        if (largoStringNombre > 45)
                        {
                            TempData["alerta"] = new Alerta("El nombre debe tener no más de 45 caracteres", TipoAlerta.ERROR);
                            ViewBag.Message = "El nombre debe tener no más de 45 caracteres";
                        }
                        else
                        {
                            if (largoStringNombre == 0) { nombre = "null"; }
                            string Consulta = "SELECT nombre FROM Diagrama WHERE nombre = '" + nombre + "';";
                            MySqlDataReader reader = this.Conector.RealizarConsulta(Consulta);
                            // Este if-else comprueba que el nombre del diagrama no exista ya en la base de datos
                            if (reader == null || nombre.Equals("null"))
                            {
                                this.agregar(nombre, id, _path, tipoDeDiagrama);
                                file.SaveAs(_path);
                                TempData["alerta"] = new Alerta("Diagrama subido con éxito!!", TipoAlerta.SUCCESS);
                                ViewBag.Message = "Diagrama subido con éxito!!";
                            }
                            else
                            {
                                TempData["alerta"] = new Alerta("Ya existe un diagrama con este nombre", TipoAlerta.ERROR);
                                ViewBag.Message = "Ya existe un diagrama con este nombre.";
                            }
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
using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                if (file.ContentLength > 0)
                {
                    
                   
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    this.agregar(nombre,id,_path,tipoDeDiagrama);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "Diagrama subido con éxito!!";
                return RedirectToAction("SubirDiagrama", "Diagrama", new { id = idProyecto });
            }
            catch
            {
                ViewBag.Message = "Falla en la subida del Diagrama!!";
                return RedirectToAction("SubirDiagrama", "Diagrama", new { id = idProyecto });
            }
        }

        public void agregar(string nombre, string idProyecto1, string url,string tipo)
        {
            int idProyecto =  Int32.Parse(idProyecto1);
            string nombree;
            if (nombre.Length == 0)
            {
                nombree = "null";
            }
            else
            {
                nombree = nombre;
            }
            string consulta = "START TRANSACTION;" +
            "INSERT INTO diagrama(nombre, ruta, tipo,ref_proyecto) VALUES ( '" + nombree + "','" + url + "','" + tipo + "'," + idProyecto + " );" +
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
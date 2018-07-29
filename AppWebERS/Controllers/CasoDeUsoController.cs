using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWebERS.Controllers{
    public class CasoDeUsoController : Controller{
        // GET: CasoDeUso


        int idProyectO=-1;
     

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult UploadFile(int id)
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
        public ActionResult UploadFile(HttpPostedFileBase file, string nombre, string id)
        {
           

            try
            {
                if (file.ContentLength > 0)
                {
                    
                   
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                  
                    file.SaveAs(_path);
                    this.agregar(nombre,id,_path);
                }
                ViewBag.Message = "Caso de uso subido con éxito!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "Falla en la subida del caso de uso!!";
                return View();
            }
        }

        public void agregar(string nombre, string idProyecto, string url)
        {
            Debug.Write(nombre);
            Debug.Write(idProyecto);
            Debug.Write(url);


        }

        public void Listar() {

        }

        public void Editar() {

        }
    }
}
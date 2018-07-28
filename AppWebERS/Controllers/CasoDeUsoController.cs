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



        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult UploadFile()
        {

            return View();
        }


        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
           

            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    string nombre= "algo";
                    string idProyecto = "algo";
                   
                    file.SaveAs(_path);
                    this.agregar(nombre,idProyecto,_path);
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


        }

        public void Listar() {

        }

        public void Editar() {

        }
    }
}
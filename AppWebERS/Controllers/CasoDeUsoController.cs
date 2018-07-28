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
        Proyecto proyecto;

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult UploadFile(int id)
        {
            Debug.Write(id);
            proyecto = this.GetProyecto(id);
            Debug.Write(proyecto.IdProyecto);
            ViewData["proyecto"] = proyecto;
            idProyectO = id;

            return View();
        }

        private Proyecto GetProyecto(int id)
        {

            return new Proyecto().ObtenerProyectoPorID(id);
        }


        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, FormCollection form)
        {
            ViewData["proyecto"] = proyecto;

            try
            {
                if (file.ContentLength > 0)
                {
                    var numero1 = form["txtNumero1"];
                    Debug.Write(numero1);
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    string nombre= "algo";
                    file.SaveAs(_path);
                    this.agregar(nombre,idProyectO,_path);
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

        public void agregar(string nombre, int idProyecto, string url)
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
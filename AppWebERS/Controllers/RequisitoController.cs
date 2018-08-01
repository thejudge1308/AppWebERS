using AppWebERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWebERS.Controllers
{
    public class RequisitoController : Controller
    {
        // GET: Requisito
        public ActionResult Index()
        {
            return View();
        }

        public void Crear() {

        }

        public void Listar() {

        }

        public void Editar() {

        }

        //Creador: Patricio Quezada
        //Retorna la fecha del servidor para el datePicker
        [HttpGet]
        public ActionResult FechaActual() {
            return Json(DateTime.Now.ToString("yyyy/MM/dd"), JsonRequestBehavior.AllowGet);  
        }

        [HttpGet]
        public ActionResult Requisito(int id)
        {
            return RedirectToAction("Requisito", "Proyecto");
        }

        [HttpPost]
        public ActionResult Requisito(string idRequisito, string nombre, string descripcion, string prioridad, string fuente,
            string estabilidad, string estado, string tipoRequisito, string medida, string escala,
            string fecha, string incremento, string tipo)
        {
            Requisito requisito = new Requisito(idRequisito, nombre, descripcion, prioridad, fuente, estabilidad, estado,
                tipoRequisito, medida, escala, fecha, incremento, tipo);
            return View();
        }
    }
}
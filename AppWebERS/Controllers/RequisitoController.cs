using AppWebERS.Models;
using AppWebERS.Utilidades;
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

        ///<author>Maximo Hernandez</author>
        /// <summary>
        /// Elimina un requisito de usuario, siempre que este no este vinculado a un requisito de sistema.
        /// </summary>
        /// <param name="IdProyecto">Id del proyecto actual</param>
        /// <param name="IdRequisito">Id del requisito a eliminar</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EliminarRequisitoUsuario(int IdProyecto, string IdRequisito)
        {
            Requisito Requisito = new Requisito();
            int NumeroDeRequisitosAsociados = Requisito.ObtenerNumeroDeRequisitosAsociadoAUnRequisito(IdProyecto, IdRequisito, 0);
            if (NumeroDeRequisitosAsociados == 0)
            {
                Requisito.EliminarRequisito(IdProyecto, IdRequisito);
                TempData["alerta"] = new Alerta("Requisito de usuario eliminado con exito", TipoAlerta.SUCCESS);
                return RedirectToAction("ListarRequisitosMinimalista/" + IdProyecto, "Proyecto");
            }
            TempData["alerta"] = new Alerta("El requisito de usuario todavia posee requisitos de sistema asociados, elimine estos primero", TipoAlerta.ERROR);
            System.Diagnostics.Debug.WriteLine(NumeroDeRequisitosAsociados);
            return RedirectToAction("ListarRequisitosMinimalista/" + IdProyecto, "Proyecto");
        }

        ///<author>Maximo Hernandez</author>
        /// <summary>
        /// Elimina un requisito de sistema si es que tiene solo una asociacion.
        /// Desvincula un requisito de sistema si es que este tiene más de una asociacion.
        /// </summary>
        /// <param name="IdProyecto">Id del proyecto actual</param>
        /// <param name="IdRequisitoSistema">Id del requisito de sistema que se quiere eliminar o desvincular</param>
        /// <param name="IdRequisitoUsuario">Id del requisito de usuario del que se debe de desvincular el requisito de sistema</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EliminarRequisitoSistema(int IdProyecto, string IdRequisitoSistema, string IdRequisitoUsuario)
        {
            Requisito Requisito = new Requisito();
            int NumeroDeRequisitos = Requisito.ObtenerNumRequisito(IdProyecto, IdRequisitoSistema);
            int NumeroDeRequisitosAsociados = Requisito.ObtenerNumeroDeRequisitosAsociadoAUnRequisito(IdProyecto, IdRequisitoSistema, 1);
            if (NumeroDeRequisitosAsociados == 1)
            {
                Requisito.EliminarRequisito(IdProyecto, IdRequisitoSistema, IdRequisitoUsuario);
                TempData["alerta"] = new Alerta("Requisito de sistema eliminado con exito", TipoAlerta.SUCCESS);
                return RedirectToAction("ListarRequisitosMinimalista/" + IdProyecto, "Proyecto");
            }
            else if (NumeroDeRequisitosAsociados > 1)
            {
                Requisito.DeshabilitarRequisito(IdProyecto, IdRequisitoSistema, IdRequisitoUsuario);
                TempData["alerta"] = new Alerta("Requisito de sistema desvinculado con exito", TipoAlerta.SUCCESS);
                return RedirectToAction("ListarRequisitosMinimalista/" + IdProyecto, "Proyecto");
            }
            TempData["alerta"] = new Alerta("Ha ocurrido un error", TipoAlerta.ERROR);
            System.Diagnostics.Debug.WriteLine(NumeroDeRequisitos);
            return RedirectToAction("ListarRequisitosMinimalista/" + IdProyecto, "Proyecto");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWebERS.Controllers
{
    public class HistorialController : Controller
    {
        // GET: Historial
        [HttpPost]
        public ActionResult HistorialCambios(int id)
        {
            return View();
        }
    }
}
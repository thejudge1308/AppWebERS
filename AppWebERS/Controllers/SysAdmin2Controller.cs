using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWebERS.Controllers
{
    public class SysAdmin2Controller : Controller
    {
        // GET: SysAdmin2
        public ActionResult Index()
        {

            return View("Principal");
        }

        // GET: SysAdmin2
        public ActionResult Principal(String name)
        {
            if (name==null) {
                ViewBag.name = " SysAdmin - Test";
            }
            else
            {
                ViewBag.name = name;
            }
            return View();
        }
    }
}
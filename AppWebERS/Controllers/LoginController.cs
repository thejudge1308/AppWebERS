using System;
using System.Web.Mvc;

namespace AppWebERS.Controllers
{
    public class LoginController : Controller
    {
        private String NombreUsuario;
        private String Contrasenna;

        public LoginController(string nombreUsuario, string contrasenna)
        {
            NombreUsuario = nombreUsuario;
            Contrasenna = contrasenna;
        }

        
    }
}
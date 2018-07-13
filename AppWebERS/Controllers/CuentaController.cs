using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AppWebERS.Models;
using System.Data.Entity;
namespace AppWebERS.Controllers
{
    [Authorize]
    public class CuentaController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public CuentaController()
        {
        }

        public CuentaController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ApplicationUser usuario = await UserManager.FindByRutAsync(model.Rut);
            if (usuario == null)
            {
                ModelState.AddModelError("", "Rut de usuario incorrecto");
                return View(model);
            }
            // No cuenta los errores de inicio de sesión para el bloqueo de la cuenta
            // Para permitir que los errores de contraseña desencadenen el bloqueo de la cuenta, cambie a shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(usuario.UserName, model.Contrasenia, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Intento de inicio de sesión no válido.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (VerificarResgistroValidoAsync(model.UserName, model.Email , model.Rut))
                {
                    var user = new ApplicationUser { UserName = model.UserName, Rut = model.Rut, Email = model.Email, Tipo = "USER" };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // Para obtener más información sobre cómo habilitar la confirmación de cuentas y el restablecimiento de contraseña, visite https://go.microsoft.com/fwlink/?LinkID=320771
                        // Enviar correo electrónico con este vínculo
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirmar cuenta", "Para confirmar la cuenta, haga clic <a href=\"" + callbackUrl + "\">aquí</a>");

                        return RedirectToAction("Index", "Home");
                    }
                    AddErrors(result);
                }
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        /*
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // No revelar que el usuario no existe o que no está confirmado
                    return View("ForgotPasswordConfirmation");
                }

                // Para obtener más información sobre cómo habilitar la confirmación de cuentas y el restablecimiento de contraseña, visite https://go.microsoft.com/fwlink/?LinkID=320771
                // Enviar correo electrónico con este vínculo
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Restablecer contraseña", "Para restablecer la contraseña, haga clic <a href=\"" + callbackUrl + "\">aquí</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }
        */

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // No revelar que el usuario no existe
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        /* <autor>Diego Matus</autor>
         * <summary>Metodo encargado de enviar la lista de usarios a la vista ListarUsuarios y mostrarla dicha
         * lista</summary>
         * <param void>
         * <returns> 
         * Retorna la vista correspodiente (ListarUsuarios).
         * </returns>
         * 
         */
        [HttpGet]
        public async Task<ActionResult> ListarUsuarios()
        {
            ViewBag.Title = "ListarUsuarios";
            var lista = await UserManager.GetAllUsersAsync();
            return View(lista);
        }

        [HttpGet]
        public async Task<ActionResult> ModificarCuenta(string rut)
        {
            ViewBag.Title = "ModificarCuenta";
            if (!String.IsNullOrEmpty(rut)) {
                ApplicationUser usuario = await UserManager.FindByRutAsync(rut);
                ModificarViewModel model = new ModificarViewModel(usuario);
                return View(model);
            }
            return RedirectToAction("Index","Home");
        }

        /*
         * Juan Abello
         * llama a la funcion de modificar la cuenta de un usuario en el modelo de este
         * usuario
         * return RedirectToAction
         */
        [HttpPost]
        public async Task<ActionResult> ModificarCuenta(ModificarViewModel usuarioViewModel)
        {
            ViewBag.Title = "ModificarCuenta";
            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine(usuarioViewModel.Email);
                if (VerificarUsuarioRepetidoAsync(usuarioViewModel.Nombre, usuarioViewModel.Email))
                {
                    ApplicationUser usuario = await UserManager.FindByRutAsync(usuarioViewModel.Rut);
                    usuario.Email = String.IsNullOrEmpty(usuarioViewModel.Email) ? usuario.Email : usuarioViewModel.Email;
                    usuario.UserName = String.IsNullOrEmpty(usuarioViewModel.Nombre) ? usuario.UserName : usuarioViewModel.Nombre;
                    usuario.Estado = usuarioViewModel.Estado ? !usuario.Estado : usuario.Estado;
                    await UserManager.UpdateAsync(usuario);
                    if (!String.IsNullOrEmpty(usuarioViewModel.Password))
                    {
                        await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), usuarioViewModel.Password);
                    }
                    return RedirectToAction("ModificarCuenta", new { rut = usuarioViewModel.Rut });
                }
                else
                {
                    return View(usuarioViewModel);
                }
            }
            else
            {
              return View(usuarioViewModel);
            }
        }

        /*
         * Creador: Maximo Hernandez
         * Accion: Verifica si alguno de los dos valores de usuario, UserName o UserEmail, se encuentran ya en la base de datos
         * Retorno: Boolean - Falso si es que existe uno de los dos valores, Verdadero en caso contrario
         */
        public Boolean VerificarUsuarioRepetidoAsync(string UserName, string UserEmail)
        {
            if(UserManager.VerificarEmailUsuario(UserEmail).Result == false && UserManager.VerificarUserNameUsuario(UserName).Result == false)
                return true;
            return false;
        }

        /*
       * Creador: Maximo Hernandez-Diego Matus
       * Accion: Verifica si alguno de los valores de usuario registrados ya existen dentro de la base de datos.
       * Retorno: Boolean - Falso si es que existe concidencia en los valores, Verdadero en caso contrario
       */
        public Boolean VerificarResgistroValidoAsync(string RegisterName, string RegisterEmail, string RegisterRut)
        {
            if (UserManager.VerificarEmailUsuario(RegisterEmail).Result == false && UserManager.VerificarUserNameUsuario(RegisterName).Result == false &&
                UserManager.VerificarRutUsuario(RegisterRut).Result == false)
                return true;
            return false;
        }

        /*
         * Creador: Maximo Hernandez
         * Accion: Retorna el tipo del usuario autentificado
         * Retorno: String con el tipo de usuario autentificado
         */
        public string RetornarTipoUsuarioAutentificado()
        {
                Task<string> tipo = UserManager.getTipoAsync((User.Identity.GetUserId()));
                return tipo.Result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Aplicaciones auxiliares
        // Se usa para la protección XSRF al agregar inicios de sesión externos
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
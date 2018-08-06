using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using AspNet.Identity.MySQL;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using AppWebERS.Models;

namespace AppWebERS
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>() as MySQLDatabase));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 0,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        /*
        * Creador: Gabriel Sanhueza
        * Accion: Busca un usuario por rut
        * Retorno: Una operacion asincrona (Parecida a las promesas de js)
        */
        public Task<ApplicationUser> FindByRutAsync(string rut)
        {
            using (var db = ApplicationDbContext.Create())
            {
                return new UserStore<ApplicationUser>(db).FindByRutAsync(rut);
            }
        }

        /*
        * Creador: Gabriel Sanhueza
        * Accion: Busca un usuario por rut
        * Retorno: Una operacion asincrona (Parecida a las promesas de js)
        */
        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            using (var db = ApplicationDbContext.Create())
            {
                return new UserStore<ApplicationUser>(db).FindByEmailAsync(email);
            }
        }

        /*
         * Creador: Gabriel Sanhueza
         * Accion: Obtiene todos los usuarios
         * Retorno: Una operacion asincrona (Parecida a las promesas de js)
         */
        public Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            using (var db = ApplicationDbContext.Create())
            {
                return new UserStore<ApplicationUser>(db).GetAllUsersAsync();
            }
        }

        /*
        * Creador: Gabriel Sanhueza
        * Accion: Busca un usuario por rut
        * Retorno: Una operacion asincrona (Parecida a las promesas de js)
        */
        public async Task<bool> IsEstadoEnabledAsync(string userId)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var store = new UserStore<ApplicationUser>(db);
                var usuarioTask = store.FindByIdAsync(userId);
                ApplicationUser usuario = await usuarioTask;
                bool result = await store.GetEstadoAsync(usuario);
                return result;
            }
        }

        /*
        * Creador: Gabriel Sanhueza
        * Accion: Busca un usuario por rut
        * Retorno: Una operacion asincrona (Parecida a las promesas de js) usar await para esperar que el resultado de la operacion este listo
        */
        public async Task setEstadoAsync(string userId, bool isEnabled)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var store = new UserStore<ApplicationUser>(db);
                var usuarioTask = store.FindByIdAsync(userId);
                ApplicationUser usuario = await usuarioTask;
                await store.SetEstadoAsync(usuario, isEnabled);
                store.UpdateAsync(usuario);
            }
        }

        /*
         * Creador: Gabriel Sanhueza
         * Accion: Busca un usuario por rut
         * Retorno: Una operacion asincrona (Parecida a las promesas de js) usar await para esperar que el resultado de la operacion este listo
         */
        public async Task<string> getTipoAsync(string userId)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var store = new UserStore<ApplicationUser>(db);
                var usuarioTask = store.FindByIdAsync(userId);
                ApplicationUser usuario = await usuarioTask;
                return await store.GetTipoAsync(usuario);
            }
        }

        public async Task<string> getRutAsync(string userId)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var store = new UserStore<ApplicationUser>(db);
                var usuarioTask = store.FindByIdAsync(userId);
                ApplicationUser usuario = await usuarioTask;
                return await store.GetRutAsync(usuario);
            }
        }

        public async Task ChangePasswordAsync(string userId, string newPassword)
        {
            var store = this.Store as IUserPasswordStore<ApplicationUser>;
            var usuario = await store.FindByIdAsync(userId);
            var newPasswordHash = this.PasswordHasher.HashPassword(newPassword);
            await store.SetPasswordHashAsync(usuario, newPasswordHash);
            Task.FromResult<Object>(null);
        }

        /*
         * Creador: Maximo Hernandez
         * Accion: Verifica si UserName se encuentran en la base de datos
         * Retorno: Boolean - Verdadero si UserName ya existe en la base de datos. Verdadero en caso contrario.
         */
        public async Task<bool> VerificarSiExisteNombre(string UserName)
        {
            if (!String.IsNullOrEmpty(UserName))
            {
                ApplicationUser usuario = await FindByNameAsync(UserName);
                if (usuario == null)
                    return false;
                return true;
            }
            return false;
        }

        /*
         * Creador: Maximo Hernandez
         * Accion: Verifica si UserEmail se encuentran en la base de datos
         * Retorno: Boolean - Verdadero si UserEmail ya existe en la base de datos. Falso en caso contrario.
         */
        public async Task<bool> VerificarSiExisteEmail(string UserEmail)
        {
            if (!String.IsNullOrEmpty(UserEmail))
            {
                ApplicationUser usuario = await FindByEmailAsync(UserEmail);
                if (usuario == null)
                    return false;
                return true;
            }
            return false;
        }



        /*
         * Creador: Maximo Hernandez
         * Accion: Verifica si UserRut se encuentran en la base de datos
         * Retorno: Boolean - Verdadero si UserRut ya existe en la base de datos. Falso en caso contrario.
         */
        public async Task<bool> VerificarSiExisteRut(string UserRut)
        {
            if (!String.IsNullOrEmpty(UserRut))
            {
                ApplicationUser usuario = await FindByRutAsync(UserRut);
                if (usuario == null)
                    return false;
                return true;
            }
            return false;
        }

        /*
        * Creador: Gabriel Sanhueza
        * Accion: Verifica si la contraseña no es la misma que la que ya esta
        * Retorno: Boolean - Verdadero si es la misma. Falso en caso contrario
        */
        public async Task<bool> VerificarSiExisteContrasenia(string UserRut, string password)
        {
            if (!String.IsNullOrEmpty(UserRut) && !String.IsNullOrEmpty(password))
            {
                ApplicationUser usuario = await FindByRutAsync(UserRut);
                return PasswordHasher.VerifyHashedPassword(usuario.PasswordHash, password) == PasswordVerificationResult.Success;
            }
            return false;
        }

        /*
         * Creador: Maximo Hernandez
         * Accion: Modifica la disponbilidad de vinculacion de un usuario.
         * Retorno: Task del usuario.
         */
        public async Task setDisponibilidadVinculacionAsync(string UserId, bool IsEnabled)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var store = new UserStore<ApplicationUser>(db);
                var usuarioTask = store.FindByIdAsync(UserId);
                ApplicationUser usuario = await usuarioTask;
                await store.SetDisponibilidadVinculacionAsync(usuario, IsEnabled);
                store.UpdateAsync(usuario);
            }
        }

        /*
         * Creador: Maximo Hernandez
         * Accion: Obtiene el nombre del usuario identificado.
         * Retorno: Task<string> que contiene el nombre del usuario identificado.
         */
        public async Task<string> getNombreUsuarioIdentificado(string UserId)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var store = new UserStore<ApplicationUser>(db);
                var usuarioTask = store.FindByIdAsync(UserId);
                ApplicationUser usuario = await usuarioTask;
                return await store.getNombreUsuarioAsync(usuario);
            }
        }

        /*
         * Creador: Maximo Hernandez
         * Accion: Obtiene la disponibilidad del usuario identificado.
         * Retorno: Task<string> que contiene el nombre del usuario identificado.
         */
        public async Task<bool> getDisponibilidadVinculacionUsuarioIdentificado(string UserId)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var store = new UserStore<ApplicationUser>(db);
                var usuarioTask = store.FindByIdAsync(UserId);
                ApplicationUser usuario = await usuarioTask;
                return await store.getDisponibilidadVinculacionUsuarioAsync(usuario);
            }
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}

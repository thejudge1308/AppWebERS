using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppWebERS.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "RUt")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Desea recordar en este navegador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "RUt")]
        public string Email { get; set; }
    }

    /**
     *   
     * <summary>
     *  Autor: Patricio Quezada Laras. 
     *  Modelo de la vista de LOGIN.
     * </summary>
     * 
     */
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Rut")]
        [RegularExpression("([1-9][0-9]*)",ErrorMessage ="El Campo Rut debe tener solo numeros.")]
        [StringLength(9,ErrorMessage ="El campo Rut debe tener 8 caractener como minimo y 9 maximo.s",MinimumLength =8)]
        /**
         * <param name="Rut">Rut</param>  
         */
        public string RUt { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        
        /**
         * <param name="Password">Contraseña</param>  
         */
        public string Password { get; set; }

        [Display(Name = "Recordar contraseña?")]

        /**
        * <summary> For more information = https://www.youtube.com/watch?v=rWK_VlekdwM </summary>>
        */
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage ="El campo Rut es obligatorio.")]
        [RegularExpression("[0-9]*", ErrorMessage ="Rut no válido.")]
        [StringLength(8,ErrorMessage ="El rut debe tener entre 7 a 8 caracteres (sin guión ni digito verif.)",MinimumLength =7)]
        [Display(Name = "Rut")]
        public string Rut { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [RegularExpression("([ ]?[a-zA-Z])*",ErrorMessage ="Nombre no válido.")]
        [StringLength(50,ErrorMessage ="El largo del nombre deber ser entre 1 a 50 caracteres.",MinimumLength =1)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [EmailAddress]
        [Display(Name = "RUt")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [StringLength(16, ErrorMessage = "La contraseña debe tener de 3 a 16 caracteres.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirme Contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas ingresadas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "RUt")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    /**
     *   
     * <summary>
     *  Autor: Patricio Quezada Laras. 
     *  Modelo de la vista de ForgotPassword.
     * </summary>
     * 
     */
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

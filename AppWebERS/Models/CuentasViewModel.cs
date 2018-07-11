﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppWebERS.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Rut")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "El campo Rut debe tener solo números.")]
        [StringLength(8, ErrorMessage = "El campo Rut debe tener 7 caractener como minimo y 8 como máximo", MinimumLength = 7)]
        /**
         * <param name="Rut">Rut</param>  
         */
        public string Rut { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]

        /**
         * <param name="Password">Contraseña</param>  
         */
        public string Contrasenia { get; set; }

        [Display(Name = "Recordar contraseña?")]

        /**
        * <summary> For more information = https://www.youtube.com/watch?v=rWK_VlekdwM </summary>>
        */
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

      

        [Required(ErrorMessage = "El campo Rut es obligatorio.")]
        [RegularExpression("[0-9]*", ErrorMessage = "Rut no válido.")]
        [StringLength(8, ErrorMessage = "El rut debe tener entre 7 a 8 caracteres (sin guión ni digito verif.)", MinimumLength = 7)]
        [Display(Name = "Rut")]
        public string Rut { get; set; }

       /** [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [RegularExpression("([ ]?[a-zA-Z])*", ErrorMessage = "Nombre no válido.")]
        [StringLength(50, ErrorMessage = "El largo del nombre deber ser entre 1 a 50 caracteres.", MinimumLength = 1)]
        [Display(Name = "Nombre de usuario")]
        public string NombreUsuario { get; set; }*/

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El largo del nombre deber ser entre 5 a 50 caracteres.", MinimumLength = 5)]
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }
    }


    public class ModificarViewModel
    {
        private string _nombre;
        private string _email;
        private string _contrasenia;
        private bool _estado;

        [RegularExpression("([ ]?[a-zA-Z])*", ErrorMessage = "Nombre no válido.")]
        [StringLength(50, ErrorMessage = "El largo del nombre deber ser entre 1 a 50 caracteres.", MinimumLength = 1)]
        [Display(Name = "Nombre")]
        public string Nombre {
            get
            {
                if (_nombre == "") return AntiguoNombre;
                return _nombre;
            }
            set { _nombre = value; }
        }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email
        {
            get
            {
                if (_email == "") return AntiguoEmail;
                return _email;
            }
            set { _email = value; }
        }

        [StringLength(16, ErrorMessage = "La contraseña debe tener de 3 a 16 caracteres.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        public bool Estado { get; set; }

        public string Rut { get; set;}
        public string AntiguoNombre {get; set;}
        public string AntiguoEmail { get; set; }

        public ModificarViewModel() {
            _nombre = "";
            _email = "";
            _contrasenia = "";
        }

        public ModificarViewModel(ApplicationUser usuario) : this()
        {
            this.Rut = usuario.Rut;
            this.AntiguoNombre = usuario.UserName;
            this.AntiguoEmail = usuario.Email;
            this._estado = usuario.Estado;
        }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
using System;
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
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "El Campo Rut debe tener solo numeros.")]
        [StringLength(9, ErrorMessage = "El campo Rut debe tener 8 caractener como minimo y 9 maximos", MinimumLength = 8)]
        /**
         * <param name="Rut">Rut</param>  
         */
        public string Id { get; set; }

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
}
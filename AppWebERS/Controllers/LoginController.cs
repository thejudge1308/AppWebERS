using MySql.Data.MySqlClient;
using System;
using System.Web.Mvc;

namespace AppWebERS.Controllers
{
    public class LoginController : Controller
    {
        private String NombreUsuario;
        private String Contrasenna;

        public LoginController()
        {
        }

        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Indica si una cadena de texto(ID usuario o contrasenia) es null o vacia.
         * </summary>
         * <param name="texto">Contiene un string con el texto a verificar</param>
         * <returns> true si es vacio, false en caso contrario </returns>
         */
        private bool VerificarCampoVacio(string texto) {
            return String.IsNullOrEmpty(texto);
        }
        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Indi ca si una cadena de texto(ID usuario o contrasenia) contiene espacios en su contenido.
         * </summary>
         *<param name="texto">Contiene un string con el texto a verificar</param>
         * <returns> true si contiene espacios, false en caso contrario</returns>
         */
        private bool VerificarEspaciosEnCampo(string texto) {
            return texto.Contains(" ");
        }


        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Indica si una cadena de texto supera un largo maximo estimado por el parametro largoMaximo.
         * </summary>
         * <param name="texto">Contiene un string con el texto a verificar</param>
         * <param name="largoMaximo">Entero que representa el largo maximo que puede tener el string texto</param>
         * <returns>true si texto es igual o menor que el largoMaximo, false en caso contrario</returns>
         */
        private bool VerificarLargoCampo(string texto, int largoMaximo) {
            return texto.Length <= largoMaximo;
        }



        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Obtiene un usuario desde la base de datos.
         * </summary>
         * <param name="id">Contiene un string con el id o rut a obtener desde la bd</param>
         * <returns>Debe retornar el objeto Usuario</returns>
         */
        private bool/*Usuario*/ ObtenerUsuario(string id)
        {
            //MySqlDataReader data = RealizarConsulta("SELECT * FROM Usuario WHERE rut='" + id + "';");
            return true;
        }
    }
}
﻿using AppWebERS.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace AppWebERS.Controllers{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(){
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return this.Ingresar(model.RutName, model.Contrasenia);
        }

        //private MySqlConnection Con;//solo para test
        private ConectorBD conexion;


        public LoginController()
        {
            //this.Con = new MySqlConnection("Server=localhost;Port=3306;Database=appers;Uid=conexion;Password=1234");
            this.conexion = ConectorBD.Instance;
            Debug.WriteLine(this.conexion);


        }


        /**
         *<autor>Ariel Cornejo - Diego Iturriaga</autor>
         * <summary>Metodo que ejecuta todas las validaciones correspondientes para
         * verificar que los datos del usuario y su respectiva contraseña son validas
         * segun el formato y corresponden a los datos registrados en la base de datos</summary>
         * 
         * <param name="usuario"> Es el string que contiene el id del usuario ingresado en la ventana de login</param>
         * <param name="contrasenia"> Es el string que contiene la contraseña ingresada en la ventana de login</param>
         *
         * <returns> 
         * Retorna true si permite el acceso y false si se niega el acceso.
         * </returns>
         * 
         */
        private Boolean PermitirAccesoUsuario(String usuario, String contrasenia)
        {
            //Se realizan las validaciones de los campos
            if (this.verificarCampoVacio(usuario) || this.verificarCampoVacio(contrasenia))
            {
                return false;
            }
            if (this.VerificarEspaciosEnCampo(usuario))
            {
                return false;
            }
            if (!this.VerificarLargoCampo(usuario,12) || !this.VerificarLargoCampo(contrasenia,16))
            {
                return false;
            } 
            if (!ValidarUsuario(usuario))
            {
                return false;
            }
            if (!this.ValidarContrasenia(usuario,contrasenia))
            {
                return false;
            }
            //Se verficia exitosamente
            return true;
        }

        /**
         * <author>
         * Ariel Cornejo
         * </author>
         * <summary>
         * Este metodo se encarga de realizar una consulta a la base de datos para comprobar si el usuario 
         * esta registrado
         * </summary>
         * <param name="usuario"> El string con el id del usuario</param>
         * <returns>
         * valor booleano, true si existe o false en caso contrario
         * </returns>
         */
        private Boolean ValidarUsuario(String usuario)
        {
            String consultaExistaUsuario = "SELECT usuario.rut as rut" +
                                            " FROM usuario" +
                                            " WHERE usuario.rut =  '"+ usuario +"';";
            MySqlDataReader readerUsuario = this.conexion.RealizarConsulta(consultaExistaUsuario);
            if (readerUsuario ==null)
            {
                conexion.CerrarConexion();
                return false;
            }
            conexion.CerrarConexion();
            return true;
            
        }
        /**
         * <autor>Diego Iturriaga</autor>
         * <summary>
         * es la correspondiente a dicho usuario.
         * El metodo se encarga de verificar si la contraseña asociada a un usuario
         * </summary> 
         * 
         * <param name="usuario"> Es el string que contiene el id del usuario ingresado en la ventana de login</param>
         * <param name="contrasenia"> Es el string que contiene la contraseña ingresada en la ventana de login</param>
         * <returns> 
         * Retorna true si la contraseña que se verifica es correcta - Retorna false si la contraseña
         * que se verifica es incorrecta al no coincidir con la contraseña asociada al usuario en la base da datos
         * </returns>
         */
        private Boolean ValidarContrasenia(String usuario, String contrasenia)
        {
            string consulta = "SELECT usuario.contrasenia FROM usuario WHERE rut = '"+usuario+"';";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            
            if (reader != null)
            { 
                reader.Read();
                string contrasennaBD = reader["contrasenia"].ToString();
                //Desencriptar contraseña de la BD 
                contrasenia = this.encriptarClave(contrasenia);
                System.Diagnostics.Debug.WriteLine(contrasenia);
                if(contrasennaBD.Equals(contrasenia))
                {
                    conexion.CerrarConexion();
                    return true;
                }
                else {
                    conexion.CerrarConexion();
                    return false;
                }
                
            }
            conexion.CerrarConexion();
            return false;
        }
        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Indica si una cadena de texto(ID usuario o contrasenia) es null o vacia.
         * </summary>
         * <param name="texto">Contiene un string con el texto a verificar</param>
         * <returns> true si es vacio, false en caso contrario </returns>
         */
        private Boolean verificarCampoVacio(string texto) {
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
        private Boolean VerificarEspaciosEnCampo(string texto) {
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
        private Boolean VerificarLargoCampo(string texto, int largoMaximo) {
            return texto.Length <= largoMaximo;
        }



        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Obtiene un usuario desde la base de datos.
         * </summary>
         * <param name="id">Contiene un string con el id o rut a obtener desde la bd</param>
         * <param name="contrasenia">Contiene un string con la contrasenia relacionada con el id o rut que se encuentra en la bd</param>
         * <returns>Debe retornar el objeto Usuario, si no se logro obtener se retorna null</returns>
         */

        public ActionResult Ingresar(string Rut, string contrasenia)
        {
            if (PermitirAccesoUsuario(Rut, contrasenia))
            {
                string consulta = "SELECT * FROM Usuario WHERE rut='" + Rut + "';";
                MySqlDataReader data = this.conexion.RealizarConsulta(consulta);
                if (data != null)
                {
                    data.Read();
                    string rutBD = data["rut"].ToString();
                    string nombreBD = data["nombre"].ToString();
                    string correoBD = data["correo_electronico"].ToString();
                    string contraseniaBD = data["contrasenia"].ToString();
                    string tipoBD = data["tipo"].ToString();
                    string stringEstadoBD = data["estado"].ToString();
                    bool estadoBD = false;
                    if (Int32.Parse(stringEstadoBD) == 1) estadoBD = true;
                    Usuario usuario = new Usuario(rutBD, nombreBD, correoBD, contraseniaBD, tipoBD, estadoBD);
                    this.conexion.CerrarConexion();

                    if (!usuario.Estado)
                    {
                        ViewBag.Message = "Acceso denegado";
                        return View();
                    }

                    if(usuario.Tipo == "SYSADMIN")
                    {
                        return RedirectToAction("Principal", "SysAdmin2", new { name = nombreBD });
                    }
                    else if(usuario.Tipo == "USUARIO")
                    {
                        return RedirectToAction("Principal", "UserView", new { name = nombreBD });
                    }
                    return View();

                }
                else
                {
                    this.conexion.CerrarConexion();
                    ViewBag.Message = "Error de conexión con el servidor";
                    return View();
                }
            }
            else
            {
                this.conexion.CerrarConexion();
                ViewBag.Message = "Datos no válidos. Acceso denegado";
                return View();
            }
        }
        /**
        * <author>Ariel Cornejo</author>
        * <summary>
        * Comprueba el estado de un usuario
        * </summary>
        * <param name="usuario">Objeto usuario que sera comprobado</param>
        * <returns>Valor booleano, true si esta habilitado y false en caso contrario</returns>
        */
        public bool ComprobarEstadoUsuario(Usuario usuario)
        {
            if (usuario.Estado)
            {
                return true;
            }
            return false;
        }

        private string encriptarClave(string original)
        {

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(original));
                return Convert.ToBase64String(data);
            }
        }
    }

}
using MySql.Data.MySqlClient;
using System;
using System.Web.Mvc;

namespace AppWebERS.Controllers
{
    public class LoginController 
    {
        private String NombreUsuario;
        private String Contrasenna;
        private MySqlConnection Con;//solo para test

        public LoginController()
        {
            Con = new MySqlConnection("Server=localhost;Port=3306;Database=appers;Uid=conexion;password=1234");
        }

        public int PermitirAccesoUsuario(String usuario, String contrasenia)
        {
            //Se realizan las validaciones de los campos
            if (this.VerificarCampoVacio(usuario) || this.VerificarCampoVacio(contrasenia))
            {
                return 0;
            }
            if (this.VerificarEspaciosEnCampo(usuario) || this.VerificarEspaciosEnCampo(contrasenia))
            {
                return 0;
            }
            if (!this.VerificarLargoCampo(usuario,12) || !this.VerificarLargoCampo(contrasenia,16))
            {
                return 0;
            } 
            if (!validacionUsuario(usuario))
            {
                return 0;
            }
            if (!this.validacionContrasenia(usuario,contrasenia))
            {
                return 0;
            }
            //Se verficia exitosamente
            return 2;
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
        private Boolean validacionUsuario(String usuario)
        {
            String consultaExistaUsuario = "SELECT usuario.rut as rut" +
                                            " FROM usuario" +
            
                                            " WHERE usuario.rut =  '"+ usuario +"';";
            MySqlDataReader readerUsuario = this.RealizarConsulta(consultaExistaUsuario);
            if (readerUsuario ==null)
            {
                Con.Close();
                return false;
            }
            Con.Close();
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
         * <returns> Retorna true si la contraseña que se verifica es correcta - Retorna false si la contraseña
         * 
         * que se verifica es incorrecta al no coincidir con la contraseña asociada al usuario en la base da datos
         * </returns>
         * 
         *
         */
        private Boolean validacionContrasenia(String usuario, String contrasenia)
        {
            string consulta = "SELECT usuario.contrasenia FROM usuario WHERE rut = '"+usuario+"';";
            MySqlDataReader reader = this.RealizarConsulta(consulta);
            
            if (reader != null)
            { 
                reader.Read();
                string contrasennaBD = reader["contrasenia"].ToString();
                //Desencriptar contraseña de la BD
                if (contrasennaBD==contrasenia)
                {
                    Con.Close();
                    return true;
                }
                else {
                    Con.Close();
                    return false;
                }
                
            }
            Con.Close();
            return false;
        }

        private MySqlDataReader RealizarConsulta(string consulta)
        { 
            MySqlCommand command = Con.CreateCommand();
            command.CommandText = consulta;
            try
            {
                Con.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) return reader;
            }
            catch (Exception e)
            {
                Con.Close();
                Console.WriteLine(e.Message);
            }
            return null;
        }
        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Indica si una cadena de texto(ID usuario o contrasenia) es null o vacia.
         * </summary>
         * <param name="texto">Contiene un string con el texto a verificar</param>
         * <returns> true si es vacio, false en caso contrario </returns>
         */
        private Boolean VerificarCampoVacio(string texto) {
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
        public Usuario ObtenerUsuario(string id, string contrasenia)
        {
            if (PermitirAccesoUsuario(id, contrasenia) == 2)
            {
                string consulta = "SELECT * FROM Usuario WHERE rut='" + id + "';";
                MySqlDataReader data = this.RealizarConsulta(consulta);
                if (data != null)
                {
                    data.Read();
                    string rutBD = data["rut"].ToString();
                    string nombreBD = data["nombre"].ToString();
                    string correoBD = data["correo_electronico"].ToString();
                    string contraseniaBD = data["contrasenia"].ToString();
                    string tipoBD = data["tipo"].ToString();
                    //bool estadoBD = data["estado"].ToBoolean();
                    Usuario usuario = new Usuario(rutBD, nombreBD, correoBD, contraseniaBD, tipoBD/*,estadoBD*/);
                    Con.Close();
                    return usuario;

                }
                else
                {
                    Con.Close();
                    return null;
                }
            }
            else
            {
                Con.Close();
                return null;
            }
        }
    }
}
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

        public int PermitirAccesoUsuario(String usuario, String Contrasenia)
        {
            //Se realiza una consulta para verificar si el usuario existe en la base de datos 
            if (!validacionUsuario(usuario))
            {
                return 0;
            }
            //Se finaliza consulta 1
            return 2;
        }

       /**
        * <author>
        * </author>
        * Ariel Cornejo
        * <summary>
        * Este metodo se encarga de realizar una consulta a la base de datos para comprobar si el usuario 
        * esta registrado
        * <param name="usuario"> El string con el id del usuario</param>
        * </summary>
        * <returns>
        * </returns>
        * valor booleano, true si existe o false en caso contrario
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
        public Boolean validacionContrasenna(String usuario, String contrasenia)
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
    }
}
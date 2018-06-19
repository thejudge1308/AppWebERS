using MySql.Data.MySqlClient;
using System;
//using System.Web.Mvc;

namespace AppWebERS.Controllers
{
    public class LoginController 
    {
        private String NombreUsuario;
        private String Contrasenna;

        private Conector_BD conector;//solo para test

        public LoginController()
        {
        }

        /**
         * <autor>Diego Iturriaga</autor>
         * <summary>
         * El metodo se encarga de verificar si la contraseña asociada a un usuario
         * es la correspondiente a dicho usuario.
         * </summary> 
         * 
         * <param name="usuario"> Es el string que contiene el id del usuario ingresado en la ventana de login</param>
         * <param name="contrasenia"> Es el string que contiene la contraseña ingresada en la ventana de login</param>
         * 
         * <returns> Retorna true si la contraseña que se verifica es correcta - Retorna false si la contraseña
         * que se verifica es incorrecta al no coincidir con la contraseña asociada al usuario en la base da datos
         * </returns>
         * 
         *
         */
        public Boolean ValidacionContrasenna(String usuario, String contrasenia)
        {
            string consulta = "SELECT contrasenna FROM testlogin.usuario WHERE rut = '"+usuario+"';";
            MySqlDataReader reader = this.RealizarConsulta(consulta);
            if (reader != null)
            {
                reader.Read();
                string contrasennaBD = reader["contrasenna"].ToString();
                //Desencriptar contraseña de la BD
                if (contrasennaBD==contrasenia)
                {
                    //this.conector.CerrarConexion();
                    return true;
                }
                else
                {
                    //this.conector.CerrarConexion();
                    return false;
                }
                
            }
            //this.conector.CerrarConexion();
            return false;
        }

        private MySqlDataReader RealizarConsulta(string consulta)
        {
            MySqlConnection Con = new MySqlConnection("Server=localhost;Port=3306;Database=testlogin;Uid=conexion;password=1234");
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
                Console.WriteLine(e.Message);
                Con.Close();
            }
            return null;
        }
    }
}
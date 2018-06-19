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
            return true;
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
                Console.WriteLine(e.Message);
                Con.Close();
            }
            return null;
        }
    }
}
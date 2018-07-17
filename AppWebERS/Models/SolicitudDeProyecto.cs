using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace AppWebERS.Models{

    public class SolicitudDeProyecto
    {
        /*
         * Constructor vacío de la clase usuario (se agrega para cualquier otro uso que se le de en un futuro).
         * 
         */
        public SolicitudDeProyecto(String idJefe)
        {
            this.jefeProyecto = idJefe;
        }

        //privatw conector solo para testing.
        private ConectorBD conector = ConectorBD.Instance;


        public SolicitudDeProyecto(String NombreUsuario, String NombreProyecto)
        {
            this.usuario = NombreUsuario;
            this.proyecto = NombreProyecto;
        }

        public String jefeProyecto { get; set; }
        public String usuario { get; set; }
        public String proyecto { get; set; }
        /*
         * 
         * ERA PARA PROBAR QUE FUNCIONE
        public List<SolicitudDeProyecto> ListarTodos()
        {
            SolicitudDeProyecto s1 = new SolicitudDeProyecto("Francisco Medel", "proyecto 1");
            SolicitudDeProyecto s2 = new SolicitudDeProyecto("Jose Morales", "proyecto 2");
            List<SolicitudDeProyecto> listaSolicitudes = new List<SolicitudDeProyecto>();
            listaSolicitudes.Add(s1);
            listaSolicitudes.Add(s2);
            return listaSolicitudes;
        }

        */
        public List<SolicitudDeProyecto> ListarTodos() {

            List<SolicitudDeProyecto> listaSolicitudes = new List<SolicitudDeProyecto>();

            /*PARA ESTA CONSULTA HAY QUE HACER UN CROSSJOIN CON EL JEFE DE PROYECTO
             */
            string consulta = "SELECT * FROM ????????"; //nose como se llama la tabla
            MySqlDataReader reader = this.conector.RealizarConsulta(consulta);
            if(reader == null)
            {
                this.conector.CerrarConexion();
                return null;
            }
            else
            {
               while (reader.Read())
                {
                    string IdUsuario = reader.GetString(0);
                    string IdProyecto = reader.GetString(1);

                    string consultaUsuario = "SELECT "+IdUsuario+" FROM usuario"; //consulta para obtener nombre del usuario
                    string consultaProyecto = "SELECT " + IdProyecto + " FROM proyecto"; //consulta para obtener nombre del proyecto

                    MySqlDataReader reader2 = this.conector.RealizarConsulta(consultaUsuario);
                    string nombreUsuario = reader2.GetString(1);

                    reader2 = this.conector.RealizarConsulta(consultaProyecto);
                    string nombreProyecto = reader2.GetString(1);

                    listaSolicitudes.Add(new SolicitudDeProyecto(nombreUsuario,nombreProyecto) );
                }

                this.conector.CerrarConexion();
                return listaSolicitudes;
            }

        }
        

        /**
         * Método para listar un usuario específico
         * <returns>Retorna un usuario específico.</returns>
         **/

        public void ListarEspecifico(Usuario usuario)
        {

        }


        /**
         * Método para Crear un Usuario
         * <returns>Retorna un boolean que indica la correcta creación del usuario.</returns>
         **/

        public bool Crear()
        {

            return true;
        }

    }

}
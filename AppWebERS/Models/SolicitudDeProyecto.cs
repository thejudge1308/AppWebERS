using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using AspNet.Identity.MySQL;
using Microsoft.AspNet.Identity;


namespace AppWebERS.Models
{

    public class SolicitudDeProyecto
    {
        /*
         * Constructor vacío de la clase usuario (se agrega para cualquier otro uso que se le de en un futuro).
         * 
         */
        
        public SolicitudDeProyecto(String idJefe,int idProyecto){
            this.jefeProyecto = idJefe;
            this.idProyecto = idProyecto;
            this.listaSolicitudes = new List<SolicitudDeProyecto>();

        }

        private ApplicationDbContext conexion = ApplicationDbContext.Create();

        public SolicitudDeProyecto(String NombreUsuario, String idUsuario, String NombreProyecto, int idProyecto){
            this.usuario = NombreUsuario;
            this.idUsuario = idUsuario;
            this.proyecto = NombreProyecto;
            this.idProyecto = idProyecto;
            this.listaSolicitudes = new List<SolicitudDeProyecto>();
        }

        public List<SolicitudDeProyecto> listaSolicitudes { get; set; }

        public String jefeProyecto { get; set; }
        public String usuario { get; set; }
        public String idUsuario { get; set; } 
        public String proyecto { get; set; }
        public int idProyecto { get; set; }
       


        /**
         * Método para listar un usuario específico
         * <returns>Retorna un usuario específico.</returns>
         **/

        public void ListarEspecifico(Usuario usuario){

        }


        /**
         * Método para Crear un Usuario
         * <returns>Retorna un boolean que indica la correcta creación del usuario.</returns>
         **/

        public bool Crear(){

            return true;
        }
    }

}
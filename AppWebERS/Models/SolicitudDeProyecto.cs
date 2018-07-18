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
        public SolicitudDeProyecto(String idJefe,String idProyecto){
            this.jefeProyecto = idJefe;
            this.idProyecto = idProyecto;
        }

        //privatw conector solo para testing.
        private ConectorBD conector = ConectorBD.Instance;


        public SolicitudDeProyecto(String NombreUsuario, String idUsuario, String NombreProyecto, String idProyecto){
            this.usuario = NombreUsuario;
            this.idUsuario = idUsuario;
            this.proyecto = NombreProyecto;
            this.idProyecto = idProyecto;
        }

        public String jefeProyecto { get; set; }
        public String usuario { get; set; }
        public String idUsuario { get; set; } 
        public String proyecto { get; set; }
        public String idProyecto { get; set; }
       
        /*
        public List<SolicitudDeProyecto> ListarTodos()
        {
            SolicitudDeProyecto s1 = new SolicitudDeProyecto("Francisco Medel","id", "proyecto 1","idp");
            SolicitudDeProyecto s2 = new SolicitudDeProyecto("Jose Morales","id", "proyecto 2","idp");
            List<SolicitudDeProyecto> listaSolicitudes = new List<SolicitudDeProyecto>();
            listaSolicitudes.Add(s1);
            listaSolicitudes.Add(s2);
            return listaSolicitudes;
        }
        */
        

        /*
         * Jose Nuñez, Manuel Gonzalez
         * Permite mostrar todas las solicitudes de proyectos que existen en el sistema
         * 
         */
        public List<SolicitudDeProyecto> ListarTodos() {

            List<SolicitudDeProyecto> listaSolicitudes = new List<SolicitudDeProyecto>();

            string consulta = "SELECT  users.userName, users.Id, proyecto.nombre, proyecto.id_proyecto " +
                "FROM users, proyecto, solicitud_vinculacion_proyecto,Vinculo_usuario_proyecto " +
                "WHERE vinculo_usuario_proyecto.rol = 'JEFEPROYECTO' AND vinculo_usuario_proyecto.ref_proyecto = proyecto.id_proyecto " +
                "AND vinculo_usuario_proyecto.ref_usuario = '" + this.jefeProyecto + "' AND users.id = solicitud_vinculacion_proyecto.ref_solicitante " +
                 " AND '"+ this.idProyecto + "' = solicitud_vinculacion_proyecto.ref_proyecto";


            MySqlDataReader reader = this.conector.RealizarConsulta(consulta);
            if(reader == null){
                this.conector.CerrarConexion();
                return null;
            }
            else{
               while (reader.Read()){
                    string nombreUsuario = reader.GetString(0);
                    string idUsuario = reader.GetString(1);
                    string nombreProyecto = reader.GetString(2);
                    string idProyecto = reader.GetString(3);

                    /*
                    string consultaUsuario = "SELECT * FROM users WHERE Id = "+IdUsuario+""; //consulta para obtener nombre del usuario
                    string consultaProyecto = "SELECT * FROM proyecto WHERE id_proyecto = "+IdProyecto+""; //consulta para obtener nombre del proyecto

                    MySqlDataReader reader2 = this.conector.RealizarConsulta(consultaUsuario);
                    string nombreUsuario = reader2.GetString(0);

                    reader2 = this.conector.RealizarConsulta(consultaProyecto);
                    string nombreProyecto = reader2.GetString(0);
                    */

                    listaSolicitudes.Add(new SolicitudDeProyecto(nombreUsuario, idUsuario,nombreProyecto, idProyecto) );
                }

                this.conector.CerrarConexion();
                return listaSolicitudes;
            }

        }
        

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
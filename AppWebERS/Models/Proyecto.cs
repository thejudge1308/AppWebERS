using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;
using System.Diagnostics;
/**
* Autor: Gerardo Estrada (Meister1412)
**/

namespace AppWebERS.Models{
    public class Proyecto {

        #region Definicion de permisos para la vista de los proyectos
        /*
         * Autor: Patricio Quezada
         * Descripcion: Define los permisos para la visualizacion de los proyectos.
        */
        public const int AUTH_COMO_JEFE_DE_PROYECTO = 0;
        public const int AUTH_COMO_SYSADMIN = 1;
        public const int AUTH_COMO_USUARIO = 2;
        public const int NO_AUTH = 3;
        #endregion

        #region Roles de un proyecto en la Base de datos
        public const String JefeDeProyecto_RolBD = "JEFEPROYECTO";
        public const String Usuario_RolBD = "USUARIO";
        public const String SysAdmin_RolBD = "SYSADMIN";
        #endregion

        #region Atributos
        private ConectorBD conexion;
        #endregion

        public Proyecto() {
        }

        /**
         * Constructor de la clase Proyecto
         * 
         * <param name = "idProyecto" > El identificador del proyecto.</param>
         * <param name = "nombre" > El identificador del proyecto.</param>
         * <param name = "proposito" > El proposito del proyecto.</param>
         * <param name = "alcance" > El alcance del proyecto.</param>
         * <param name = "contexto" > El contexto del proyecto.</param>
         * <param name = "definiciones" > Las definiciones del proyecto.</param>
         * <param name = "acronimos" > Los acronimos del proyecto.</param>
         * <param name = "abreviaturas" > Las abreviaturas del proyecto.</param>
         * <param name = "referencias" > Las referencias del proyecto.</param>
         * <param name = "ambienteOperacional" > El ambiente operacional del proyecto.</param>
         * <param name = "relacionProyectos" > La relacion con otros proyectos del proyecto.</param>
         * <param name = "usuarios" > La lista de usuarios involucrados en el proyecto.</param>
         * <param name = "requisitos" > La lista de requisitos asociados al proyecto.</param>
         * <param name = "casosDeUso" > La lista de casos de uso asociados al proyecto.</param>
         * <param name = "actores" > La lista de actores asociados al proyecto.</param>
         **/
        public Proyecto(int idProyecto, string nombre,string proposito, string alcance, string contexto, string definiciones, string acronimos, string abreviaturas, string referencias, string ambienteOperacional, string relacionProyectos, List<Usuario> usuarios, List<Requisito> requisitos, List<CasoDeUso> casosDeUso, List<Actor> actores) {
            this.IdProyecto = idProyecto;
            this.Nombre = nombre;
            this.Proposito = proposito;
            this.Alcance = alcance;
            this.Contexto = contexto;
            this.Definiciones = definiciones;
            this.Acronimos = acronimos;
            this.Abreviaturas = abreviaturas;
            this.Referencias = referencias;
            this.AmbienteOperacional = ambienteOperacional;
            this.RelacionProyectos = relacionProyectos;
            this.Usuarios = new List<Usuario>();
            this.Requisitos = new List<Requisito>();
            this.CasosDeUso = new List<CasoDeUso>();
            this.Actores = new List<Actor>();
        }


        /**
         * Autor: Patricio Quezada 
         * <param name = "idProyecto" > El identificador del proyecto.</param>
         * <param name = "nombre" > El identificador del proyecto.</param>
         * <param name = "proposito" > El proposito del proyecto.</param>
         * <param name = "alcance" > El alcance del proyecto.</param>
         * <param name = "contexto" > El contexto del proyecto.</param>
         * <param name = "definiciones" > Las definiciones del proyecto.</param>
         * <param name = "acronimos" > Los acronimos del proyecto.</param>
         * <param name = "abreviaturas" > Las abreviaturas del proyecto.</param>
         * <param name = "referencias" > Las referencias del proyecto.</param>
         * <param name = "ambienteOperacional" > El ambiente operacional del proyecto.</param>
         * <param name = "relacionProyectos" > La relacion con otros proyectos del proyecto.</param>
         * <param name = "usuarios" > La lista de usuarios involucrados en el proyecto.</param>
         * <param name = "requisitos" > La lista de requisitos asociados al proyecto.</param>
         * <param name = "casosDeUso" > La lista de casos de uso asociados al proyecto.</param>
         * <param name = "actores" > La lista de actores asociados al proyecto.</param>
         **/
        public Proyecto(int idProyecto, string nombre, string proposito, string alcance, string contexto, string definiciones, string acronimos, string abreviaturas, string referencias, string ambienteOperacional, string relacionProyectos) {
            IdProyecto = idProyecto;
            Nombre = nombre;
            Proposito = proposito;
            Alcance = alcance;
            Contexto = contexto;
            Definiciones = definiciones;
            Acronimos = acronimos;
            Abreviaturas = abreviaturas;
            Referencias = referencias;
            AmbienteOperacional = ambienteOperacional;
            RelacionProyectos = relacionProyectos;
        }

        /**
         * Setter y Getter de ID del proyecto
         * 
         * <param name = "idProyecto" > El identificador del proyecto.</param>
         * <returns>Retorna el valor int del identificador.</returns>
         * 
         **/
        [Display(Name = "Id del Proyecto")]
        [StringLength(128, ErrorMessage = "Este campo debe tener maximo 128 caracteres.", MinimumLength = 1)]
        public int IdProyecto {get; set;}

        /**
         * Setter y Getter de Nombre del proyecto
         * 
         * <param name = "nombre" > El identificador del proyecto.</param>
         * <returns>Retorna el valor string del nombre.</returns>
         * 
         **/
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(255, ErrorMessage = "Es requerido", MinimumLength = 1)]
        public string Nombre { get; set;}

        /**
         * Setter y Getter del proposito del proyecto
         * 
         * <param name = "proposito" > El proposito del proyecto.</param>
         * <returns>Retorna el valor string del proposito.</returns>
         * 
         **/
        [Required]
        [Display(Name = "Proposito")]
        [StringLength(255, ErrorMessage = "Es requerido", MinimumLength = 1)]
        public string Proposito {get; set;}

        /**
         * Setter y Getter del alcance del proyecto
         * 
         * <param name = "alcance" > El alcance del proyecto.</param>
         * <returns>Retorna el valor string del alcance.</returns>
         * 
         **/
        [Required]
        [Display(Name = "Alcance")]
        [StringLength(255, ErrorMessage = "Es requerido", MinimumLength = 1)]
        public string Alcance {get; set;}

        /**
         * Setter y Getter del contexto del proyecto
         * 
         * <param name = "contexto" > El contexto del proyecto.</param>
         * <returns>Retorna el valor string del contexto.</returns>
         * 
         **/
        [Required]
        [Display(Name = "Contexto")]
        [StringLength(255, ErrorMessage = "Es requerido", MinimumLength = 1)]
        public string Contexto {get; set;}

        /**
         * Setter y Getter del atributo que contiene las definiciones del proyecto
         * 
         * <param name = "definiciones" > Las definiciones del proyecto.</param>
         * <returns>Retorna el valor string de las definiciones.</returns>
         * 
         **/
        [Display(Name = "Definiciones")]
        public string Definiciones {get; set;}

        /**
         * Setter y Getter del atributo que contiene los acronimos del proyecto
         * 
         * <param name = "acronimos" > Los acronimos del proyecto.</param>
         * <returns>Retorna el valor string de los acronimos.</returns>
         * 
         **/
        [Display(Name = "Acronimos")]
        public string Acronimos {get; set;}

        /**
        * Setter y Getter del atributo que contiene las abreviaturas del proyecto
        * 
        * <param name = "abreviaturas" > Las abreviaturas del proyecto.</param>
        * <returns>Retorna el valor string de las abreviaturas.</returns>
        * 
        **/
        [Display(Name = "Abreviaturas")]
        public string Abreviaturas {get; set;}

        /**
        * Setter y Getter del atributo que contiene las referencias del proyecto
        * 
        * <param name = "referencias" > Las referencias del proyecto.</param>
        * <returns>Retorna el valor string de las referencias.</returns>
        * 
        **/
        [Display(Name = "Referencias")]
        public string Referencias {get; set;}

        /**
        * Setter y Getter del ambiente operacional del proyecto
        * 
        * <param name = "ambienteOperacional" > El ambiente operacional del proyecto.</param>
        * <returns>Retorna el valor string del ambiente operacional.</returns>
        * 
        **/
        [Required]
        [Display(Name = "Ambiente operacional")]
        [StringLength(255, ErrorMessage = "Es requerido", MinimumLength = 1)]
        public string AmbienteOperacional {get; set;}

        /**
        * Setter y Getter del atributo que contiene la relacion con otros proyectos
        * 
        * <param name = "relacionProyectos" > La relacion con otros proyectos del proyecto.</param>
        * <returns>Retorna el valor string de la relacion con otros proyectos del proyecto.</returns>
        * 
        **/
        [Display(Name = "Relacion con otros proyectos")]
        public string RelacionProyectos {get; set;}

        /**
         * Setter y Getter de los usuarios relacionados con el proyecto.
         * 
         * <param name = "usuarios" > La lista de usuarios involucrados en el proyecto.</param>
         * <returns>Retorna la lista de usuarios.</returns>
         * 
         **/

        public List<Usuario> Usuarios {get; set;}

        /**
         * Setter y Getter de los requisitos relacionados con el proyecto.
         * 
         * <param name = "requisitos" > La lista de requisitos asociados al proyecto.</param>
         * <returns>Retorna la lista de requisitos.</returns>
         * 
         **/

        public List<Requisito> Requisitos {get; set;}

        /**
         * Setter y Getter de los casos de uso relacionados con el proyecto.
         * 
         * <param name = "casosDeUso" > La lista de casos de uso asociados al proyecto.</param>
         * <returns>Retorna la lista de casos de uso.</returns>
         * 
         **/

        public List<CasoDeUso> CasosDeUso {get; set;}

        /**
         * Setter y Getter de los actores relacionados con el proyecto.
         * 
         * <param name = "actores" > La lista de actores asociados al proyecto.</param>
         * <returns>Retorna la lista de actores.</returns>
         * 
         **/

        public List<Actor> Actores {get; set;}

        /**
         * Método para listar todos los proyectos existentes
         **/

        public void ListarTodos() {
        }

        /**
         * Método para listar un proyecto específico
         * <returns>Retorna un proyecto específico.</returns>
         **/

        public void ListarEspecifico(Usuario usuario) {
           
        }

        /**
         * Método para seleccionar un proyecto 
         **/

        public void Seleccionar(int id) {

        }

        /**
         * Método para Crear un Proyecto
         * <returns>Retorna un boolean que indica la correcta creación del proyecto.</returns>
         **/

        public bool Crear() {
            return true;
        }

        /**
         * Creador:Patricio Quezada
         * 
         * <param name = "ID" >ID del proyecto buscado.</param>
         * <returns>El proyecto con sus respectivos datos.</returns>
         */
        public Proyecto ObtenerProyectoPorID(int ID) {
            Proyecto proyecto = null;
            this.conexion = ConectorBD.Instance;
            string consulta = "SELECT * FROM proyecto WHERE id_proyecto = " + ID + ";";
            MySqlDataReader data = this.conexion.RealizarConsulta(consulta);
            if(data != null) {
                data.Read();
                string nombre = data["nombre"].ToString();
                string proposito = data["proposito"].ToString();
                string alcance = data["alcance"].ToString();
                string contexto = data["contexto"].ToString();
                string definiciones = data["definiciones"].ToString();
                string acronimos = data["acronimos"].ToString();
                string abreviaturas = data["abreviaturas"].ToString();
                string referencias = data["referencias"].ToString();
                string ambiente_operacional = data["ambiente_operacional"].ToString();
                string relacion_con_otros_proyectos = data["relacion_con_otros_proyectos"].ToString();


                proyecto = new Proyecto(ID, nombre, proposito, alcance, contexto, definiciones, acronimos, abreviaturas, referencias, ambiente_operacional, relacion_con_otros_proyectos);
                //Debug.WriteLine(proyecto.Proposito);
                this.conexion.CerrarConexion();
            }
            return proyecto;
        }


        /**
         * Creador: Patricio Quezada 
         * <param name="IdUsuario">id del usuario</param>
         * <param name="Proyecto">id del proyecto</param>
         * <returns>El rol del usuario en un proyecto</returns>
         * <summary>Retorna el rol del usuario de un proyecto en particular</summary>
         */
        public int ObtenerRolDelUsuario(String IdUsuario,int Proyecto) {
           int permiso = NO_AUTH; 
           this.conexion = ConectorBD.Instance;
           string consulta = "SELECT Tipo From users WHERE id='" + IdUsuario + "';";
           MySqlDataReader data = this.conexion.RealizarConsulta(consulta);
           if(data != null) {
                data.Read();
                string rol = data["Tipo"].ToString().Trim();
                if(rol.Equals(SysAdmin_RolBD)) {
                        permiso = AUTH_COMO_SYSADMIN;
                } else {
                    //this.conexion.CerrarConexion();
                    //this.conexion = ConectorBD.Instance;
                        this.conexion.CerrarConexion();
                        this.conexion = ConectorBD.Instance;
                        consulta = "SELECT vinculo_usuario_proyecto.rol FROM vinculo_usuario_proyecto WHERE vinculo_usuario_proyecto.ref_usuario = '" + IdUsuario + "' AND vinculo_usuario_proyecto.ref_proyecto = " + Proyecto + ";";
                        //Debug.WriteLine(consulta);
                        MySqlDataReader data2 = this.conexion.RealizarConsulta(consulta);
                        //Debug.WriteLine("data"+ data2);
                        if(data2 != null) {
                            while(data2.Read()) {
                                rol = data2.GetString(0).ToString();
                                //Debug.WriteLine("Rol: " + rol);
                                if(rol.Equals(JefeDeProyecto_RolBD)) {
                                    permiso = AUTH_COMO_JEFE_DE_PROYECTO;
                                } else {
                                    permiso = AUTH_COMO_USUARIO;
                                }
                            }  
                        } else {
                            permiso = NO_AUTH;
                        }     
                }
                this.conexion.CerrarConexion();
           } else {
                permiso = NO_AUTH;
           }
            return permiso;
        }


        /**
         * Método para cargar datos
         **/

        public void CargarDatos(DataRow dr) {

        }
    }
}
﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        


        private ApplicationDbContext conexion = ApplicationDbContext.Create();

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
        public Proyecto(int idProyecto, string nombre, string proposito, string alcance, string contexto, string definiciones, string acronimos, string abreviaturas, string referencias, string ambienteOperacional, string relacionProyectos, string estado) {
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
            Estado = estado;
        }

        public Proyecto(int idProyecto, string nombre, string proposito, string alcance, string contexto, string definiciones, string acronimos, string abreviaturas, string ambienteOperacional, string relacionProyectos, string estado) {
            IdProyecto = idProyecto;
            Nombre = nombre;
            Proposito = proposito;
            Alcance = alcance;
            Contexto = contexto;
            Definiciones = definiciones;
            Acronimos = acronimos;
            Abreviaturas = abreviaturas;
            AmbienteOperacional = ambienteOperacional;
            RelacionProyectos = relacionProyectos;
            Estado = estado;
        }




        /**
         * Setter y Getter de ID del proyecto
         * 
         * <param name = "idProyecto" > El identificador del proyecto.</param>
         * <returns>Retorna el valor int del identificador.</returns>
         * 
         **/
        [Display(Name = "Id del Proyecto")]
        [StringLength(128, ErrorMessage = "Este campo debe tener máximo 128 caracteres.", MinimumLength = 1)]
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
        [StringLength(255, ErrorMessage = "El nombre es requerido.", MinimumLength = 1)]
        public string Nombre { get; set;}

        /**
         * Setter y Getter del proposito del proyecto
         * 
         * <param name = "proposito" > El proposito del proyecto.</param>
         * <returns>Retorna el valor string del proposito.</returns>
         * 
         **/
        [Required]
        [Display(Name = "Propósito")]
        [StringLength(255, ErrorMessage = "El propósito es requerido.", MinimumLength = 1)]
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
        [StringLength(255, ErrorMessage = "El alcance es requerido.", MinimumLength = 1)]
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
        [StringLength(255, ErrorMessage = "El contexto es requerido.", MinimumLength = 1)]
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
        [Display(Name = "Acrónimos")]
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
        [StringLength(255, ErrorMessage = "El ambiente operacional es requerido.", MinimumLength = 1)]
        public string AmbienteOperacional {get; set;}


        /**
        * Setter y Getter del atributo que contiene la relacion con otros proyectos
        * 
        * <param name = "relacionProyectos" > La relacion con otros proyectos del proyecto.</param>
        * <returns>Retorna el valor string de la relacion con otros proyectos del proyecto.</returns>
        * 
        **/
        [Display(Name = "Relación con otros proyectos")]
        public string RelacionProyectos {get; set;}

        /**
        * Setter y Getter del atributo que contiene la relacion con otros proyectos
        * 
        * <param name = "estado" > La relacion con otros proyectos del proyecto.</param>
        * <returns>Retorna el valor string de la relacion con otros proyectos del proyecto.</returns>
        * 
        **/
        public string Estado { get; set; }


        /**
         * Setter y Getter de los usuarios relacionados con el proyecto.
         * 
         * <param name = "usuarios" > La lista de usuarios involucrados en el proyecto.</param>
         * <returns>Retorna la lista de usuarios.</returns>
         * 
         **/

        public List<Usuario> Usuarios { get; set; }

        /**
         * Setter y Getter de los requisitos relacionados con el proyecto.
         * 
         * <param name = "requisitos" > La lista de requisitos asociados al proyecto.</param>
         * <returns>Retorna la lista de requisitos.</returns>
         * 
         **/

        public List<Requisito> Requisitos { get; set; }

        /**
         * Setter y Getter de los casos de uso relacionados con el proyecto.
         * 
         * <param name = "casosDeUso" > La lista de casos de uso asociados al proyecto.</param>
         * <returns>Retorna la lista de casos de uso.</returns>
         * 
         **/

        public List<CasoDeUso> CasosDeUso { get; set; }

        /**
         * Setter y Getter de los actores relacionados con el proyecto.
         * 
         * <param name = "actores" > La lista de actores asociados al proyecto.</param>
         * <returns>Retorna la lista de actores.</returns>
         * 
         **/

        public List<Actor> Actores { get; set; }

        /**
         * Método para listar todos los proyectos existentes
         **/

        public void ListarTodos()
        {
        }

        /**
         * Método para listar un proyecto específico
         * <returns>Retorna un proyecto específico.</returns>
         **/

        public void ListarEspecifico(Usuario usuario)
        {

        }

        /**
         * Método para seleccionar un proyecto 
         **/

        public void Seleccionar(int id)
        {

        }

        /**
         * <autor>Diego Iturriaga</autor>
         * <summary>Este metodo se encarga de crear un proyecto con los atributos correspondientes a este
         * siempre y cuando estos cumplan el esquema de validaciones</summary>
         * <param name = "idProyecto" > El identificador del proyecto.</param>
         * <param name = "nombre" > El nombre del proyecto.</param>
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
         * <returns>Retorna un objeto Proyecto que contienen los datos que se ingresan como
         * parametros o retorna el objeto Proyecto como un null en caso de que alguno de los datos
         * no pasen las validaciones correspondientes.</returns>
         **/

        public Proyecto CrearProyecto(int idProyecto, string nombre, string proposito, string alcance, string contexto, string definiciones, string acronimos, string abreviaturas, string referencias, string ambienteOperacional, string relacionProyectos)
        {
            Proyecto proyectoNuevo = null;
            if (this.VerificarNombre(nombre))
            {
                if (this.ValidarNombre(nombre))
                {
                    proyectoNuevo = new Proyecto(idProyecto, nombre, proposito, alcance, contexto, definiciones, acronimos, abreviaturas, referencias, ambienteOperacional, relacionProyectos, "HABILITADO");
                    return proyectoNuevo;
                }
            }
            return proyectoNuevo;
        }

        /**
         * 
         * <autor>Diego Iturriaga</autor>
         * <summary>Este metodo se encarga de validar que el atributo nombre de un proyecto que se desea crear, 
         * el cual no se debe repetir </summary>
         * <param name="nombreProyecto">Contiene un string con el nombre del proyecto que se desea crear.</param>
         * <returns>true si el nombre del proyecto que se desea crear es valido, por lo tanto no se repite,
         * en caso contrario, retorna false</returns>
         * 
         **/
        private bool ValidarNombre(string nombreProyecto)
        {
            string consulta = "SELECT proyecto.nombre FROM proyecto;";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader != null)
            {
                while (reader.Read())
                {
                    string nombre = reader["nombre"].ToString();
                    if (nombre == nombreProyecto)
                    {
                        this.conexion.EnsureConnectionClosed();
                        return false;
                    }
                }
            }
            this.conexion.EnsureConnectionClosed();
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
                //string referencias = data["referencias"].ToString();
                string ambiente_operacional = data["ambiente_operacional"].ToString();
                string relacion_con_otros_proyectos = data["relacion_con_otros_proyectos"].ToString();
                string estado = data["estado"].ToString();


                proyecto = new Proyecto(ID, nombre, proposito, alcance, contexto, definiciones, acronimos, abreviaturas, ambiente_operacional, relacion_con_otros_proyectos, estado);
                //Debug.WriteLine(proyecto.Proposito);
                this.conexion.EnsureConnectionClosed();
            }

            this.conexion.EnsureConnectionClosed();
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
                        this.conexion.EnsureConnectionClosed();
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
                this.conexion.EnsureConnectionClosed();
           } else {
                this.conexion.EnsureConnectionClosed();
                permiso = NO_AUTH;
           }
            return permiso;
        }

        /**
            * Autor: Gerardo Estrada
            * <param name = "id" > Id del proyecto.</param>
            * <returns>La lista de usuarios involucrados en el proyecto</returns>
            **/
        public List<Usuario> GetListaUsuarios(int id) {
            List<Usuario> listaUsuarios = new List<Usuario>();

            string consulta = "SELECT users.Rut, users.UserName, users.Email, vinculo_usuario_proyecto.rol FROM vinculo_usuario_proyecto INNER JOIN users on vinculo_usuario_proyecto.ref_usuario = users.Id WHERE vinculo_usuario_proyecto.ref_proyecto= " + id +";";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader == null) {
                this.conexion.EnsureConnectionClosed();
                return null;
            }
            else {
                while (reader.Read()) {
                    string rut = reader.GetString(0);
                    string nombre = reader.GetString(1);
                    string correo = reader.GetString(2);
                    string tipo = reader.GetString(3);
                    if(tipo.Equals(JefeDeProyecto_RolBD)) {
                        tipo = "Jefe de proyecto";
                    } else {
                        tipo = "Usuario";
                    }

                    listaUsuarios.Add(new Usuario(rut, nombre, correo, tipo));
                }

                this.conexion.EnsureConnectionClosed();
                return listaUsuarios;
            }


            //Proyecto proyecto = null;
            //this.conexion = ConectorBD.Instance;
            //string consulta = "SELECT users.Nombre, users.Rut, users.Email, users.Tipo FROM users, vinculo_usuario_proyecto, proyecto WHERE id_proyecto = " + id + " AND vinculo_usuario_proyecto.ref_proyecto = id_proyecto AND vinculo_usuario_proyecto.ref_usuario = users.Rut ;";
            //MySqlDataReader data = this.conexion.RealizarConsulta(consulta);
            //if (data != null) {
            //    data.Read();
            //    string nombre = data["nombre"].ToString();
            //    string rut = data["rut"].ToString();
            //    string correo_electronico = data["correo_electronico"].ToString();
            //    string tipo = data["tipo"].ToString();
            //    this.conexion.CerrarConexion();

            //}
            //return Usuario;
        }

        public List<SolicitudDeProyecto> GetSolicitudesProyecto(int id)
        {
            string jefeProyecto = this.GetJefeProyecto(id);
            List<SolicitudDeProyecto> listaSolicitudes = new List<SolicitudDeProyecto>();

            string consulta = "SELECT  users.userName, users.Id, proyecto.nombre, proyecto.id_proyecto " +
                "FROM users, proyecto, solicitud_vinculacion_proyecto,Vinculo_usuario_proyecto " +
                "WHERE proyecto.id_proyecto = " + id +
                " AND vinculo_usuario_proyecto.rol = 'JEFEPROYECTO' AND vinculo_usuario_proyecto.ref_proyecto = proyecto.id_proyecto " +
                "AND vinculo_usuario_proyecto.ref_usuario = '" + jefeProyecto + "' AND users.id = solicitud_vinculacion_proyecto.ref_solicitante " +
                 " AND proyecto.id_proyecto = solicitud_vinculacion_proyecto.ref_proyecto";



            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader == null)
            {
                this.conexion.EnsureConnectionClosed();
                return null;
            }
            else
            {
                while (reader.Read())
                {
                    string nombreUsuario = reader.GetString(0);
                    string idUsuario = reader.GetString(1);
                    string nombreProyecto = reader.GetString(2);
                    int idProyecto = Int32.Parse(reader.GetString(3));

                    listaSolicitudes.Add(new SolicitudDeProyecto(nombreUsuario, idUsuario, nombreProyecto, idProyecto));
                }

                this.conexion.EnsureConnectionClosed();
                return listaSolicitudes;
            }

        }


        /**
         * <author>Matías Parra</author>
         * <author>Gerardo Estrada (Modificación 07-08-2018)</author>
         * <summary>
         * Actualiza la base de datos de la tabla proyectos, con los nuevos datos.
         * </summary>
         * <param name = "idProyecto" > El identificador del proyecto.</param>
         * <param name = "valor" > El nombre del JSON, el cual será un string compuesto por HTML y e valor de la base de datos del atributo al que hace referencia.</param>
         * <param name = "atributo" > La etiqueta para reconocer a qué atributo se debe actualizar en la base de datos.</param>
         * <param name = "idUsuario" > La etiqueta para reconocer el usuario que está realizando modificaciones en la base de datos. (Modificación 07-08-2018)</param>
         */
        public void ActualizarDatosProyecto(int idProyecto, string valor, string atributo, string idUsuario)
        {
            string consulta;
            MySqlDataReader reader;
            switch (atributo)
            {
                case "nombre":
                    consulta = "UPDATE proyecto SET nombre='" + valor + "' WHERE id_proyecto=" + idProyecto;
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    consulta = "CALL controlVersiones('" + idUsuario+"',"+idProyecto+",'"+valor+"','"+atributo+"')";
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    break;

                case "proposito":
                    consulta = "UPDATE proyecto SET proposito='" + valor + "' WHERE id_proyecto=" + idProyecto;
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    consulta = "CALL controlVersiones('" + idUsuario + "'," + idProyecto + ",'" + valor + "','" + atributo + "')";
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    break;                    

                case "alcance":
                    consulta = "UPDATE proyecto SET alcance='" + valor + "' WHERE id_proyecto=" + idProyecto;
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    consulta = "CALL controlVersiones('" + idUsuario + "'," + idProyecto + ",'" + valor + "','" + atributo + "')";
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    break;

                case "contexto":
                    consulta = "UPDATE proyecto SET contexto='" + valor + "' WHERE id_proyecto=" + idProyecto;
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    consulta = "CALL controlVersiones('" + idUsuario + "'," + idProyecto + ",'" + valor + "','" + atributo + "')";
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    break;

                case "definicion":
                    consulta = "UPDATE proyecto SET definiciones='" + valor + "' WHERE id_proyecto=" + idProyecto;
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    consulta = "CALL controlVersiones('" + idUsuario + "'," + idProyecto + ",'" + valor + "','" + atributo + "')";
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    break;

                case "acronimo":
                    consulta = "UPDATE proyecto SET acronimos='" + valor + "' WHERE id_proyecto=" + idProyecto;
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    consulta = "CALL controlVersiones('" + idUsuario + "'," + idProyecto + ",'" + valor + "','" + atributo + "')";
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    break;

                case "abreviatura":
                    consulta = "UPDATE proyecto SET abreviaturas='" + valor + "' WHERE id_proyecto=" + idProyecto;
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    consulta = "CALL controlVersiones('" + idUsuario + "'," + idProyecto + ",'" + valor + "','" + atributo + "')";
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    break;

                case "referencia":
                    consulta = "UPDATE proyecto SET referencias='" + valor + "' WHERE id_proyecto=" + idProyecto;
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    consulta = "CALL controlVersiones('" + idUsuario + "'," + idProyecto + ",'" + valor + "','" + atributo + "')";
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    break;

                case "ambiente":
                    consulta = "UPDATE proyecto SET ambiente_operacional='" + valor + "' WHERE id_proyecto=" + idProyecto;
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    consulta = "CALL controlVersiones('" + idUsuario + "'," + idProyecto + ",'" + valor + "','" + atributo + "')";
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    break;

                case "relacion":
                    consulta = "UPDATE proyecto SET relacion_con_otros_proyectos='" + valor + "' WHERE id_proyecto=" + idProyecto;
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    consulta = "CALL controlVersiones('" + idUsuario + "'," + idProyecto + ",'" + valor + "','" + atributo + "')";
                    reader = this.conexion.RealizarConsulta(consulta);
                    this.conexion.EnsureConnectionClosed();
                    break;
            }
            this.conexion.EnsureConnectionClosed();
        }


        /**
         * Método para cargar datos
         **/

        public void CargarDatos(DataRow dr)
        {

        }

        public string GetJefeProyecto(int id)
        {
            string consulta = "SELECT vinculo_usuario_proyecto.ref_usuario FROM vinculo_usuario_proyecto" +
                " WHERE vinculo_usuario_proyecto.rol = 'JEFEPROYECTO' AND vinculo_usuario_proyecto.ref_proyecto = " + id;
            string jefe ="";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader!= null)
            {
                reader.Read();
                jefe = reader["ref_usuario"].ToString();
            }
            this.conexion.EnsureConnectionClosed();
            return jefe;
        }

        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Indica si el largo de una cadena de texto es mayor a 0.
         * </summary>
         * <param name="texto">Contiene un string con el texto a verificar</param>
         * <returns>true si texto es mayor que 0, false en caso contrario</returns>
         */
        private bool VerificarNombre(string texto)
        {
            return texto.Length > 0;
        }
        /**
         *<autor>Ariel Cornejo</autor>
         * <summary>
         * Metodo encargado de insertar un proyecto creado en la Base De datos
         * </summary> 
         * 
         * <param name="proyecto"> Objeto proyecto que sera ingresado en la BD</param>
         * <returns>Retorna true si el registro del proyecto en la base de datos se realiza
         * exitosamente, retorna false en caso contrario</returns>
         * 
         * */
        public bool RegistrarProyectoEnBd(Proyecto proyecto)
        {
            String nombre = proyecto.Nombre;
            String proposito = proyecto.Proposito;
            String alcance = proyecto.Alcance;
            String contexto = proyecto.Contexto;
            String definiciones = proyecto.Definiciones;
            String acronimos = proyecto.Acronimos;
            String abreviaturas = proyecto.Abreviaturas;
            String referencias = proyecto.Referencias;
            String ambiente = proyecto.AmbienteOperacional;
            String relacion = proyecto.RelacionProyectos;
            String estado = proyecto.Estado;
            String consulta = "INSERT INTO proyecto (nombre,proposito,alcance,contexto,definiciones,acronimos,abreviaturas,referencias,ambiente_operacional,relacion_con_otros_proyectos,estado)" +
                " VALUES ('" + nombre + "', '" + proposito + "','" + alcance + "','" + contexto + "','" + definiciones + "','" + acronimos + "','" + abreviaturas + "','" + referencias + "','" + ambiente + "','" + relacion + "','" + estado + "')";
            return this.conexion.RealizarConsultaNoQuery(consulta);
        }
        //Metodos para Asignar Jefes de Proyectos
        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Obtiene los nombres de proyecto desde la base de datos.
         * </summary>
         * <returns>Devuelve una lista de SelectListItem con el nombre de los proyectos.</returns>
         */
        public List<SelectListItem> ObtenerProyectos()
        {
            List<SelectListItem> listaProyectos = new List<SelectListItem>();
            string consulta = "SELECT proyecto.nombre FROM proyecto WHERE id_proyecto IN (SELECT vinculo_usuario_proyecto.ref_proyecto FROM vinculo_usuario_proyecto WHERE rol='JEFEPROYECTO');";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader != null)
            {
                int i = 0;
                while (reader.Read())
                {
                    string nombre = reader["nombre"].ToString();
                    i++;
                    listaProyectos.Add(new SelectListItem() { Text = nombre, Value = nombre });
                }
            }
            this.conexion.EnsureConnectionClosed();
            return listaProyectos;
        }

        /**
        * <author>Roberto Ureta-Diego Iturriaga</author>
        * <summary>
        * Obtiene los nombres de proyecto que no tienen jefe de proyecto asignado desde la base de datos.
        * </summary>
        * <returns>Devuelve una lista de SelectListItem con el nombre de los proyectos.</returns>
        */
        public List<SelectListItem> ObtenerProyectosSinJefe()
        {
            List<SelectListItem> listaProyectos = new List<SelectListItem>();
            string consulta = "SELECT proyecto.nombre FROM proyecto WHERE id_proyecto NOT IN (SELECT vinculo_usuario_proyecto.ref_proyecto FROM vinculo_usuario_proyecto WHERE rol='JEFEPROYECTO');";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader != null)
            {
                int i = 0;
                while (reader.Read())
                {
                    string nombre = reader["nombre"].ToString();
                    i++;
                    listaProyectos.Add(new SelectListItem() { Text = nombre, Value = nombre });
                }
            }
            this.conexion.EnsureConnectionClosed();
            return listaProyectos;
        }
        /**
         * 
         * <autor>Diego Iturriaga</autor>
         * <summary>Este metodo se encarga de Obtener una lista de usuarios validos desde la base de datos
         * que puedan ser asignados a un proyecto con el rol JEFEPROYECTO</summary>
         * <returns>Retorna una List de tipo SelectListItem que contiene todos los usuarios validos
         * en la base de datos que puedan ser asignados a un proyecto como JEFEPROYECTO</returns>
         * 
         **/
        public List<SelectListItem> ObtenerUsuarios()
        {
            List<SelectListItem> listasUsuarios = new List<SelectListItem>();
            string consulta = "SELECT users.UserName, users.Rut FROM users WHERE Tipo != 'SYSADMIN' AND Estado = 1 AND Disponibilidad_Vinculacion = 1; ";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader != null)
            {
                int i = 0;
                while (reader.Read())
                {
                    string rutBD = reader["Rut"].ToString();
                    string nombreBD = reader["UserName"].ToString();
                    string texto = nombreBD + " / " + rutBD;
                    i++;
                    listasUsuarios.Add(new SelectListItem() { Text = texto, Value = texto });
                }
            }

            this.conexion.EnsureConnectionClosed();
            return listasUsuarios;
        }

        /**
         * 
         * <autor>Roberto Ureta-Diego Iturriaga</autor>
         * <summary>Obtiene una lista de Usuarios disponibles para ser JEFE DE PROYECTO en un proyecto      *determinado </summary>
         * <returns>Retorna una List de tipo Usuario que contiene todos los usuarios que pueden  ser asignados a un * proyecto como JEFEPROYECTO</returns>
         * <param name="idProyecto">Contiene un entero que tiene el id de un proyecto.</param>
         **/
        public List<Usuario> ObtenerUsuarios2(int idProyecto)
        {
            List<Usuario> listasUsuarios = new List<Usuario>();
            string consulta = "SELECT users.UserName, users.Rut, users.Email FROM users WHERE Id NOT IN(SELECT vinculo_usuario_proyecto.ref_usuario FROM vinculo_usuario_proyecto WHERE ref_proyecto = "+idProyecto+" AND rol = 'JEFEPROYECTO') AND Tipo != 'SYSADMIN' AND Estado = 1; ";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader != null)
            {
                while (reader.Read())
                {
                    string rutBD = reader["Rut"].ToString();
                    string nombreBD = reader["UserName"].ToString();
                    string correoBD = reader["Email"].ToString();
                    listasUsuarios.Add(new Usuario() { Rut = rutBD, Nombre = nombreBD, CorreoElectronico = correoBD,
                    Contrasenia = String.Empty,Estado= true, Tipo=String.Empty });
                }
            }

            this.conexion.EnsureConnectionClosed();
            return listasUsuarios;
        }

        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Asigna un jefe de proyecto segun el rut del usuario y el nombre del proyecto.
         * </summary>
         * <param name="nombreUsuario">Contiene un string que tiene el nombre de usuario y su rut separados por un / .</param>
         * <param name="nombreProyecto">Contiene un string que tiene el nombre del Proyecto</param>
         * <returns>true si inserto, false en caso contrario</returns>
         */
        public bool AsignarJefeProyecto(string nombreUsuario, string nombreProyecto)
        {
            string consultaNombreProyecto = "SELECT id_proyecto FROM proyecto WHERE nombre ='" + nombreProyecto + "';";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consultaNombreProyecto);
            int idProyecto = -1;
            if (reader != null)
            {
                reader.Read();
                idProyecto = Int32.Parse(reader["id_proyecto"].ToString());
            }
            this.conexion.EnsureConnectionClosed();
            String rut = ObtenerRutDesdeString(nombreUsuario);
            String consultaRutId = "SELECT users.id FROM users WHERE rut='"+rut+"' GROUP BY id;";
            reader = this.conexion.RealizarConsulta(consultaRutId);
            reader.Read();
            String id = reader["id"].ToString();
            this.conexion.EnsureConnectionClosed();
            if (IdProyecto != -1)
            {
                String consultaInsertar = "INSERT INTO vinculo_usuario_proyecto(ref_usuario,ref_proyecto,rol)" +
                    "VALUES ('" + id + "','" + idProyecto + "','JEFEPROYECTO');" +
                    "DELETE FROM vinculo_usuario_proyecto WHERE ref_usuario = '" + id + "' AND ref_proyecto = '" + idProyecto + "' AND rol = 'USUARIO'; ";
                if (this.conexion.RealizarConsultaNoQuery(consultaInsertar))
                {
                    this.conexion.EnsureConnectionClosed();
                    return true;
                }
                else
                {
                    this.conexion.EnsureConnectionClosed();
                    return false;
                }
            }
            return false;
        }

        public bool EliminarSolicitudesUnionProyectosInnecesarias(int idProyecto)
        {
            string consulta = "SELECT vinculo_usuario_proyecto.ref_usuario FROM vinculo_usuario_proyecto WHERE vinculo_usuario_proyecto.ref_proyecto ="+idProyecto+";";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader != null)
            {
                while (reader.Read())
                {
                    string idUsuarioBD = reader["ref_usuario"].ToString();
                    EliminarSolicitudUnionProyecto(idUsuarioBD,idProyecto);
                }
            }
            this.conexion.EnsureConnectionClosed();
            return true;
        }

        public bool EliminarSolicitudUnionProyecto(string idUsuario, int idProyecto)
        {
            ApplicationDbContext conexionLocal = ApplicationDbContext.Create();
            string consulta = "DELETE FROM solicitud_vinculacion_proyecto WHERE ref_usuario = '"+idUsuario+"' AND ref_proyecto = "+idProyecto+";";
            bool valor = conexionLocal.RealizarConsultaNoQuery(consulta);
            conexionLocal.EnsureConnectionClosed();
            return valor;
        }

        /**
         * 
         * <autor>Diego Iturriaga</autor>
         * <summary>Este metodo se encarga de modificar el jefe de proyecto de un proyecto en especifico
         * dado un usuario y un proyecto en especifico para que a este ultimo se le modifique un nuevo 
         * jefe de proyecto.</summary>
         * <param name="nombreUsuario">nombre del usuario que pasara a ser el nuevo jefe de proyecto (contiene el rut de este concadenado)</param>
         * <param name="nombreProyecto"> nombre del proyecto al que se le desea modificar el jefe de proyecto</param>
         * <returns>Retorna true si se modifica el Jefe de Proyecto existosamente en la tabla
         * vinculo_usuario_proyecto, Retorna false si falla la modificacion</returns>
         * 
         **/
        public bool ModificarJefeProyecto(string rut, int idProyecto)
        {
            String consultaRutId = "SELECT users.id FROM users WHERE rut='" + rut + "' GROUP BY id;";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consultaRutId);
            reader.Read();
            String id = reader["id"].ToString();
            this.conexion.EnsureConnectionClosed();
            if (!VerificarRolEnProyecto(id,idProyecto)) {
                if (idProyecto != -1)
                {
                    String consulta1 = "UPDATE vinculo_usuario_proyecto SET rol = 'USUARIO' WHERE ref_proyecto = " + idProyecto + " AND rol = 'JEFEPROYECTO';";
                    if (this.conexion.RealizarConsultaNoQuery(consulta1)) {
                        this.conexion.EnsureConnectionClosed();
                        String consulta2 = "INSERT INTO vinculo_usuario_proyecto(ref_usuario,ref_proyecto,rol) VALUES ('"+id+"',"+idProyecto+",'JEFEPROYECTO');" +
                            "DELETE FROM vinculo_usuario_proyecto WHERE ref_usuario = '" + id + "' AND ref_proyecto = " + idProyecto + " AND rol = 'USUARIO'; ";
                        if (this.conexion.RealizarConsultaNoQuery(consulta2))
                        {
                            this.EliminarSolicitudesPendientes(idProyecto,id);
                            this.conexion.EnsureConnectionClosed();
                            return true;
                        }
                        else
                        {
                            this.conexion.EnsureConnectionClosed();
                            return false;
                        }
                    }
                    this.conexion.EnsureConnectionClosed();
                    return false;
                }
            }           
            return false;

        }

        /**
        * <author>Roberto Ureta-Ariel Cornejo-Diego Iturriaga</author>
        * <summary>
        * Elimina solicitudes de un usuario determinado en un proyecto determinado.
        * </summary>     
        * <param name="idProyecto">Contiene un int con el id de un proyecto.</param>
        * <param name="idUsuario">Contiene un string que tiene el id de un usuario.</param>  
        * <returns> true si se ejecuto la consulta, false en caso contrario.</returns>
        */
        public bool EliminarSolicitudesPendientes(int idProyecto, string idUsuario) {
            String consulta = "DELETE FROM solicitud_jefeproyecto_usuario WHERE ref_proyecto = '"+idProyecto+"' AND ref_destinario='"+idUsuario+"';"+
                               "DELETE FROM solicitud_vinculacion_proyecto WHERE ref_proyecto = '"+idProyecto+"' AND ref_solicitante = '"+idUsuario+"';";
            return this.conexion.RealizarConsultaNoQuery(consulta);
        }

        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Verifica si un usuario ya es jefe de proyecto de un proyecto.
         * </summary>
         * <param name="rut">Contiene un string que tiene el rut de un usuario.</param>
         * <param name="idProyecto">Contiene un int con el id de un proyecto.</param>
         * <returns> true si el rol es jefe de proyecto, false en caso contrario.</returns>
         */
        public bool VerificarRolEnProyecto(string rut, int idProyecto) {
            string consulta = "SELECT vinculo_usuario_proyecto.rol FROM vinculo_usuario_proyecto WHERE vinculo_usuario_proyecto.ref_usuario = '" + rut + "' AND vinculo_usuario_proyecto.ref_proyecto=" + idProyecto + ";";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader != null)
            {
                reader.Read();
                string rol = reader["rol"].ToString();
                this.conexion.EnsureConnectionClosed();
                if (rol.Equals("JEFEPROYECTO"))
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            this.conexion.EnsureConnectionClosed();
            return false;

        }
        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Recupera el rut desde un string con formato nombreUsuario / rutUsuario.
         * </summary>
         * <param name="texto">Contiene un string que tiene el nombre de usuario y su rut separados por un / .</param>
         * <returns> el string con el rut de un usuario</returns>
         */
        public String ObtenerRutDesdeString(String texto)
        {
            String[] subStrings = texto.Split('/');
            String rut = subStrings[1].Trim(' ');
            return rut;
        }

        public List<Actor> GetListaActores(int id)
        {
            List<Actor> listaActores = new List<Actor>();

            string consulta = "SELECT * FROM actor WHERE actor.ref_proyecto= " + id + ";";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader == null)
            {
                this.conexion.EnsureConnectionClosed();
                return null;
            }
            else
            {
                while (reader.Read())
                {
                    int idActor = reader.GetInt32(0);
                    string nombre = reader.GetString(1);
                    string descripcion = reader.GetString(2);
                    int numActual = reader.GetInt32(3);
                    int numFuturo = reader.GetInt32(4);
                    
                    listaActores.Add(new Actor(idActor,descripcion,numActual,numFuturo,nombre));
                }

                this.conexion.EnsureConnectionClosed();
                return listaActores;
            }


            //Proyecto proyecto = null;
            //this.conexion = ConectorBD.Instance;
            //string consulta = "SELECT users.Nombre, users.Rut, users.Email, users.Tipo FROM users, vinculo_usuario_proyecto, proyecto WHERE id_proyecto = " + id + " AND vinculo_usuario_proyecto.ref_proyecto = id_proyecto AND vinculo_usuario_proyecto.ref_usuario = users.Rut ;";
            //MySqlDataReader data = this.conexion.RealizarConsulta(consulta);
            //if (data != null) {
            //    data.Read();
            //    string nombre = data["nombre"].ToString();
            //    string rut = data["rut"].ToString();
            //    string correo_electronico = data["correo_electronico"].ToString();
            //    string tipo = data["tipo"].ToString();
            //    this.conexion.CerrarConexion();

            //}
            //return Usuario;
        }
    }
}
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.ComponentModel.DataAnnotations;
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
        #endregion

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


        public Proyecto() {

        }

        private ConectorBD conexion = ConectorBD.Instance;

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

        public Proyecto CrearProyecto(int idProyecto, string nombre, string proposito, string alcance, string contexto, string definiciones, string acronimos, string abreviaturas, string referencias, string ambienteOperacional, string relacionProyectos) {
            Proyecto proyectoNuevo = null;
            if (this.VerificarNombre(nombre))
            {
                if (this.ValidarNombre(nombre))
                {
                    proyectoNuevo = new Proyecto(idProyecto, nombre, proposito, alcance, contexto, definiciones, acronimos, abreviaturas, referencias, ambienteOperacional, relacionProyectos);
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
            if (reader!=null)
            {
                while (reader.Read())
                {
                    string nombre = reader["nombre"].ToString();
                    if (nombre==nombreProyecto)
                    {
                        this.conexion.CerrarConexion();
                        return false;
                    }
                }
            }
            this.conexion.CerrarConexion();
            return true;
        }

        /**
         * Método para cargar datos
         **/

        public void CargarDatos(DataRow dr) {

        }

        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Indica si el largo de una cadena de texto es mayor a 0.
         * </summary>
         * <param name="texto">Contiene un string con el texto a verificar</param>
         * <returns>true si texto es mayor que 0, false en caso contrario</returns>
         */
        private bool VerificarNombre(string texto) {
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
            ConectorBD conector = ConectorBD.Instance;
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
            String consulta = "INSERT INTO proyecto (nombre,proposito,alcance,contexto,definiciones,acronimos,abreviaturas,referencias,ambiente_operacional,relacion_con_otros_proyectos)" +
                " VALUES ('"+nombre+"', '"+proposito+"','"+alcance+"','"+contexto+"','"+definiciones+"','"+acronimos+"','"+abreviaturas+"','"+referencias+"','"+ambiente+"','"+relacion+"')";
            return conector.RealizarConsultaNoQuery(consulta);
        }
        //Metodos para Asignar Jefes de Proyectos
        /**
         * <author>Roberto Ureta</author>
         * <summary>
         * Obtiene los nombres de proyecto desde la base de datos.
         * </summary>
         * <returns>Devuelve una lista de SelectListItem con el nombre de los proyectos.</returns>
         */
        public List<SelectListItem> ObtenerProyectos() {
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
            this.conexion.CerrarConexion();
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
            this.conexion.CerrarConexion();
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
            string consulta = "SELECT usuario.nombre, usuario.rut FROM usuario WHERE rut NOT IN (SELECT vinculo_usuario_proyecto.ref_usuario FROM vinculo_usuario_proyecto WHERE rol = 'JEFEPROYECTO') AND tipo != 'SYSADMIN' AND estado = 1; ";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta);
            if (reader != null)
            {
                int i = 0;
                while (reader.Read())
                {
                    string rutBD = reader["rut"].ToString();
                    string nombreBD =reader["nombre"].ToString();
                    string texto = nombreBD + " / " + rutBD;
                    i++;
                    listasUsuarios.Add(new SelectListItem() { Text = texto, Value = texto});
                }
            }

            this.conexion.CerrarConexion();
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
        public bool AsignarJefeProyecto(string nombreUsuario, string nombreProyecto) {
            string consultaNombreProyecto = "SELECT id_proyecto FROM proyecto WHERE nombre ='"+nombreProyecto+"';";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consultaNombreProyecto);
            int idProyecto = -1;
            if (reader != null)
            {
                reader.Read();
                idProyecto = Int32.Parse(reader["id_proyecto"].ToString());
            }
            this.conexion.CerrarConexion();
            String rut = ObtenerRutDesdeString(nombreUsuario);
            if (IdProyecto != -1) {
                String consultaInsertar = "INSERT INTO vinculo_usuario_proyecto(ref_usuario,ref_proyecto,rol)"+
                    "VALUES ('"+rut+"','"+idProyecto+"','JEFEPROYECTO');"+
                    "DELETE FROM vinculo_usuario_proyecto WHERE ref_usuario = '" + rut + "' AND ref_proyecto = '" + idProyecto + "' AND rol = 'USUARIO'; ";
                if (this.conexion.RealizarConsultaNoQuery(consultaInsertar))
                {
                    this.conexion.CerrarConexion();
                    return true;
                }
                else
                {
                    this.conexion.CerrarConexion();
                    return false;
                }
            }
            return false;
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
        public bool ModificarJefeProyecto(string nombreUsuario, string nombreProyecto)
        {
            String consulta1 = "SELECT id_proyecto FROM proyecto WHERE nombre = '"+nombreProyecto+"';";
            MySqlDataReader reader = this.conexion.RealizarConsulta(consulta1);
            int idProyecto = -1;
            if (reader != null)
            {
                reader.Read();
                idProyecto = Int32.Parse(reader["id_proyecto"].ToString());
            }
            this.conexion.CerrarConexion();
            String rut = this.ObtenerRutDesdeString(nombreUsuario);
            if (idProyecto != -1)
            {
                String consulta2 = "UPDATE vinculo_usuario_proyecto SET ref_usuario = '"+rut+"' WHERE ref_proyecto = '"+idProyecto+"' AND rol = 'JEFEPROYECTO'; " +
                    "DELETE FROM vinculo_usuario_proyecto WHERE ref_usuario = '" + rut + "' AND ref_proyecto = '" + idProyecto + "' AND rol = 'USUARIO'; ";
                if (this.conexion.RealizarConsultaNoQuery(consulta2))
                {
                    this.conexion.CerrarConexion();
                    return true;
                }
                else
                {
                    this.conexion.CerrarConexion();
                    return false;
                }
            }
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
        public String ObtenerRutDesdeString(String texto) {
            String[] subStrings = texto.Split('/');
            String rut = subStrings[1].Trim(' ');
            return rut;
        }
    }
}
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/**
 * Autor: Gerardo Estrada (Meister1412)
 **/

namespace AppWebERS.Models
{
    public class Proyecto {

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

        public Proyecto(int idProyecto, string nombre,string proposito, string alcance, string contexto, string definiciones, string acronimos, string abreviaturas, string referencias, string ambienteOperacional, string relacionProyectos) {
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

        private ConectorBD conexion = ConectorBD.Instance;

        /**
         * Setter y Getter de ID del proyecto
         * 
         * <param name = "idProyecto" > El identificador del proyecto.</param>
         * <returns>Retorna el valor int del identificador.</returns>
         * 
         **/

        public int IdProyecto {get; set;}

        /**
         * Setter y Getter de Nombre del proyecto
         * 
         * <param name = "nombre" > El identificador del proyecto.</param>
         * <returns>Retorna el valor string del nombre.</returns>
         * 
         **/
        public string Nombre { get; set;}

        /**
         * Setter y Getter del proposito del proyecto
         * 
         * <param name = "proposito" > El proposito del proyecto.</param>
         * <returns>Retorna el valor string del proposito.</returns>
         * 
         **/

        public string Proposito {get; set;}

        /**
         * Setter y Getter del alcance del proyecto
         * 
         * <param name = "alcance" > El alcance del proyecto.</param>
         * <returns>Retorna el valor string del alcance.</returns>
         * 
         **/

        public string Alcance {get; set;}

        /**
         * Setter y Getter del contexto del proyecto
         * 
         * <param name = "contexto" > El contexto del proyecto.</param>
         * <returns>Retorna el valor string del contexto.</returns>
         * 
         **/

        public string Contexto {get; set;}

        /**
         * Setter y Getter del atributo que contiene las definiciones del proyecto
         * 
         * <param name = "definiciones" > Las definiciones del proyecto.</param>
         * <returns>Retorna el valor string de las definiciones.</returns>
         * 
         **/

        public string Definiciones {get; set;}

        /**
         * Setter y Getter del atributo que contiene los acronimos del proyecto
         * 
         * <param name = "acronimos" > Los acronimos del proyecto.</param>
         * <returns>Retorna el valor string de los acronimos.</returns>
         * 
         **/

        public string Acronimos {get; set;}

        /**
        * Setter y Getter del atributo que contiene las abreviaturas del proyecto
        * 
        * <param name = "abreviaturas" > Las abreviaturas del proyecto.</param>
        * <returns>Retorna el valor string de las abreviaturas.</returns>
        * 
        **/

        public string Abreviaturas {get; set;}

        /**
        * Setter y Getter del atributo que contiene las referencias del proyecto
        * 
        * <param name = "referencias" > Las referencias del proyecto.</param>
        * <returns>Retorna el valor string de las referencias.</returns>
        * 
        **/

        public string Referencias {get; set;}

        /**
        * Setter y Getter del ambiente operacional del proyecto
        * 
        * <param name = "ambienteOperacional" > El ambiente operacional del proyecto.</param>
        * <returns>Retorna el valor string del ambiente operacional.</returns>
        * 
        **/

        public string AmbienteOperacional {get; set;}

        /**
        * Setter y Getter del atributo que contiene la relacion con otros proyectos
        * 
        * <param name = "relacionProyectos" > La relacion con otros proyectos del proyecto.</param>
        * <returns>Retorna el valor string de la relacion con otros proyectos del proyecto.</returns>
        * 
        **/

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
            if (this.VerificarTexto(nombre))
            {
                if (this.ValidarNombre(nombre))
                {
                    proyectoNuevo = new Proyecto(idProyecto, nombre, proposito, alcance, contexto, definiciones, acronimos, abreviaturas, referencias, ambienteOperacional, relacionProyectos);
                    return proyectoNuevo;
                }
            }
            return proyectoNuevo;
        }

        private bool VerificarTexto(string nombre)
        {
            throw new NotImplementedException();
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
         * 
         * */
        public bool RegistrarProyectoEnBd(Proyecto proyecto)
        {
            ConectorBD conector = ConectorBD.Instance;
            int id = proyecto.IdProyecto;
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
            String consulta = "INSERT INTO proyecto (id_proyecto,nombre,proposito,alcance,contexto,definiciones,acronimos,abreviaturas,referencias,ambiente_operacional,relacion_con_otros_proyectos)" +
                " VALUES ('"+id+"','"+nombre+"', '"+proposito+"','"+alcance+"','"+contexto+"','"+definiciones+"','"+acronimos+"','"+abreviaturas+"','"+referencias+"','"+ambiente+"','"+relacion+"')";
            return conector.RealizarConsultaNoQuery(consulta);
        }
    }
}
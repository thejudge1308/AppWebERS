using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/**
 * Autor: Gerardo Estrada (Meister1412)
 **/

namespace AppWebERS.Models
{
    public class Proyecto {
        private int idProyecto;
        private string proposito;
        private string alcance;
        private string contexto;
        private string definiciones;
        private string acronimos;
        private string abreviaturas;
        private string referencias;
        private string ambienteOperacional;
        private string relacionProyectos;
        private List<Usuario> usuarios;
        private List<Requisito> requisitos;
        private List<CasosDeUso> casosDeUso;
        private List<Actor> actores;

        /**
         * Constructor de la clase Proyecto
         * 
         * <param name = "idProyecto" > El identificador del proyecto.</param>
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

        public Proyecto(int idProyecto, string proposito, string alcance, string contexto, string definiciones, string acronimos, string abreviaturas, string referencias, string ambienteOperacional, string relacionProyectos, List<Usuario> usuarios, List<Requisito> requisitos, List<CasosDeUso> casosDeUso, List<Actor> actores) {
            this.idProyecto = idProyecto;
            this.proposito = proposito;
            this.alcance = alcance;
            this.contexto = contexto;
            this.definiciones = definiciones;
            this.acronimos = acronimos;
            this.abreviaturas = abreviaturas;
            this.referencias = referencias;
            this.ambienteOperacional = ambienteOperacional;
            this.relacionProyectos = relacionProyectos;
            this.usuarios = new List<Usuario>();
            this.requisitos = new List<Requisito>();
            this.casosDeUso = new List<CasosDeUso>();
            this.actores = new List<Actor>();
        }

        /**
         * Setter y Getter de ID del proyecto
         * 
         * <param name = "idProyecto" > El identificador del proyecto.</param>
         * <returns>Retorna el valor int del identificador.</returns>
         * 
         **/

        public int IdProyecto {get; set;}

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

        public List<CasosDeUso> CasosDeUso {get; set;}

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

        public void listarTodos() {
        }

        /**
         * Método para listar un proyecto específico
         * <returns>Retorna un proyecto específico.</returns>
         **/

        public void listarEspecifico(Usuario usuario) {
            int aux = 0;
            usuariosEspecificos = new List<Usuario>();
            numUsuarios = proyecto.usuarios.Count;
            while (aux < numUsuarios) {
                if (proyecto.usuarios.id.equals(usuario.id)) {
                    usuariosEspecificos.add(proyecto.usuarios(aux));
                }
            }
            return usuariosEspecificos;
        }

        /**
         * Método para seleccionar un proyecto 
         **/

        public void seleccionar(int id) {

        }

        /**
         * Método para Crear un Proyecto
         * <returns>Retorna un boolean que indica la correcta creación del proyecto.</returns>
         **/

        public bool crear() {
            return true;
        }

        /**
         * Método para cargar datos
         **/

        public void cargarDatos(dr DataRow) {

        }
    }
}
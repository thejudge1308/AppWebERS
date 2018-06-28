using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/**
 * Autor: Gerardo Estrada (Meister1412)
 **/

namespace AppWebERS.Models {
    public class Requisito {

        /**
         * Constructor de la clase Requisito
         * 
         * <param name = "idRequisito" > El identificador del requisito.</param>
         * <param name = "nombre" > El nombre del requisito.</param>
         * <param name = "descripcion" > La descripcion del requisito.</param>
         * <param name = "prioridad" > La prioridad asignada al requisito.</param>
         * <param name = "categoria" > La categoria del requisito.</param>
         * <param name = "fuente" > La fuente del requisito.</param>
         * <param name = "estabilidad" > La estabilidad requerida para el requisito.</param>
         * <param name = "estado" > El estado del requisito.</param>
         * <param name = "tipo" > El tipo  del requisito.</param>
         * <param name = "actores" > Lista de actores que tienen algún tipo de participación en el requisito.</param>
         **/

        public Requisito(int idRequisito, string nombre, string descripcion, string prioridad, string categoria, string fuente, string estabilidad, string estado, string tipo, List<Actor> actores) {
            this.IdRequisito = idRequisito;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.Prioridad = prioridad;
            this.Categoria = categoria;
            this.Fuente = fuente;
            this.Estabilidad = estabilidad;
            this.Estado = estado;
            this.Tipo = tipo;
            this.Actores = new List<Actor>();
        }

        /**
         * Setter y Getter de ID del requisito
         * 
         * <param name = "idRequisito" > El identificador del requisito.</param>
         * <returns>Retorna el valor int del idRequisito.</returns>
         * 
         **/

        public int IdRequisito {get; set;}

        /**
         * Setter y Getter del nombre del requisito.
         * 
         * <param name = "nombre" > El nombre del requisito.</param>
         * <returns>Retorna el valor string del nombre.</returns>
         * 
         **/

        public string Nombre {get; set;}

        /**
         * Setter y Getter de la descripcion del requisito.
         * 
         * <param name = "descripcion" > La descripcion del requisito.</param>
         * <returns>Retorna el valor string de la descripcion.</returns>
         * 
         **/

        public string Descripcion {get; set;}

        /**
         * Setter y Getter de la prioridad del requisito.
         * 
         * <param name = "prioridad" > La prioridad del requisito.</param>
         * <returns>Retorna el valor string de la prioridad.</returns>
         * 
         **/

        public string Prioridad {get; set;}

        /**
         * Setter y Getter de la categoria del requisito.
         * 
         * <param name = "categoria" > La categoria del requisito.</param>
         * <returns>Retorna el valor string de la categoria.</returns>
         * 
         **/

        public string Categoria {get; set;}

        /**
         * Setter y Getter de la fuente del requisito.
         * 
         * <param name = "fuente" > La fuente del requisito.</param>
         * <returns>Retorna el valor string de la fuente.</returns>
         * 
         **/

        public string Fuente {get; set;}

        /**
         * Setter y Getter de la estabilidad del requisito.
         * 
         * <param name = "estabilidad" > La estabilidad del requisito.</param>
         * <returns>Retorna el valor string de la estabilidad.</returns>
         * 
         **/

        public string Estabilidad {get; set;}

        /**
         * Setter y Getter del estado del requisito.
         * 
         * <param name = "estado" > El estado del requisito.</param>
         * <returns>Retorna el valor string del estado.</returns>
         * 
         **/

        public string Estado {get; set;}

        /**
         * Setter y Getter del tipo del requisito.
         * 
         * <param name = "tipo" > El tipo del requisito.</param>
         * <returns>Retorna el valor string del tipo.</returns>
         * 
         **/

        public string Tipo {get; set;}

        /**
         * Setter y Getter de los actores del requisito.
         * 
         * <param name = "actores" > La lista de actores involucrados en el requisito.</param>
         * <returns>Retorna la lista de actores.</returns>
         * 
         **/

        public List<Actor> Actores {get; set;}

        /**
         * Método para Crear un Requisito
         * <returns>Retorna un boolean que indica el correcto registro del requisito.</returns>
         **/

        public bool Crear() {
            return true;
        }

        /**
         * Método para listar un registro específico
         * <returns>Retorna un registro específico.</returns>
         **/

        public void ListarEspecifico(Proyecto proyecto) {
            
        }

        /**
         * Método para seleccionar un requisito 
         **/

        public void Seleccionar(int id) {

        }

        /**
         * Método para cargar datos
         **/

        public void CargarDatos(DataRow dr ) {

        }
    }
}
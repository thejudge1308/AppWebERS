using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
         * <param name = "fuente" > La fuente del requisito.</param>
         * <param name = "estabilidad" > La estabilidad requerida para el requisito.</param>
         * <param name = "estado" > El estado del requisito.</param>
         * <param name = "tipo" > El tipo  del requisito.</param>
         * <param name = "actores" > Lista de actores que tienen algún tipo de participación en el requisito.</param>
         * FALTAN UNOS
         **/

        public Requisito(int idRequisito, string nombre, string tipoUsuario,string medida, string fecha, string incremento,
            string descripcion, string prioridad, string fuente, string estabilidad, string estado, string tipoReq, List<Actor> actores) {
            this.IdRequisito = idRequisito;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.Fuente = fuente;
            this.TipoUsuario = tipoUsuario;
            this.TipoRequisito = tipoReq;
            this.Estado = estado;
            this.Prioridad = prioridad;
            this.Estabilidad = estabilidad;
            this.Medida = medida;
            this.Fecha = fecha;
            this.Incremento = incremento;

            //LOS ACTORES NO SE SI SON LAS FUENTES?
            this.Actores = new List<Actor>();
        }

        /**
         * Setter y Getter de ID del requisito
         * 
         * <param name = "idRequisito" > El identificador del requisito.</param>
         * <returns>Retorna el valor int del idRequisito.</returns>
         * 
         **/

        [Display(Name = "Código")]
        public int IdRequisito {get; set;}

        /**
         * Setter y Getter del nombre del requisito.
         * 
         * <param name = "nombre" > El nombre del requisito.</param>
         * <returns>Retorna el valor string del nombre.</returns>
         * 
         **/
        [Display(Name = "Nombre")]
        public string Nombre {get; set;}

        /**
         * Setter y Getter de la descripcion del requisito.
         * 
         * <param name = "descripcion" > La descripcion del requisito.</param>
         * <returns>Retorna el valor string de la descripcion.</returns>
         * 
         **/
        [Display(Name = "Descripción")]
        public string Descripcion {get; set;}

        /**
         * Setter y Getter de la prioridad del requisito.
         * 
         * <param name = "prioridad" > La prioridad del requisito.</param>
         * <returns>Retorna el valor string de la prioridad.</returns>
         * 
         **/
        [Display(Name = "Prioridad")]
        public string Prioridad {get; set;}

        /**
         * Setter y Getter de la fuente del requisito.
         * 
         * <param name = "fuente" > La fuente del requisito.</param>
         * <returns>Retorna el valor string de la fuente.</returns>
         * 
         **/
        [Display(Name = "Fuente")]
        public string Fuente {get; set;}

        /**
         * Setter y Getter de la estabilidad del requisito.
         * 
         * <param name = "estabilidad" > La estabilidad del requisito.</param>
         * <returns>Retorna el valor string de la estabilidad.</returns>
         * 
         **/
        [Display(Name = "Estabilidad")]
        public string Estabilidad {get; set;}

        /**
         * Setter y Getter del estado del requisito.
         * 
         * <param name = "estado" > El estado del requisito.</param>
         * <returns>Retorna el valor string del estado.</returns>
         * 
         **/
        [Display(Name = "Estado")]
        public string Estado {get; set;}

        /**
         * Setter y Getter del tipo del requisito.
         * 
         * <param name = "tipo" > El tipo de usuario.</param>
         * <returns>Retorna el valor string del tipo.</returns>
         * 
         **/
        [Display(Name = "Tipo Usuario")]
        public string TipoUsuario {get; set;}

        /**
         * Setter y Getter del tipo del requisito.
         * 
         * <param name = "tipo requisito" > El tipo del requisito.</param>
         * <returns>Retorna el valor string del tipo.</returns>
         * 
         **/
        [Display(Name = "Tipo Requisito")]
        public string TipoRequisito { get; set; }

        /**
         * Setter y Getter del tipo de medida.
         * 
         * <param name = "medida" > La medida.</param>
         * <returns>Retorna el valor string de la medida.</returns>
         * 
         **/
        [Display(Name = "Medida")]
        public string Medida { get; set; }

        /**
         * Setter y Getter del tipo de la escala.
         * 
         * <param name = "escala" > La escala del requisito.</param>
         * <returns>Retorna el valor string de la escala.</returns>
         * 
         **/
        [Display(Name = "Escala")]
        public string Escala { get; set; }

        /**
         * Setter y Getter de la fecha actualizacion.
         * 
         * <param name = "fecha" > Fecha del requisito.</param>
         * <returns>.</returns>
         * 
         **/
        [Display(Name = "Fecha Actualización")]
        public string Fecha { get; set; }

        /**
         * Setter y Getter del incremento.
         * 
         * <param name = "incremento" > El incremento del requisito.</param>
         * <returns>Retorna el valor string del incremento.</returns>
         * 
         **/
        [Display(Name = "Incremento")]
        public string Incremento { get; set; }
        /**
         * Setter y Getter de los actores del requisito.
         * 
         * <param name = "actores" > La lista de actores involucrados en el requisito.</param>
         * <returns>Retorna la lista de actores.</returns>
         * 
         **/
        [Display(Name = "Actores")]
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
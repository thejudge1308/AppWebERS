using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
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
        public Requisito(string idRequisito, string nombre, string descripcion, string prioridad, string fuente, 
            string estabilidad, string estado, string tipoUsuario, string tipoRequisito, string medida, string escala, 
            string fecha, string incremento, string tipo)
        {
            IdRequisito = idRequisito;
            Nombre = nombre;
            Descripcion = descripcion;
            Prioridad = prioridad;
            Fuente = fuente;
            Estabilidad = estabilidad;
            Estado = estado;
            TipoUsuario = tipoUsuario;
            TipoRequisito = tipoRequisito; //Categoria en la BD
            Medida = medida;
            Escala = escala;
            Fecha = fecha; //AAAA-MM-DD
            Incremento = incremento;
            Tipo = tipo;

            //LOS ACTORES NO SE SI SON LAS FUENTES?
            this.Actores = new List<Actor>();
        }

        public Requisito()
        {
            this.IdRequisito = "";
            this.Nombre = "";
            this.Descripcion = "";
            this.Prioridad = "";
            this.Fuente = "";
            this.Estabilidad = "";
            this.Estado = "";
            this.TipoUsuario = "";
            this.TipoRequisito = "";
            this.Medida = "";
            this.Escala = "";
            this.Fecha = "";
            this.Incremento = "";
            this.Tipo = "";
        }

        private ApplicationDbContext conexion = ApplicationDbContext.Create();

        /**
         * Setter y Getter de ID del requisito
         * 
         * <param name = "idRequisito" > El identificador del requisito.</param>
         * <returns>Retorna el valor int del idRequisito.</returns>
         * 
         **/
        [Required(ErrorMessage = "El campo Código es obligatorio.")]
        //[RegularExpression("[0-9]*", ErrorMessage = ".")]
        [StringLength(20, ErrorMessage = "El código debe tener entre 3 a 20 caracteres ", MinimumLength = 3)]
        [Display(Name = "Código")]
        public string IdRequisito {get; set;}

        /**
         * Setter y Getter del nombre del requisito.
         * 
         * <param name = "nombre" > El nombre del requisito.</param>
         * <returns>Retorna el valor string del nombre.</returns>
         * 
         **/
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [Display(Name = "Nombre")]
        public string Nombre {get; set;}

        /**
         * Setter y Getter de la descripcion del requisito.
         * 
         * <param name = "descripcion" > La descripcion del requisito.</param>
         * <returns>Retorna el valor string de la descripcion.</returns>
         * 
         **/
        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
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
        [Required(ErrorMessage = "El campo Fuente es obligatorio.")]
        [StringLength(20, ErrorMessage = "La fuente debe tener menos de 20 caracteres ", MinimumLength = 1)]
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
        [Required(ErrorMessage = "El campo Tipo Usuario es obligatorio.")]
        [StringLength(20, ErrorMessage = "El tipo de usuario debe tener menos de 20 caracteres ", MinimumLength = 1)]
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
        [Required(ErrorMessage = "El campo Medida es obligatorio.")]
        [StringLength(20, ErrorMessage = "La medida debe tener a lo más 20 caracteres ", MinimumLength = 1)]
        [Display(Name = "Medida")]
        public string Medida { get; set; }

        /**
         * Setter y Getter del tipo de la escala.
         * 
         * <param name = "escala" > La escala del requisito.</param>
         * <returns>Retorna el valor string de la escala.</returns>
         * 
         **/
        [Required(ErrorMessage = "El campo Escala es obligatorio.")]
        [StringLength(20, ErrorMessage = "La Escala debe tener a lo más 20 caracteres ", MinimumLength = 1)]
        [Display(Name = "Escala")]
        public string Escala { get; set; }

        /**
         * Setter y Getter de la fecha actualizacion.
         * 
         * <param name = "fecha" > Fecha del requisito.</param>
         * <returns>.</returns>
         * 
         **/
        [Required(ErrorMessage = "El campo Fecha es obligatorio.")]
        [RegularExpression("^[0-9]{4}-[0-9]{2}-[0-9]{2}", ErrorMessage = "La fecha debe seguir el formato AAAA-MM-DD")]
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
        [Required(ErrorMessage = "El campo Incremento es obligatorio.")]
        [StringLength(20, ErrorMessage = "El Incremento debe tener a lo más 20 caracteres", MinimumLength = 1)]
        public string Incremento { get; set; }
        /**
         * Setter y Getter del incremento.
         * 
         * <param name = "tipo" > El Tipo del requisito.</param>
         * <returns>Retorna el valor string del Tipo.</returns>
         * 
         **/
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
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

        /**
         *
         *<author>Diego Iturriaga</author>
         *<summary>Metodo para registrar un requisito con todos sus parametros en la base de datos</summary>
         *<param name="idProyecto">Contiene un int con el id de un proyecto.</param>
         *<returns>Retorna True si el requisito se registra en la base de datos. False en caso contrario.</returns>
         */
        public bool RegistrarRequisito(int idProyecto)
        {
            string consultaInsert = "INSERT INTO requisito(id_requisito, nombre, descripcion, fuente, tipo_usuario, categoria, " +
                "prioridad, estabilidad, estado, medida, escala, incremento, fecha_actualizacion, ref_proyecto, tipo) " +
                "VALUES ('"+this.IdRequisito+ "','" +this.Nombre + "','" +this.Descripcion + "','" +this.Fuente + "','" +this.TipoUsuario + "','" +
                 this.TipoRequisito+"','" +this.Prioridad + "','" +this.Estabilidad + "','" +this.Estado + "','" +this.Medida
                 + "','" +this.Escala + "','" +this.Incremento + "','" +this.Fecha + "'," +idProyecto+ ",'"+this.Tipo+ "'); ";
            if (this.conexion.RealizarConsultaNoQuery(consultaInsert))
            {
                return true;
            }
            return false;
        }


    }
}
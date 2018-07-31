using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;

/**
 * Autor: Gerardo Estrada (Meister1412)
 **/

namespace AppWebERS.Models {
    public class Requisito
    {

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
            string estabilidad, string estado, string tipoRequisito, string medida, string escala,
            string fecha, string incremento, string tipo)
        {
            IdRequisito = idRequisito;
            Nombre = nombre;
            Descripcion = descripcion;
            Prioridad = prioridad;
            Fuente = fuente;
            Estabilidad = estabilidad;
            Estado = estado;
            TipoRequisito = tipoRequisito; //Categoria en la BD
            Medida = medida;
            Escala = escala;
            Fecha = fecha; //AAAA-MM-DD
            Incremento = incremento;
            Tipo = tipo;
            Actores = new List<CheckBox>();

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
            this.TipoRequisito = "";
            this.Medida = "";
            this.Escala = "";
            this.Fecha = "";
            this.Incremento = "";
            this.Tipo = "";
        }

        public class CheckBox
        {
            public string nombre { set; get; }
            public string id { set; get; }
            public bool isChecked { set; get; }
        }
        private ApplicationDbContext conexion = ApplicationDbContext.Create();
        private string idVerdadero = "";
        /**
         * Setter y Getter de ID del requisito
         * 
         * <param name = "idRequisito" > El identificador del requisito.</param>
         * <returns>Retorna el valor int del idRequisito.</returns>
         * 
         **/
        [Required(ErrorMessage = "El campo Código es obligatorio.")]
        //[RegularExpression("[0-9]*", ErrorMessage = ".")]
        [StringLength(20, ErrorMessage = "El código debe tener entre 3 a 20 caracteres.", MinimumLength = 3)]
        [Display(Name = "Código")]
        public string IdRequisito { get; set; }

        /**
         * Setter y Getter del nombre del requisito.
         * 
         * <param name = "nombre" > El nombre del requisito.</param>
         * <returns>Retorna el valor string del nombre.</returns>
         * 
         **/
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        /**
         * Setter y Getter de la descripcion del requisito.
         * 
         * <param name = "descripcion" > La descripcion del requisito.</param>
         * <returns>Retorna el valor string de la descripcion.</returns>
         * 
         **/
        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        /**
         * Setter y Getter de la prioridad del requisito.
         * 
         * <param name = "prioridad" > La prioridad del requisito.</param>
         * <returns>Retorna el valor string de la prioridad.</returns>
         * 
         **/
        [Display(Name = "Prioridad")]
        public string Prioridad { get; set; }

        /**
         * Setter y Getter de la fuente del requisito.
         * 
         * <param name = "fuente" > La fuente del requisito.</param>
         * <returns>Retorna el valor string de la fuente.</returns>
         * 
         **/
        [Required(ErrorMessage = "El campo Fuente es obligatorio.")]
        [StringLength(20, ErrorMessage = "La fuente debe tener menos de 20 caracteres.", MinimumLength = 1)]
        [Display(Name = "Fuente")]
        public string Fuente { get; set; }

        /**
         * Setter y Getter de la estabilidad del requisito.
         * 
         * <param name = "estabilidad" > La estabilidad del requisito.</param>
         * <returns>Retorna el valor string de la estabilidad.</returns>
         * 
         **/
        [Display(Name = "Estabilidad")]
        public string Estabilidad { get; set; }

        /**
         * Setter y Getter del estado del requisito.
         * 
         * <param name = "estado" > El estado del requisito.</param>
         * <returns>Retorna el valor string del estado.</returns>
         * 
         **/
        [Display(Name = "Estado")]
        public string Estado { get; set; }


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
        [StringLength(20, ErrorMessage = "La medida debe tener a lo más 20 caracteres.", MinimumLength = 1)]
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
        [StringLength(20, ErrorMessage = "La Escala debe tener a lo más 20 caracteres.", MinimumLength = 1)]
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
        [RegularExpression("^[0-9]{4}-[0-9]{2}-[0-9]{2}", ErrorMessage = "La fecha debe seguir el formato AAAA-MM-DD.")]
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
        [StringLength(20, ErrorMessage = "El Incremento debe tener a lo más 20 caracteres.", MinimumLength = 1)]
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
        public List<CheckBox> Actores { get; set; }

        /**
         * Método para Crear un Requisito
         * <returns>Retorna un boolean que indica el correcto registro del requisito.</returns>
         **/
        public bool Crear()
        {
            return true;
        }

        /**
         * Método para listar un registro específico
         * <returns>Retorna un registro específico.</returns>
         **/

        public void ListarEspecifico(Proyecto proyecto)
        {

        }

        /**
         * Método para seleccionar un requisito 
         **/

        public void Seleccionar(int id)
        {

        }

        public void agregarActor(string nombre)
        {
            Actores.Add(new CheckBox() { nombre = nombre, isChecked = false });
        }

        /**
         * Método para cargar datos
         **/

        public void CargarDatos(DataRow dr)
        {

        }

        public string RegistrarRequisito(int idProyecto)
        {
            string value = "";
            string consultaInsert = "START TRANSACTION;" +
                "INSERT INTO requisito(id_requisito, nombre, descripcion, fuente, categoria, " +
                "prioridad, estabilidad, estado, medida, escala, incremento, fecha_actualizacion, ref_proyecto, tipo) " +
                "VALUES ('" + this.IdRequisito + "','" + this.Nombre + "','" + this.Descripcion + "','" + this.Fuente + "','" +
                 this.TipoRequisito + "','" + this.Prioridad + "','" + this.Estabilidad + "','" + this.Estado + "','" + this.Medida
                 + "','" + this.Escala + "','" + this.Incremento + "','" + this.Fecha + "'," + idProyecto + ",'" + this.Tipo + "'); " +
                 "SELECT LAST_INSERT_ID() AS T1;" +
                 "COMMIT;";
            ConectorBD Conector = ConectorBD.Instance;
            MySqlDataReader reader = Conector.RealizarConsulta(consultaInsert);
            if (reader != null)
            {
                while (reader.Read())
                {
                    value = reader[0].ToString();
                }
                Conector.CerrarConexion();
            }
            return value;

            
        }


        public bool registrarActor(string actor, string id)
        {
            string n = this.idVerdadero;
            string consultaInsertar = "INSERT INTO vinculo_actor_requisito(ref_actor,ref_req) VALUES('" + actor + "','" + id + "');";
            if (this.conexion.RealizarConsultaNoQuery(consultaInsertar))
            {
                return true;
            }
            return false;
        }
    }
}
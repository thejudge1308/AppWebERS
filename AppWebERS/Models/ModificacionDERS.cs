using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebERS.Models{
    /*
     * Matías Parra
     */
    public class ModificacionDERS{
        private int idModificacion;
        private double version;
        private DateTime fecha;
        private string refUsuario;

        /*
         * Constructor vacío de la clase modificacionesDERS (se agrega para cualquier otro uso que se le de en un futuro).
         * 
         */
        public ModificacionDERS(){

        }

        /*
         * Constructor de la clase ModificacionDERS.
         * 
         * <param name="idModificacion">El id de modificación.</param>
         * <param name="version">La versión de la modificación.</param>
         * <param name="fecha">La fecha de la modificación.</param>
         * <param name="refUsuario">La referencia entre el usuario y la modificación.</param>
         * 
         */
        public ModificacionDERS(int idModificacion, double version, DateTime fecha, string refUsuario){
            this.idModificacion = idModificacion;
            this.version = version;
            this.fecha = fecha;
            this.refUsuario = refUsuario;
        }

        /*
         * Setter y getter del id de la modificación.
         * 
         * <param name="idModificacion">El id de modificación.</param>
         * 
         * <returns>Retorna el valor integer del id de la motificación.</returns>
         * 
         */
        public int IdModificacion {get; set;}

        /*
         * Setter y getter de la versión de la modificación.
         * 
         * <param name="version">La versión de la modificación.</param>
         * 
         * <returns>Retorna el valor double de la versión de la modificación.</returns>
         * 
         */
        public double Version {get; set;}

        /*
         * Setter y getter de la fecha de modificación.
         * 
         * <param name="fecha">La fecha de la modificación.</param>
         * 
         * <returns>Retorna el valor DateTime de la fecha de modificación.</returns>
         * 
         */
        public DateTime Fecha {get; set;}

        /*
         * Setter y getter de la referencia de usuario.
         * 
         * <param name="refUsuario">La referencia entre el usuario y la modificación.</param>
         * 
         * <returns>Retorna el valor string de la referencia de usuario.</returns>
         * 
         */
        public string RefUsuario {get; set;}

        /**
         * Método para Crear una Modificacion
         * <returns>Retorna un boolean que indica el correcto registro de la modificacion.</returns>
         **/

        public bool crear() {
            return true;
        }

        /**
         * Método para listar una modificación específica
         * <returns>Retorna una modificación específica.</returns>
         **/

        public void listarEspecifico(Proyecto proyecto) {
            int aux = 0;
            modificacionesEspecificas = new List<ModificacionDERS>();
            numModificaciones = proyecto.modificaciones.Count;
            while (aux < numModificaciones) {
                if (proyecto.modificaciones.id.equals(this.id)) {
                    modificacionesEspecificas.add(proyecto.modificaciones(aux));
                }
            }
            return modificacionesEspecificas;
        }

        /**
         * Método para seleccionar una modificación 
         **/

        public void seleccionar(int id) {

        }

        /**
         * Método para cargar datos
         **/

        public void cargarDatos(dr DataRow) {

        }
    }
}
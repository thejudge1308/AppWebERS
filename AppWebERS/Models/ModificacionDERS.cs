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
        public int IdModificacion{
            get => idModificacion;
            set => idModificacion = value;
        }

        /*
         * Setter y getter de la versión de la modificación.
         * 
         * <param name="version">La versión de la modificación.</param>
         * 
         * <returns>Retorna el valor double de la versión de la modificación.</returns>
         * 
         */
        public double Version{
            get => version;
            set => version = value;
        }

        /*
         * Setter y getter de la fecha de modificación.
         * 
         * <param name="fecha">La fecha de la modificación.</param>
         * 
         * <returns>Retorna el valor DateTime de la fecha de modificación.</returns>
         * 
         */
        public DateTime Fecha{
            get => fecha;
            set => fecha = value;
        }

        /*
         * Setter y getter de la referencia de usuario.
         * 
         * <param name="refUsuario">La referencia entre el usuario y la modificación.</param>
         * 
         * <returns>Retorna el valor string de la referencia de usuario.</returns>
         * 
         */
        public string RefUsuario{
            get => refUsuario;
            set => refUsuario = value;
        }
    }
}
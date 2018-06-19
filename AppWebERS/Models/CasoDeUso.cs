using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebERS.Models{
    /*
     * Matías Parra
     */
    public class CasoDeUso{
        private int idCasoDeUso;
        private string rutaImagen;

        /*
         * Constructor vacío de la clase CasoDeUso (se agrega para cualquier otro uso que se le de en un futuro).
         * 
         */
        public CasoDeUso(){

        }

        /*
         * Constructor de la clase ModificacionDERS.
         * 
         * <param name="idCasoDeUso">El id del caso de uso.</param>
         * <param name="rutaImagen">La ruta de la imagen del caso de uso.</param>
         * 
         */
        public CasoDeUso(int idCasoDeUso, string rutaImagen){
            this.idCasoDeUso = idCasoDeUso;
            this.rutaImagen = rutaImagen;
        }

        /*
         * Setter y getter del id del caso de uso.
         * 
         * <param name="idCasoDeUso">El id del caso de uso.</param>
         * 
         * <returns>Retorna el valor integer del id del caso de uso.</returns>
         * 
         */
        public int IdCasoDeUso{
            get => idCasoDeUso;
            set => idCasoDeUso = value;
        }

        /*
         * Setter y getter de la ruta de la imagen del caso de uso.
         * 
         * <param name="rutaImagen">La ruta de la imagen del caso de uso.</param>
         * 
         * <returns>Retorna el valor string de la ruta de la imagen.</returns>
         * 
         */
        public string RutaImagen{
            get => rutaImagen;
            set => rutaImagen = value;
        }
    }
}
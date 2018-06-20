using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AppWebERS.Models{
    /*
     * Matías Parra
     */
    public class CasoDeUso{

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
            this.IdCasoDeUso = idCasoDeUso;
            this.RutaImagen = rutaImagen;
        }

        /*
         * Setter y getter del id del caso de uso.
         * 
         * <param name="idCasoDeUso">El id del caso de uso.</param>
         * 
         * <returns>Retorna el valor integer del id del caso de uso.</returns>
         * 
         */
        public int IdCasoDeUso {get; set;}

        /*
         * Setter y getter de la ruta de la imagen del caso de uso.
         * 
         * <param name="rutaImagen">La ruta de la imagen del caso de uso.</param>
         * 
         * <returns>Retorna el valor string de la ruta de la imagen.</returns>
         * 
         */
        public string RutaImagen {get; set;}

        /**
         * Método para Crear un Caso de Uso
         * <returns>Retorna un boolean que indica el correcto registro del caso de uso.</returns>
         **/

        public bool Crear() {
            return true;
        }

        /**
         * Método para listar un caso de uso específico
         * <returns>Retorna un caso de uso específico.</returns>
         **/

        public void ListarEspecifico(Proyecto proyecto) {
           
        }

        /**
         * Método para seleccionar un caso de uso 
         **/

        public void Seleccionar(int id) {

        }

        /**
         * Método para cargar datos
         **/

        public void CargarDatos(DataRow dr) {

        }
    }
}
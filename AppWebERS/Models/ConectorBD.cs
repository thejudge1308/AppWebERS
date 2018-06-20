using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebERS.Models{
    /*
     * Matías Parra
     */
    public class ConectorBD{
        private string connString;

        /*
         * Constructor vacío de la clase ConectorBD (se agrega para cualquier otro uso que se le de en un futuro).
         * 
         */
        public ConectorBD(){

        }

        /*
         * Constructor de la clase ConectorBD.
         * 
         * <param name="connString">El conector de la base de datos.</param>
         * 
         */
        public ConectorBD(string connString){
            this.connString = connString;
        }

        /*
         * Setter y getter del conector de la base de datos.
         * 
         * <param name="connString">El conector de la base de datos.</param>
         * 
         * <returns>Retorna el valor string del conector de la base de datos.</returns>
         * 
         */
        public string ConnString {get; set;}

        /**
         * Método para ejecutar una query 
         **/

        //public void ejecutarQuery(command MySqlCommand) {

        //}

        /**
         * Método para ejecutar una query 
         * <returns>Retorna un DataSet</returns>
         **/

        //public void ejecutarNoQuery(command MySqlCommand) {

        //}

        /**
         * Método para no ejecutar una query 
         * <returns>Retorna un MySqlCommand</returns>
         **/

        //public void ejecutarNoQuery(command MySqlCommand) {

        //}
    }
}
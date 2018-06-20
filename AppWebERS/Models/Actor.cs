using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/**
 * Autor: Gerardo Estrada (Meister1412)
 **/

namespace AppWebERS.Models {
    public class Actor {
        private int idActor;
        private string descripcion;
        private int numActual;
        private int numFuturo;
        private int numContactables;
        private string tipoUsuario;

        /**
         * Constructor de la clase Actor
         * 
         * <param name = "idActor" > El ID del actor.</param>
         * <param name = "descripcion" > La descripción del actor.</param>
         * <param name = "numActual" > El número actual del actor.</param>
         * <param name = "numFuturo" > El número futuro del actor.</param>
         * <param name = "numContactables" > El número de contactables del actor.</param>
         * <param name = "tipoUsuario" > El tipo de usuario del actor.</param>
         **/

        public Actor(int idActor, string descripcion, int numActual, int numFuturo, int numContactables, string tipoUsuario) {
            this.idActor = idActor;
            this.descripcion = descripcion;
            this.numActual = numActual;
            this.numFuturo = numFuturo;
            this.numContactables = numContactables;
            this.tipoUsuario = tipoUsuario;
        }

        /**
         * Setter y Getter del ID del actor
         * 
         * <param name = "idActor" > El ID del actor.</param>
         * <returns>Retorna el valor int del ID.</returns>
         * 
         **/

        public int IdActor {get; set;}

        /**
         * Setter y Getter de la descripcion del actor
         * 
         * <param name = "proposito" > La descripcion del actor.</param>
         * <returns>Retorna el valor string de la descripcion del actor.</returns>
         * 
         **/

        public string Descripcion {get; set;}

        /**
         * Setter y Getter del número actual del actor
         * 
         * <param name = "numActual" > El numero actual del actor.</param>
         * <returns>Retorna el valor int del numero actual.</returns>
         * 
         **/

        public int NumActual {get; set;}

        /**
         * Setter y Getter del número futuro del actor
         * 
         * <param name = "numFuturo" > El numero futuro del actor.</param>
         * <returns>Retorna el valor int del numero futuro.</returns>
         * 
         **/

        public int NumFuturo {get; set;}

        /**
         * Setter y Getter del número de contactables del actor
         * 
         * <param name = "numContactables" > El numero de contactables del actor.</param>
         * <returns>Retorna el valor int del numero de contactables.</returns>
         * 
         **/

        public int NumContactables {get; set;}

        /**
         * Setter y Getter del tipo de usuario
         * 
         * <param name = "tipoUsuario" > El tipo de usuario que corresponde al actor.</param>
         * <returns>Retorna el valor string del tipo de usuario.</returns>
         * 
         **/

        public string TipoUsuario {get; set;}

        /**
         * Método para Crear un Actor
         * <returns>Retorna un boolean que indica la correcta creación del actor.</returns>
         **/

        public bool crear() {
            return true;
        }

        /**
         * Método para listar un actor específico
         * <returns>Retorna un actor específico.</returns>
         **/

        public void listarEspecifico(Proyecto proyecto) {
          
        }

        /**
         * Método para seleccionar un actor 
         **/

        public void seleccionar(int id) {

        }

        /**
         * Método para cargar datos
         **/

        public void cargarDatos(DataRow dr) {

        }
    }
}
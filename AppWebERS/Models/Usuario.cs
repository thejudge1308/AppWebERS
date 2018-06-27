using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AppWebERS.Models{
    /*
     * Matías Parra
     */
    public class Usuario{
        /*
         * Constructor vacío de la clase usuario (se agrega para cualquier otro uso que se le de en un futuro).
         * 
         */
        public Usuario(){

        }

        /*
         * Constructor de la clase usuario.
         * 
         * <param name="rut">El rut del usuario.</param>
         * <param name="nombre">El nombre del usuario.</param>
         * <param name="correoElectronico">El correo electrónico del usuario.</param>
         * <param name="contrasenia">La contraseña del usuario.</param>
         * <param name="tipo">El tipo del usuario (administrador, jefe de proyecto y usuario normal).</param>
         * 
         */
        public Usuario(string rut, string nombre, string correoElectronico, string contrasenia, string tipo){
            this.Rut = rut;
            this.Nombre = nombre;
            this.CorreoElectronico = correoElectronico;
            this.Contrasenia = contrasenia;
            this.Tipo = tipo;
        }

        /*
         * Setter y getter de rut del usuario.
         * 
         * <param name="rut">El rut del usuario.</param>
         * 
         * <returns>Retorna el valor string del rut.</returns>
         * 
         */
        public string Rut {get; set;}

        /*
         * Setter y getter de nombre del usuario.
         * 
         * <param name="nombre">El nombre del usuario.</param>
         * 
         * <returns>Retorna el valor string del rut.</returns>
         * 
         */
        public string Nombre {get; set;}

        /*
         * Setter y getter de correo electrónico del usuario.
         * 
         * <param name="correoElectronico">El correo electrónico del usuario.</param>
         * 
         * <returns>Retorna el valor string del correo electrónico.</returns>
         * 
         */
        public string CorreoElectronico {get; set;}

        /*
         * Setter y getter de contraseña del usuario.
         * 
         * <param name="contrasenia">La contraseña del usuario.</param>
         * 
         * <returns>Retorna el valor string de la contraseña.</returns>
         * 
         */
        public string Contrasenia {get; set;}

        /*
         * Setter y getter de tipo del usuario.
         * 
         * <param name="tipo">El tipo del usuario (administrador, jefe de proyecto y usuario normal).</param>
         * 
         * <returns>Retorna el valor string del tipo.</returns>
         * 
         */
        public string Tipo {get; set;}

        /**
         * Método para listar todos los usuarios existentes
         **/

        public void ListarTodos() {
        }

        /**
         * Método para listar un usuario específico
         * <returns>Retorna un usuario específico.</returns>
         **/

       public void ListarEspecifico(Usuario usuario) {
            
        }
        
        /**
         * Método para seleccionar un usuario 
         **/

        public void Seleccionar(int id) {

        }

        /**
         * Método para Crear un Usuario
         * <returns>Retorna un boolean que indica la correcta creación del usuario.</returns>
         **/

        public bool Crear() {

            string values = this.Rut + "," + this.Nombre + "," + this.CorreoElectronico + "," + this.Contrasenia + "," + this.Tipo + ",Activo" ;
            string consulta = "INSERT INTO Usuario (rut,nombre,correo_electronico,constrasenia,tipo,estado) VALUES ( " + values +")";
            return true;
        }

        /**
         * Método para cargar datos
         **/

        public void CargarDatos(DataRow dr) {

        }
    }
}
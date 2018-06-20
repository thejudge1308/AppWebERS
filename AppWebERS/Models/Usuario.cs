using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebERS.Models{
    /*
     * Matías Parra
     */
    public class Usuario{
        private string rut;
        private string nombre;
        private string correoElectronico;
        private string contrasenia;
        private string tipo;

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
            this.rut = rut;
            this.nombre = nombre;
            this.correoElectronico = correoElectronico;
            this.contrasenia = contrasenia;
            this.tipo = tipo;
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

        public void listarTodos() {
        }

        /**
         * Método para listar un usuario específico
         * <returns>Retorna un usuario específico.</returns>
         **/

        public void listarEspecifico(Usuario usuario) {
            int aux = 0;
            usuariosEspecificos = new List<Usuario>();
            numUsuarios = proyecto.usuarios.Count;
            while (aux < numUsuarios) {
                if (proyecto.usuarios.id.equals(usuario.id)) {
                    usuariosEspecificos.add(proyecto.usuarios(aux));
                }
            }
            return usuariosEspecificos;
        }

        /**
         * Método para seleccionar un usuario 
         **/

        public void seleccionar(int id) {

        }

        /**
         * Método para Crear un Usuario
         * <returns>Retorna un boolean que indica la correcta creación del usuario.</returns>
         **/

        public bool crear() {
            return true;
        }

        /**
         * Método para cargar datos
         **/

        public void cargarDatos(dr DataRow) {

        }
    }
}
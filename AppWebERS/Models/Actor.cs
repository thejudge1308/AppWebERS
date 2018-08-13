using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Web;

/**
 * Autor: Gerardo Estrada (Meister1412)
 **/

namespace AppWebERS.Models {
    public class Actor {

        /**
         * Constructor de la clase Actor
         * 
         * <param name = "idActor" > El ID del actor.</param>
         * <param name = "descripcion" > La descripción del actor.</param>
         * <param name = "numActual" > El número actual del actor.</param>
         * <param name = "numFuturo" > El número futuro del actor.</param>
         * <param name = "numContactables" > El número de contactables del actor.</param>
         * <param name = "nombre" > El nombre del actor.</param>
         **/

        public Actor(int idActor, string descripcion, int numActual, int numFuturo, string nombre) {
            this.IdActor = idActor;
            this.Descripcion = descripcion;
            this.NumActual = numActual;
            this.NumFuturo = numFuturo;
            this.Nombre = nombre;
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
         * Setter y Getter del nombre
         * 
         * <param name = "nombre" > El nombre que corresponde al actor.</param>
         * <returns>Retorna el valor string del nombre.</returns>
         * 
         **/

        public string Nombre {get; set;}

        /**
         * Método para Crear un Actor
         * <returns>Retorna un boolean que indica la correcta creación del actor.</returns>
         **/


        public bool Crear() {
            return true;
        }

        /**
         * Método para listar un actor específico
         * <returns>Retorna un actor específico.</returns>
         **/

        public void ListarEspecifico(Proyecto proyecto) {
          
        }

        /**
         * Método para seleccionar un actor 
         **/

        public void Seleccionar(int id) {

        }

        /**
         * Método para cargar datos
         **/

        public void CargarDatos(DataRow dr) {

        }

        public bool AgregarModificacionActoresDERS(int id, string fecha, string userId, string descripcion)
        {

            float versionActual = this.ObtenerVersionActual(id) + 0.01F;
            string vers = versionActual.ToString().Replace(',', '.');
            string consulta = "INSERT INTO modificacion_ders(version,ref_proyecto,fecha,ref_autor_modificacion,descripcion) " +
                "VALUES(" + vers + ", " + id + ", '" + fecha + "' , '" + userId + "' , '" + descripcion + "' ) ";
            ApplicationDbContext con = ApplicationDbContext.Create();
            if (con.RealizarConsultaNoQuery(consulta))
            {
                return true;
            }
            return false;
        }

        private float ObtenerVersionActual(int id)
        {

            float version = 0.00F;
            string consulta = "SELECT modificacion_ders.version FROM modificacion_ders WHERE ref_proyecto = " + id +
                " ORDER BY version DESC LIMIT 1";
            ApplicationDbContext con = ApplicationDbContext.Create();
            MySqlDataReader reader = con.RealizarConsulta(consulta);

            if (reader != null)
            {
                reader.Read();
                version = float.Parse(reader[0].ToString());
                return version;
            }
            else
            {

                return 0.00F;
            }
        }
    }
}
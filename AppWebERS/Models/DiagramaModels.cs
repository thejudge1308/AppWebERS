using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;

namespace AppWebERS.Models
{
    public class DiagramaModels
    {
        /**
         * Clase utilizada para asignar a una imagen su nombre y su correspondiente URL
         */
        public class Imagen
        {
            public string nombre { set; get; }
            public string url { set; get; }
        }
        /**
        * <param name = "urlsCasosDeUso" > Lista con las URL's con los diagramas de casos de uso</param>
        * <param name = "urlsaArquiFisica" > Lista con las URL'S con los diagramas de aquitectura fisica</param>
        * <param name = "urlsArquiLogica" > Lista con las URL's con los diagramas de aquitectura logica</param>
        * <param name = "IdProyecto" > Identificador de un proyecto</param>
        * <param name = "TipoPermiso" >Permiso del usuario logeado</param>
        **/
        public List<Imagen> urlsCasosDeUso { get; set; }
        public List<Imagen> urlsaArquiFisica { get; set; }
        public List<Imagen> urlsArquiLogica { get; set; }
        public int IdProyecto { get; set; }
        public int TipoPermiso { get; set; }
        public DiagramaModels(){ }

        /**
         * Constructor del modelo diagrama
        * <param name = "idProyecto" > Identificador del proyecto que contiene los diagramas</param>
        * <param name = "tipoPermiso" > Permiso del usuario logeado en el sistema</param>
        */
        public DiagramaModels(int idProyecto, int tipoPermiso){
            this.IdProyecto = idProyecto;
            this.TipoPermiso = tipoPermiso;
            this.urlsaArquiFisica = this.agregarURLArquiFisica(IdProyecto.ToString());
            this.urlsArquiLogica = this.agregarURLArquiLogica(IdProyecto.ToString());
            this.urlsCasosDeUso = this.agregarURLCasos(IdProyecto.ToString());
        }

        /**
         * <author>Jose Nunnez</author>
         * <summary>
         * Metodo que permite obtner los diagramas de casos de uso de un proyecto
         * </summary>
         * <param name=""="id"> Es el ID correspondiente a un proyecto
         * <returns> Un lista que contiene todos los diagramas encontrados en la BD</returns>
         */
        public List<Imagen> agregarURLCasos(string id) {
            string consulta = "SELECT diagrama.ruta, diagrama.nombre FROM diagrama WHERE diagrama.tipo ='CASO_DE_USO' AND diagrama.ref_proyecto = " + id + ";";
            List<Imagen> image = new List<Imagen>();
            ApplicationDbContext con = ApplicationDbContext.Create();
            MySqlDataReader reader = con.RealizarConsulta(consulta);
            if (reader != null)
            {
                while (reader.Read())
                {
                    Imagen im = new Imagen();
                    im.url = reader[0].ToString();
                    im.nombre = reader[1].ToString();
                    image.Add(im);
                }
            }
            con.EnsureConnectionClosed();

            return image;
        }
        /**
       * <author>Jose Nunnez</author>
       * <summary>
       * Metodo que permite obtner los diagramas de arquitectura fisica de un proyecto
       * </summary>
       * <param name=""="id"> Es el ID correspondiente a un proyecto
       * <returns> Un lista que contiene todos los diagramas encontrados en la BD</returns>
       */
        public List<Imagen> agregarURLArquiFisica(string id)
        {
            string consulta = "SELECT diagrama.ruta, diagrama.nombre FROM diagrama WHERE diagrama.tipo ='ARQ_FISICA' AND diagrama.ref_proyecto = " + id + ";";
            List<Imagen> image = new List<Imagen>();
            ApplicationDbContext con = ApplicationDbContext.Create();
            MySqlDataReader reader = con.RealizarConsulta(consulta);
            if (reader != null)
            {
                while (reader.Read())
                {
                    Imagen im = new Imagen();
                    im.url = reader[0].ToString();
                    im.nombre = reader[1].ToString();
                    image.Add(im);
                }
            }
            con.EnsureConnectionClosed();
            return image;
        }
        /**
      * <author>Jose Nunnez</author>
      * <summary>
      * Metodo que permite obtner los diagramas de arquitectura lógica de un proyecto
      * </summary>
      * <param name=""="id"> Es el ID correspondiente a un proyecto
      * <returns> Un lista que contiene todos los diagramas encontrados en la BD</returns>
      */
        public List<Imagen> agregarURLArquiLogica(string id)
        {
            string consulta = "SELECT diagrama.ruta, diagrama.nombre FROM diagrama WHERE diagrama.tipo ='ARQ_LOGICA' AND diagrama.ref_proyecto = " + id + ";";
            List<Imagen> image = new List<Imagen>();
            ApplicationDbContext con = ApplicationDbContext.Create();
            MySqlDataReader reader = con.RealizarConsulta(consulta);
            if (reader != null)
            {
                while (reader.Read())
                {
                    Imagen im = new Imagen();
                    im.url = reader[0].ToString();
                    im.nombre = reader[1].ToString();
                    image.Add(im);
                }
            }
            con.EnsureConnectionClosed();
            return image;
        }
    }
}
 
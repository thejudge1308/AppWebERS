using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;

namespace AppWebERS.Models
{
    public class DiagramaModels
    {
        public class Imagen
        {
            public string nombre { set; get; }
            public string url { set; get; }
        }

        public List<Imagen> urlsCasosDeUso { get; set; }
        public List<Imagen> urlsaArquiFisica { get; set; }
        public List<Imagen> urlsArquiLogica { get; set; }
        public int IdProyecto { get; set; }
        public int TipoPermiso { get; set; }
        public DiagramaModels(){ }

        public DiagramaModels(int idProyecto, int tipoPermiso){
            this.IdProyecto = idProyecto;
            this.TipoPermiso = tipoPermiso;
            this.urlsaArquiFisica = this.agregarURLArquiFisica(IdProyecto.ToString());
            this.urlsArquiLogica = this.agregarURLArquiLogica(IdProyecto.ToString());
            this.urlsCasosDeUso = this.agregarURLCasos(IdProyecto.ToString());
        }
        
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
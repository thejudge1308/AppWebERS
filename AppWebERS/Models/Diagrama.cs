using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebERS.Models
{
    public class Diagrama
    {

        private int Id { get; set; }

        private string nombre;

        public string GetNombre()
        {
            return nombre;
        }

        private void SetNombre(string value)
        {
            nombre = value;
        }

        private string ruta;

        public string GetRuta()
        {
            return ruta;
        }

        private void SetRuta(string value)
        {
            ruta = value;
        }

        private string tipo;

        public string GetTipo()
        {
            return tipo;
        }

        private void SetTipo(string value)
        {
            tipo = value;
        }

        private int RefProyecto { get; set; }

        public Diagrama(int id, string nombre, string ruta, string tipo, int refProyecto)
        {
            Id = id;
            SetNombre(nombre);
            SetRuta(ruta);
            SetTipo(tipo);
            RefProyecto = refProyecto;
        }


    }
}
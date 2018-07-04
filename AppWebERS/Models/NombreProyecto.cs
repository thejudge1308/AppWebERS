using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebERS.Models
{
    public class NombreProyecto
    {
        public NombreProyecto(string nombre)
        {
            this.Nombre = nombre;
        }
        public string Nombre { get; set; }
    }
}
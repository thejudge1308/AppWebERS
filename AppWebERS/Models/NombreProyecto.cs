using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebERS.Models
{
    public class NombreProyecto
    {
        public NombreProyecto(string nombre,string id)
        {
            this.Nombre = nombre;
            this.Id = id;
        }
        public string Nombre { get; set; }
        public string Id { get; set; }
    }
}
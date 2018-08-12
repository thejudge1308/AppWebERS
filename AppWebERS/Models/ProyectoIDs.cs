using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebERS.Models
{
    public class ProyectoIDs
    {
        public ProyectoIDs()
        {

        }

        public ProyectoIDs(int IdProyecto)
        {
            this.IdProyecto = IdProyecto;
        }


        /**
         * Setter y Getter de ID del proyecto
         * 
         * <param name = "idProyecto" > El identificador del proyecto.</param>
         * <returns>Retorna el valor int del identificador.</returns>
         * 
         **/
        public int IdProyecto { get; set; }

        
    }
}
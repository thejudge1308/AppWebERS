﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebERS.Models
{
    public class Referencia
    {
        public int ref_proyecto { get; set; }
        public string referencia { get; set; }

        public Referencia(int ref_proyecto, string referencia)
        {
            this.ref_proyecto = ref_proyecto;
            this.referencia = referencia;
        }

        public string getReferencia()
        {
            return this.referencia;
        }
    }
}

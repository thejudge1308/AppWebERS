using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebERS.Utilidades
{
   
    public class TipoAlerta
    {
        public const string ERROR = "alert-danger";
        public const string SUCCESS = "alert-success";
        public const string WARNING = "alert-warning";
    }

    public class Alerta
    {
        public readonly string _mensaje;
        public readonly string _tipoAlerta;

        public Alerta(string mensaje, string tipoAlerta) {
            _mensaje = mensaje;
            _tipoAlerta = tipoAlerta;
        }

        public string Mensaje { get { return _mensaje; } }
        public string TipoAlerta { get { return _tipoAlerta; }}
    }
}
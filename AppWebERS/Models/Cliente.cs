using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppWebERS.Models
{
    public class Cliente
    {
        public int Id { get;}
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(35, ErrorMessage = "El largo del nombre deber ser entre 2 a 35 caracteres.", MinimumLength = 2)]
        [RegularExpression(@"(\s|[a-zA-ZñÑáéíóúÁÉÍÓÚ])*", ErrorMessage = "Nombre no válido.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; }
        [Required(ErrorMessage = "El campo Rol es obligatorio.")]
        [StringLength(35, ErrorMessage = "El largo del rol deber ser entre 2 a 35 caracteres.", MinimumLength = 2)]
        [RegularExpression(@"(\s|[a-zA-ZñÑáéíóúÁÉÍÓÚ])*", ErrorMessage = "Rol no válido.")]
        [Display(Name = "Rol")]
        public string Rol { get; }
        [Required(ErrorMessage = "El campo Correo electrónico es obligatorio.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,3})(\]?)$", ErrorMessage = "Por favor ingrese un correo válido.")]
        [Display(Name = "Correo de contacto")]
        public string Contacto { get; }
        public int RefProyecto { get; }

        public Cliente() { }

        ///<autor>Gabriel Sanhueza</autor>
        /// <summary>
        /// Constructor de la clase cliente
        /// </summary>
        /// <param name="id">Id asignado por la base de datos para un cliente</param>
        /// <param name="nombre">Nombre del cliente</param>
        /// <param name="rol">El rol que tiene el cliente</param>
        /// <param name="contacto">El numero de contacto del cliente</param>
        /// <param name="refProyecto">La referencia al proyecto al que esta asociado el cliente</param>
        public Cliente(int id, string nombre, string rol, string contacto, int refProyecto) : this(nombre, rol, contacto, refProyecto)
        {
            Id = id;
        }

        ///<autor>Gabriel Sanhueza</autor>
        /// <summary>
        /// Constructor de la clase cliente
        /// </summary>
        /// <param name="nombre">Nombre del cliente</param>
        /// <param name="rol">El rol que tiene el cliente</param>
        /// <param name="contacto">El numero de contacto del cliente</param>
        /// <param name="refProyecto">La referencia al proyecto al que esta asociado el cliente</param>
        public Cliente(string nombre, string rol, string contacto, int refProyecto)
        {
            Nombre = nombre;
            Rol = rol;
            Contacto = contacto;
            RefProyecto = refProyecto;
        }

        /// <summary>
        /// Registra un cliente en la base de datos
        /// </summary>
        /// <param name="cliente"> El objeto cliente que contiene los datos del cliente a registrar</param>
        public void registrarCliente(string nombre, string rol, string contacto, int refProyecto)
        {
            using (var db = ApplicationDbContext.Create())
            {
                string query = @"INSERT INTO `appers`.`cliente_proyecto` (`Nombre`, `Rol`, `Contacto`, `ref_proyecto`) 
                              VALUES (@nombre, @rol, @contacto, @refProyecto);";
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@nombre", nombre);
                parametros.Add("@rol", rol);
                parametros.Add("@contacto", contacto);
                parametros.Add("@refProyecto", refProyecto);

                db.Execute(query, parametros);
            }
        }

        /// <summary>
        /// Obtiene todos los clientes de la base de datos dado una referencia de un proyecto
        /// </summary>
        /// <param name="idProyecto"> El id del proyecto del que se quieren obtener los clientes</param>
        /// <returns>Una lista de todos los clientes del proyecto</returns>
        public List<Cliente> obtenerTodosLosClientes(int idProyecto)
        {
            List<Cliente> clientes = new List<Cliente>();
            using (var db = ApplicationDbContext.Create())
            {
                string query = @"SELECT * FROM `cliente_proyecto` WHERE `ref_proyecto` = @idProyecto";
                Dictionary<string, object> parametros = new Dictionary<string, object>() {{ "@idProyecto", idProyecto } };
                List<Dictionary<string, string>> filas = db.Query(query, parametros);

                foreach (var fila in filas)
                {
                    string id = fila["Id"];
                    string nombre = fila["Nombre"];
                    string rol = fila["Rol"];
                    string contacto = fila["Contacto"];
                    string refProyecto = fila["ref_proyecto"];
                    clientes.Add(new Cliente(Int32.Parse(id), nombre, rol, contacto, Int32.Parse(refProyecto)));
                }
            }
            return clientes;
        }
    }
}
﻿using AppWebERS.Models;
using AppWebERS.Utilidades;
using AspNet.Identity.MySQL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWebERS.Controllers
{
    public class SolicitudDeUsuarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        /**
         * <author>Ariel Cornejo</author>
         * <sumary>
         * Metodo encargado de de devolver la interfaz asociada a lista de solictudes pendientes para un usuario.
         * </sumary>
         * <returns>La vista ListadoSolictudUsuario la cual contiene la tabla con las solicitudes pendientes del usuario</returns>
         * 
         */
        [HttpGet]
        public ActionResult ListadoSolicitudUsuario()
        {
            SolicitudDeUsuario solicitud = new SolicitudDeUsuario();
            String id;
            String tipo;
            using (var db = ApplicationDbContext.Create())
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                string s = User.Identity.GetUserId();
                ApplicationUser user = userManager.FindByIdAsync(s).Result;
                id = user.Id;
                tipo = user.Tipo;

            }
            if (tipo == "USUARIO")
            {
                var list = solicitud.ObtenerSolicitudesDeUsuario(id);
                ViewBag.MiListadoSolicitudes = list;
                if (list.Count == 0)
                {
                    ViewBag.vacio = true;
                    return View();
                }
                ViewBag.vacio = false;
                return View();
            }

            return RedirectToAction("Index", "Home");


        }

        /**
         * 
         * <author>Diego Iturriaga</author>
         * <summary>
         * Metodo para el funcionamiento del boton aceptar de la interfaz y asi obtener el id del usuario a partir del usuario 
         * logeado y el id del proyecto enviada por la interfaz para poder registra la aceptacion de la solicitud
         * </summary>
         * <param name="idProyecto">id del proyecto cuya solicitud fue Aceptada por el usuario logeado en el sistema.</param>
         * <returns>Retorna la misma vista para actualizar la tabla.</returns>
         * 
         **/
        public ActionResult Aceptar(int idProyecto)
        {
            SolicitudDeUsuario solicitud = new SolicitudDeUsuario();
            String id;
            using (var db = ApplicationDbContext.Create())
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                string s = User.Identity.GetUserId();
                ApplicationUser user = userManager.FindByIdAsync(s).Result;
                id = user.Id;

            }
            if (solicitud.AceptarSolicitud(idProyecto,id))
            {
                TempData["alerta"] = new Alerta("Solicitud Aceptada Exitosamente.", TipoAlerta.SUCCESS);
            }
            else
            {
                TempData["alerta"] = new Alerta("ERROR al Aceptar Solicitud.", TipoAlerta.ERROR);
                
            }
            return RedirectToAction("ListadoSolicitudUsuario", "SolicitudDeUsuario");
        }

        /**
         * 
         * <author>Diego Iturriaga</author>
         * <summary>
         * Metodo para el funcionamiento del boton rechazar de la interfaz y asi obtener el id del usuario a partir del usuario 
         * logeado y el id del proyecto enviada por la interfaz para poder registra el rechazo de la solicitud
         * </summary>
         * <param name="idProyecto">id del proyecto cuya solicitud fue Rechazada por el usuario logeado en el sistema.</param>
         * <returns>Retorna la misma vista para actualizar la tabla.</returns>
         * 
         **/
        public ActionResult Rechazar(int idProyecto)
        {
            SolicitudDeUsuario solicitud = new SolicitudDeUsuario();
            String id;
            using (var db = ApplicationDbContext.Create())
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                string s = User.Identity.GetUserId();
                ApplicationUser user = userManager.FindByIdAsync(s).Result;
                id = user.Id;

            }
            if (solicitud.RechazarSolicitud(idProyecto, id))
            {
                TempData["alerta"] = new Alerta("Solicitud Rechazada Exitosamente.", TipoAlerta.SUCCESS);
            }
            else
            {
                TempData["alerta"] = new Alerta("ERROR al Rechazar Solicitud.", TipoAlerta.ERROR);
            }
            return RedirectToAction("ListadoSolicitudUsuario", "SolicitudDeUsuario");
        }

    }
}
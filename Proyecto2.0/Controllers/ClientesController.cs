using Proyecto2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Proyecto2._0.Controllers;
using System.Web.Compilation;
using Microsoft.Ajax.Utilities;
using System.Web.UI.WebControls;
using Proyecto2._0.Permisos;

namespace Proyecto2._0.Controllers
{


    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar()
        {
            List<clientes> oClientes = new List<clientes>();

            using (baseEntities db = new baseEntities())
            {
                oClientes = (from p in db.clientes select p).ToList();
            }

            return Json(new { data = oClientes }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Obtener(int idClientes)
        {
            clientes oClientes = new clientes();

            using(baseEntities db = new baseEntities())
            {
                oClientes = (from p in db.clientes where p.Id == idClientes select p).FirstOrDefault();
            }

            return Json(oClientes , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(clientes oClientes)
        {
            bool respuesta = true;

            try
            {
                if(oClientes.Id == 0)
                {
                    using(baseEntities db = new baseEntities())
                    {
                        db.clientes.Add(oClientes);
                        db.SaveChanges();
                    }
                }
                else
                {
                    using (baseEntities db = new baseEntities())
                    {
                        clientes tempCliente = (from p in db.clientes where p.Id == oClientes.Id select p).FirstOrDefault();

                        tempCliente.nombre = oClientes.nombre;
                        tempCliente.apellido = oClientes.apellido;
                        tempCliente.celular = oClientes.celular;
                        tempCliente.email = oClientes.email;

                        db.SaveChanges();
                    }
                }
            }
            catch
            {
                respuesta = false;
            }

            return Json(new {resultado = respuesta}, JsonRequestBehavior.AllowGet);

        }

        

        public JsonResult eliminar(int idClientes)
        {
            bool respuesta = true;

            try
            {
                using( baseEntities db = new baseEntities())
                {
                    clientes oClientes = new clientes();

                    oClientes = (from p in db.clientes.Where(x => x.Id == idClientes) select p).FirstOrDefault();

                    db.clientes.Remove(oClientes);
                    db.SaveChanges();
                }

            }
            catch {  respuesta = false; }

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}
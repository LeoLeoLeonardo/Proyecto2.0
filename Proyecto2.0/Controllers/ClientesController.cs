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
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;


using Proyecto2._0.Controllers;
using Proyecto2._0.Models;
using System.Web.Services.Description;

using System.Data.SqlClient;
using System.Data;
using Proyecto2._0.Permisos;

namespace Proyecto2._0.Controllers
{

    public class AccesoController : Controller
    {

        static string cadena = "Data Source=DESKTOP-4QI6ARF;Initial Catalog=base;Integrated Security=true";


        // GET: acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario oUsuario)
        {
            bool registrado;
            string mensaje;

            if(oUsuario.contraseña == oUsuario.confirmarContra) 
            {
                oUsuario.contraseña = convertirSha256(oUsuario.contraseña);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("Registrar", cn);
                cmd.Parameters.AddWithValue("email", oUsuario.email);
                cmd.Parameters.AddWithValue("contraseña", oUsuario.contraseña);
                cmd.Parameters.Add("registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();


                registrado = Convert.ToBoolean(cmd.Parameters["registrado"].Value);
                mensaje = cmd.Parameters["mensaje"].Value.ToString();
            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(Usuario oUsuario)
        {

            oUsuario.contraseña = convertirSha256(oUsuario.contraseña);

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("Validar", cn);
                cmd.Parameters.AddWithValue("email", oUsuario.email);
                cmd.Parameters.AddWithValue("contraseña", oUsuario.contraseña);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                oUsuario.id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }

            if(oUsuario.id != 0)
            {
                Session["usuario"] = oUsuario;
                return RedirectToAction("Index", "Clientes");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }
            
        }

        public static string convertirSha256(string texto)
        {
            //using System.text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using(SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
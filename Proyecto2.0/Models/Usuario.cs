using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto2._0.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string email { get; set; }
        public string contraseña { get; set; }

        public string confirmarContra { get; set; }

    }
}
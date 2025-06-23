﻿using System.ComponentModel.DataAnnotations;

namespace GestionRepuestosAPI.Modelos
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public ICollection<Repuesto> Repuestos { get; set; }
    }
}

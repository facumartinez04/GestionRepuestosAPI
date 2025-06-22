using System.ComponentModel.DataAnnotations;
using GestionRepuestosAPI.Modelos;
using GestionRepuestosAPI.Modelos.Dtos;
using GestionRepuestosAPI.Repository;
using GestionRepuestosAPI.Repository.Interfaces;

namespace GestionRespuestosAPI.Modelos
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        public string NombreUsuario { get; set; }

        public string Nombre { get; set; }

        public string Password { get; set; }

        public string Rol { get; set; }





    }
}

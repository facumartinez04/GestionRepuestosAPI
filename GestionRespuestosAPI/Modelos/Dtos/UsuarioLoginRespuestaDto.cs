using GestionRepuestosAPI.Modelos;

namespace GestionRespuestosAPI.Modelos.Dtos
{
    public class UsuarioLoginRespuestaDto
    {

        public UsuarioReadDto Usuario { get; set; }

        public string Token { get; set; }


        public ICollection<Permiso> Permisos { get; set; }

        public ICollection<Rol> Roles { get; set; }

        public UsuarioLoginRespuestaDto()
        {
            Permisos = new List<Permiso>();
            Roles = new List<Rol>();
        }

    }
}

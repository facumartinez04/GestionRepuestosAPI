using System.ComponentModel.DataAnnotations;

namespace GestionRepuestosAPI.Modelos
{
    public class UsuarioRol
    {
        public int idUsuario { get; set; }

        public Guid idRol { get; set; }
    }
}

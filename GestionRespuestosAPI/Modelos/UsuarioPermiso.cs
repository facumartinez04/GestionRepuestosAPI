using System.ComponentModel.DataAnnotations;

namespace GestionRepuestosAPI.Modelos
{
    public class UsuarioPermiso
    {
        public int idUsuario { get; set; }
        public Guid idPermiso { get; set; }
    }
}

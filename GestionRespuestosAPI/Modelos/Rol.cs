using System.ComponentModel.DataAnnotations;

namespace GestionRepuestosAPI.Modelos
{
    public class Rol
    {
        [Key]
        public Guid idRol { get; set; }

        public string descripcion { get; set; }
    }
}

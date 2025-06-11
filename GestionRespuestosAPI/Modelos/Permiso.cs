using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GestionRepuestosAPI.Modelos
{
    public class Permiso
    {
        [Key]
        public Guid idPermiso {  get; set; }

        public string nombre { get; set; }

        public string dataPermiso { get; set; }


    }
}

using System.ComponentModel.DataAnnotations;

namespace GestionRepuestosAPI.Modelos
{
    public class RepuestoVehiculo
    {
        public int RepuestoId { get; set; }
        public Repuesto Repuesto { get; set; }

        public int VehiculoId { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace GestionRespuestosAPI.Modelos
{
    public class Vehiculo
    {
        
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }

        public ICollection<RepuestoVehiculo> RepuestosVehiculos { get; set; }
    }
}

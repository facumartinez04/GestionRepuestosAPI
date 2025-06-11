using GestionRespuestosAPI.Modelos;

namespace GestionRepuestosAPI.Repository.Interfaces
{
    public interface IRepuestoVehiculoRepository
    {
        ICollection<RepuestoVehiculo> ObtenerRepuestosVehiculos();
        RepuestoVehiculo ObtenerRepuestoVehiculo(int repuestoId, int vehiculoId);
        bool ExisteRepuestoVehiculo(int repuestoId, int vehiculoId);
        bool CrearRepuestoVehiculo(RepuestoVehiculo repuestoVehiculo);
        bool ActualizarRepuestoVehiculo(RepuestoVehiculo repuestoVehiculo);
        bool EliminarRepuestoVehiculo(int repuestoId, int vehiculoId);
        bool GuardarCambios();
    }
}

using GestionRespuestosAPI.Modelos;

namespace GestionRepuestosAPI.Repository.Interfaces
{
    public interface IVehiculoRepository
    {
        ICollection<Vehiculo> ObtenerVehiculos();
        Vehiculo ObtenerVehiculo(int id);
        bool ExisteVehiculo(int id);
        bool CrearVehiculo(Vehiculo vehiculo);
        bool ActualizarVehiculo(Vehiculo vehiculo);
        bool EliminarVehiculo(int id);
        bool GuardarCambios();
    }
}

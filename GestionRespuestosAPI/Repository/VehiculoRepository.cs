using GestionRepuestosAPI.Repository.Interfaces;
using GestionRespuestosAPI.Data;
using GestionRespuestosAPI.Modelos;

namespace GestionRepuestosAPI.Repository
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly AppDbContext _dbContext;

        public VehiculoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ActualizarVehiculo(Vehiculo vehiculo)
        {
            if (vehiculo == null)
            {
                return false;
            }
            _dbContext.Vehiculos.Update(vehiculo);
            return GuardarCambios();
        }

        public bool CrearVehiculo(Vehiculo vehiculo)
        {
            if (vehiculo == null)
            {
                return false;
            }
            _dbContext.Vehiculos.Add(vehiculo);
            return GuardarCambios();
        }

        public bool EliminarVehiculo(int id)
        {
            var vehiculo = _dbContext.Vehiculos.FirstOrDefault(v => v.Id == id);
            if (vehiculo == null)
            {
                return false;
            }
            _dbContext.Vehiculos.Remove(vehiculo);
            return GuardarCambios();
        }

        public bool ExisteVehiculo(int id)
        {
            return _dbContext.Vehiculos.Any(v => v.Id == id);
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public Vehiculo ObtenerVehiculo(int id)
        {
            return _dbContext.Vehiculos.FirstOrDefault(v => v.Id == id);
        }

        public ICollection<Vehiculo> ObtenerVehiculos()
        {
            return _dbContext.Vehiculos.OrderBy(v => v.Marca).ThenBy(v => v.Modelo).ToList();
        }
    }
}

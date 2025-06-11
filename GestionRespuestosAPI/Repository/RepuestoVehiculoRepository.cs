using GestionRepuestosAPI.Repository.Interfaces;
using GestionRespuestosAPI.Data;
using GestionRespuestosAPI.Modelos;

namespace GestionRepuestosAPI.Repository
{
    public class RepuestoVehiculoRepository : IRepuestoVehiculoRepository
    {
        private readonly AppDbContext _dbContext;

        public RepuestoVehiculoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ActualizarRepuestoVehiculo(RepuestoVehiculo repuestoVehiculo)
        {
            if (repuestoVehiculo == null)
            {
                return false;
            }
            _dbContext.RepuestosVehiculos.Update(repuestoVehiculo);
            return GuardarCambios();
        }

        public bool CrearRepuestoVehiculo(RepuestoVehiculo repuestoVehiculo)
        {
            if (repuestoVehiculo == null)
            {
                return false;
            }
            _dbContext.RepuestosVehiculos.Add(repuestoVehiculo);
            return GuardarCambios();
        }

        public bool EliminarRepuestoVehiculo(int repuestoId, int vehiculoId)
        {
            var entidad = _dbContext.RepuestosVehiculos.FirstOrDefault(rv => rv.RepuestoId == repuestoId && rv.VehiculoId == vehiculoId);
            if (entidad == null)
            {
                return false;
            }
            _dbContext.RepuestosVehiculos.Remove(entidad);
            return GuardarCambios();
        }

        public bool ExisteRepuestoVehiculo(int repuestoId, int vehiculoId)
        {
            return _dbContext.RepuestosVehiculos.Any(rv => rv.RepuestoId == repuestoId && rv.VehiculoId == vehiculoId);
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public RepuestoVehiculo ObtenerRepuestoVehiculo(int repuestoId, int vehiculoId)
        {
            return _dbContext.RepuestosVehiculos.FirstOrDefault(rv => rv.RepuestoId == repuestoId && rv.VehiculoId == vehiculoId);
        }

        public ICollection<RepuestoVehiculo> ObtenerRepuestosVehiculos()
        {
            return _dbContext.RepuestosVehiculos.ToList();
        }
    }
}

using GestionRepuestosAPI.Modelos;
using GestionRepuestosAPI.Repository.Interfaces;
using GestionRespuestosAPI.Data;

namespace GestionRepuestosAPI.Repository
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly AppDbContext _dbContext;

        public PermisoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ActualizarPermiso(Permiso permiso)
        {
            if (permiso == null)
            {
                return false;
            }
            _dbContext.Permisos.Update(permiso);
            return GuardarCambios();
        }

        public bool CrearPermiso(Permiso permiso)
        {
            if (permiso == null)
            {
                return false;
            }
            _dbContext.Permisos.Add(permiso);
            return GuardarCambios();
        }

        public bool EliminarPermiso(Guid id)
        {
            var permiso = _dbContext.Permisos.FirstOrDefault(p => p.idPermiso == id);
            if (permiso == null)
            {
                return false;
            }
            _dbContext.Permisos.Remove(permiso);
            return GuardarCambios();
        }

        public bool ExistePermiso(Guid id)
        {
            return _dbContext.Permisos.Any(p => p.idPermiso == id);
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public Permiso ObtenerPermiso(Guid id)
        {
            return _dbContext.Permisos.FirstOrDefault(p => p.idPermiso == id);
        }

        public ICollection<Permiso> ObtenerPermisos()
        {
            return _dbContext.Permisos.OrderBy(p => p.nombre).ToList();
        }
    }
}

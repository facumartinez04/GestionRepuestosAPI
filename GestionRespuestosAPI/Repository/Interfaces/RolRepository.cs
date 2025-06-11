using GestionRepuestosAPI.Modelos;
using GestionRespuestosAPI.Data;

namespace GestionRepuestosAPI.Repository.Interfaces
{
    public class RolRepository : IRolRepository
    {
        private readonly AppDbContext _dbContext;

        public RolRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ActualizarRol(Rol rol)
        {
            if (rol == null)
            {
                return false;
            }
            _dbContext.Roles.Update(rol);
            return GuardarCambios();
        }

        public bool CrearRol(Rol rol)
        {
            if (rol == null)
            {
                return false;
            }
            _dbContext.Roles.Add(rol);
            return GuardarCambios();
        }

        public bool EliminarRol(Guid id)
        {
            var rol = _dbContext.Roles.FirstOrDefault(r => r.idRol == id);
            if (rol == null)
            {
                return false;
            }
            _dbContext.Roles.Remove(rol);
            return GuardarCambios();
        }

        public bool ExisteRol(Guid id)
        {
            return _dbContext.Roles.Any(r => r.idRol == id);
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public Rol ObtenerRol(Guid id)
        {
            return _dbContext.Roles.FirstOrDefault(r => r.idRol == id);
        }

        public ICollection<Rol> ObtenerRoles()
        {
            return _dbContext.Roles.OrderBy(r => r.descripcion).ToList();
        }
    }
}

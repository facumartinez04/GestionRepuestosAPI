using GestionRepuestosAPI.Modelos;
using GestionRepuestosAPI.Repository.Interfaces;
using GestionRespuestosAPI.Data;

namespace GestionRepuestosAPI.Repository
{
    public class UsuarioPermisoRepository : IUsuarioPermisoRepository
    {
        private readonly AppDbContext _dbContext;

        public UsuarioPermisoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ActualizarUsuarioPermiso(UsuarioPermiso usuarioPermiso)
        {
            if (usuarioPermiso == null)
            {
                return false;
            }
            _dbContext.UsuariosPermisos.Update(usuarioPermiso);
            return GuardarCambios();
        }

        public bool CrearUsuarioPermiso(UsuarioPermiso usuarioPermiso)
        {
            if (usuarioPermiso == null)
            {
                return false;
            }
            _dbContext.UsuariosPermisos.Add(usuarioPermiso);
            return GuardarCambios();
        }

        public bool EliminarUsuarioPermiso(int usuarioId, Guid permisoId)
        {
            var entidad = _dbContext.UsuariosPermisos.FirstOrDefault(up => up.idUsuario == usuarioId && up.idPermiso == permisoId);
            if (entidad == null)
            {
                return false;
            }
            _dbContext.UsuariosPermisos.Remove(entidad);
            return GuardarCambios();
        }

        public bool ExisteUsuarioPermiso(int usuarioId, Guid permisoId)
        {
            return _dbContext.UsuariosPermisos.Any(up => up.idUsuario == usuarioId && up.idPermiso == permisoId);
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public UsuarioPermiso ObtenerUsuarioPermiso(int usuarioId, Guid idUsuarioId)
        {
            return _dbContext.UsuariosPermisos.FirstOrDefault(up => up.idUsuario == usuarioId && up.idPermiso == idUsuarioId);
        }

        public ICollection<UsuarioPermiso> ObtenerUsuariosPermisos()
        {
            return _dbContext.UsuariosPermisos.ToList();


        }

        public ICollection<UsuarioPermiso> ObtenerPermisosPorUsuario(int usuarioId)
        {
            return _dbContext.UsuariosPermisos
                .Where(up => up.idUsuario == usuarioId)
                .ToList();
            
        }

    }

}

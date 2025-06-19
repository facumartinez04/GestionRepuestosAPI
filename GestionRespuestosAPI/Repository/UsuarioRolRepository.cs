using GestionRepuestosAPI.Modelos;
using GestionRepuestosAPI.Repository.Interfaces;
using GestionRespuestosAPI.Data;

namespace GestionRepuestosAPI.Repository
{
    public class UsuarioRolRepository : IUsuarioRolRepository
    {
        private readonly AppDbContext _dbContext;

        public UsuarioRolRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ActualizarUsuarioRol(UsuarioRol usuarioRol)
        {
            if (usuarioRol == null)
            {
                return false;
            }
            _dbContext.UsuariosRoles.Update(usuarioRol);
            return GuardarCambios();
        }

        public bool CrearUsuarioRol(UsuarioRol usuarioRol)
        {
            if (usuarioRol == null)
            {
                return false;
            }
            _dbContext.UsuariosRoles.Add(usuarioRol);
            return GuardarCambios();
        }

        public bool EliminarUsuarioRol(int usuarioId, Guid rolId)
        {
            var entidad = _dbContext.UsuariosRoles.FirstOrDefault(ur => ur.idUsuario == usuarioId && ur.idRol == rolId);
            if (entidad == null)
            {
                return false;
            }
            _dbContext.UsuariosRoles.Remove(entidad);
            return GuardarCambios();
        }

        public bool ExisteUsuarioRol(int usuarioId, Guid rolId)
        {
            return _dbContext.UsuariosRoles.Any(ur => ur.idUsuario == usuarioId && ur.idRol == rolId);
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public ICollection<UsuarioRol> ObtenerRolesPorUsuario(int usuarioId)
        {
            return _dbContext.UsuariosRoles
                .Where(ur => ur.idUsuario == usuarioId)
                .ToList();
        }

        public UsuarioRol ObtenerUsuarioRol(int usuarioId, Guid rolId)
        {
            return _dbContext.UsuariosRoles.FirstOrDefault(ur => ur.idUsuario == usuarioId && ur.idRol == rolId);
        }

        public ICollection<UsuarioRol> ObtenerUsuariosRoles()
        {
            return _dbContext.UsuariosRoles.ToList();
        }
    }
}

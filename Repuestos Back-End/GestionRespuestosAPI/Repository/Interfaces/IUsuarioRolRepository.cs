using GestionRepuestosAPI.Modelos;

namespace GestionRepuestosAPI.Repository.Interfaces
{
    public interface IUsuarioRolRepository
    {
        ICollection<UsuarioRol> ObtenerUsuariosRoles();
        UsuarioRol ObtenerUsuarioRol(int usuarioId, Guid rolId);
        bool ExisteUsuarioRol(int usuarioId, Guid rolId);
        bool CrearUsuarioRol(UsuarioRol usuarioRol);
        bool ActualizarUsuarioRol(UsuarioRol usuarioRol);
        bool EliminarUsuarioRol(int usuarioId, Guid rolId);

        ICollection<UsuarioRol> ObtenerRolesPorUsuario(int usuarioId);
        bool GuardarCambios();
    }
}

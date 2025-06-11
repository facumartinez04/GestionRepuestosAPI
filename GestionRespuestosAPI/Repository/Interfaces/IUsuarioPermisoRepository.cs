using GestionRepuestosAPI.Modelos;

namespace GestionRepuestosAPI.Repository.Interfaces
{
    public interface IUsuarioPermisoRepository
    {
        ICollection<UsuarioPermiso> ObtenerUsuariosPermisos();
        UsuarioPermiso ObtenerUsuarioPermiso(int usuarioId, Guid permisoId);
        bool ExisteUsuarioPermiso(int usuarioId, Guid permisoId);
        bool CrearUsuarioPermiso(UsuarioPermiso usuarioPermiso);
        bool ActualizarUsuarioPermiso(UsuarioPermiso usuarioPermiso);
        bool EliminarUsuarioPermiso(int usuarioId, Guid permisoId);
        bool GuardarCambios();
    }
}

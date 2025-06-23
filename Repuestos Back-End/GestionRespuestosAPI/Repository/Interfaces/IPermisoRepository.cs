using GestionRepuestosAPI.Modelos;

namespace GestionRepuestosAPI.Repository.Interfaces
{
    public interface IPermisoRepository
    {
        ICollection<Permiso> ObtenerPermisos();
        Permiso ObtenerPermiso(Guid id);
        bool ExistePermiso(Guid id);
        bool CrearPermiso(Permiso permiso);
        bool ActualizarPermiso(Permiso permiso);
        bool EliminarPermiso(Guid id);
        bool GuardarCambios();
    }
}

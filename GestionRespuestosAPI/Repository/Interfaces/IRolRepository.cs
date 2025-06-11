using GestionRepuestosAPI.Modelos;

namespace GestionRepuestosAPI.Repository.Interfaces
{
    public interface IRolRepository
    {
        ICollection<Rol> ObtenerRoles();
        Rol ObtenerRol(Guid id);
        bool ExisteRol(Guid id);
        bool CrearRol(Rol rol);
        bool ActualizarRol(Rol rol);
        bool EliminarRol(Guid id);
        bool GuardarCambios();
    }
}

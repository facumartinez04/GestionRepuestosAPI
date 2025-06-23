using GestionRepuestosAPI.Modelos;

namespace GestionRepuestosAPI.Repository.Interfaces
{
    public interface IProveedorRepository
    {
        ICollection<Proveedor> ObtenerProveedores();
        Proveedor ObtenerProveedor(int id);
        bool ExisteProveedor(int id);
        bool CrearProveedor(Proveedor proveedor);
        bool ActualizarProveedor(Proveedor proveedor);
        bool EliminarProveedor(int id);
        bool GuardarCambios();
    }
}

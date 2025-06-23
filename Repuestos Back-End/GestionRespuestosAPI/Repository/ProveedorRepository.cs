using GestionRepuestosAPI.Modelos;
using GestionRepuestosAPI.Repository.Interfaces;
using GestionRespuestosAPI.Data;

namespace GestionRepuestosAPI.Repository
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly AppDbContext _dbContext;

        public ProveedorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ActualizarProveedor(Proveedor proveedor)
        {
            if (proveedor == null)
            {
                return false;
            }
            _dbContext.Update(proveedor);
            return GuardarCambios();
        }

        public bool CrearProveedor(Proveedor proveedor)
        {
            if (proveedor == null)
            {
                return false;
            }
            _dbContext.Proveedores.Add(proveedor);
            return GuardarCambios();
        }

        public bool EliminarProveedor(int id)
        {
            var proveedor = _dbContext.Proveedores.FirstOrDefault(p => p.Id == id);
            if (proveedor == null)
            {
                return false;
            }
            _dbContext.Proveedores.Remove(proveedor);
            return GuardarCambios();
        }

        public bool ExisteProveedor(int id)
        {
            return _dbContext.Proveedores.Any(p => p.Id == id);
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public Proveedor ObtenerProveedor(int id)
        {
            return _dbContext.Proveedores.FirstOrDefault(p => p.Id == id);
        }

        public ICollection<Proveedor> ObtenerProveedores()
        {
            return _dbContext.Proveedores.OrderBy(p => p.Nombre).ToList();
        }
    }
}

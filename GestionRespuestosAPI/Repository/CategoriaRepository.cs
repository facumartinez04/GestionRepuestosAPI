using GestionRespuestosAPI.Data;
using GestionRespuestosAPI.Modelos;
using GestionRespuestosAPI.Repository.Interfaces;

namespace GestionRespuestosAPI.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {

        private readonly AppDbContext _dbContext;

        public CategoriaRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool ActualizarCategoria(Categoria categoria)
        {
            if (categoria == null)
            {
                return false;
            }
            _dbContext.Categorias.Update(categoria);
            return GuardarCambios();
        }

        public bool CrearCategoria(Categoria categoria)
        {
            if (categoria == null)
            {
                return false;
            }
            _dbContext.Categorias.Add(categoria);
            return GuardarCambios();
        }

        public bool EliminarCategoria(int id)
        {
            var categoria = _dbContext.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                return false;
            }
            _dbContext.Categorias.Remove(categoria);
            return GuardarCambios();
        }

        public bool ExisteCategoria(int id)
        {
            return _dbContext.Categorias.Any(c => c.Id == id);
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0 ? true : false; 
        }

        public Categoria ObtenerCategoria(int id)
        {
            return _dbContext.Categorias.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Categoria> ObtenerCategorias()
        {
            return _dbContext.Categorias.OrderBy(c => c.Nombre).ToList();

        }
    }
}

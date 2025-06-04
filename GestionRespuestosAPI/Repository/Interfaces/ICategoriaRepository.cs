using GestionRespuestosAPI.Modelos;

namespace GestionRespuestosAPI.Repository.Interfaces
{
    public interface ICategoriaRepository
    {
        ICollection<Categoria> ObtenerCategorias();
        Categoria ObtenerCategoria(int id);
        bool ExisteCategoria(int id);
        bool CrearCategoria(Categoria categoria);
        bool ActualizarCategoria(Categoria categoria);
        bool EliminarCategoria(int id);

        bool GuardarCambios();
    }

}

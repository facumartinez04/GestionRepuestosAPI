
using GestionRespuestosAPI.Modelos;
using GestionRespuestosAPI.Modelos.Dtos;

namespace GestionRespuestosAPI.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario>  GetUsuarios();

        Usuario GetUsuario(int id);

        bool ExisteUsuario(string usuario);

        Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
        Task<Usuario> Register(UsuarioRegisterDto usuarioRegisterDto);

    }

}

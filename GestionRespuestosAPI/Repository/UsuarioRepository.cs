using GestionRespuestosAPI.Data;
using GestionRespuestosAPI.Modelos;
using GestionRespuestosAPI.Modelos.Dtos;
using GestionRespuestosAPI.Repository.Interfaces;
using System.Security.Cryptography;

namespace GestionRespuestosAPI.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _dbContext;

        public UsuarioRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ExisteUsuario(string usuario)
        {
            return _dbContext.Usuarios.Any(u => u.Nombre.ToLower().Trim() == usuario.ToLower().Trim());
        
        }

        public Usuario GetUsuario(int id)
        {
            return _dbContext.Usuarios.FirstOrDefault(u => u.Id == id);
        }





        public ICollection<Usuario> GetUsuarios()
        {
            return _dbContext.Usuarios.OrderBy(u => u.Nombre).ToList();
        }

        public Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> Register(UsuarioRegisterDto usuarioRegisterDto)
        {
            var passwordEncryptado = HashPassword(usuarioRegisterDto.Password);

            Usuario usuario = new Usuario
            {
                Nombre = usuarioRegisterDto.Nombre,

                Password = passwordEncryptado,
                NombreUsuario = usuarioRegisterDto.NombreUsuario,
                Rol = usuarioRegisterDto.Rol



            };

            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            usuario.Password = passwordEncryptado;

            return usuario;

         



        }


        public static string HashPassword(string password)
        {
            return MD5.HashData(System.Text.Encoding.UTF8.GetBytes(password))
                .Aggregate(string.Empty, (current, b) => current + b.ToString("x2"));
        }
    }
}



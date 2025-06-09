using GestionRespuestosAPI.Data;
using GestionRespuestosAPI.Modelos;
using GestionRespuestosAPI.Modelos.Dtos;
using GestionRespuestosAPI.Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace GestionRespuestosAPI.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _dbContext;

        private string keySecret; 

        public UsuarioRepository(AppDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;

            keySecret = config["JwtSettings:Key"];



        }

        public bool ExisteUsuario(string usuario)
        {
            return _dbContext.Usuarios.Any(u => u.NombreUsuario.ToLower().Trim() == usuario.ToLower().Trim());
        

        }

        public Usuario GetUsuario(int id)
        {
            return _dbContext.Usuarios.FirstOrDefault(u => u.Id == id);
        }





        public ICollection<Usuario> GetUsuarios()
        {
            return _dbContext.Usuarios.OrderBy(u => u.Nombre).ToList();
        }

        public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
          
            var passwordEncryptado = HashPassword(usuarioLoginDto.Password);

            var usuario = _dbContext.Usuarios.FirstOrDefault(u => u.NombreUsuario.ToLower() == usuarioLoginDto.NombreUsuario.ToLower() && u.Password == passwordEncryptado);

            if (usuario == null)
            {
                return null;
            }

            var manejaToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(keySecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim("nombreUsuario", usuario.NombreUsuario),
                    new System.Security.Claims.Claim("rol", usuario.Rol)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejaToken.CreateToken(tokenDescriptor);

            UsuarioLoginRespuestaDto usuarioLoginRespuestaDto = new UsuarioLoginRespuestaDto
            {
                Usuario = usuario,
                Rol = usuario.Rol,
                Token = manejaToken.WriteToken(token)
            };

            return usuarioLoginRespuestaDto;


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



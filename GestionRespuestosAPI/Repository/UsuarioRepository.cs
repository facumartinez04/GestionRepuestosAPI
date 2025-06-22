using GestionRepuestosAPI.Modelos;
using GestionRepuestosAPI.Modelos.Dtos;
using GestionRespuestosAPI.Data;
using GestionRespuestosAPI.Modelos;
using GestionRespuestosAPI.Modelos.Dtos;
using GestionRespuestosAPI.Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GestionRespuestosAPI.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _dbContext;

        private string keySecret;

        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;

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
            var usuario = _dbContext.Usuarios
                .FirstOrDefault(u => u.NombreUsuario.ToLower() == usuarioLoginDto.NombreUsuario.ToLower());

            if (usuario == null || !VerifyPassword(usuarioLoginDto.Password, usuario.Password))
            {
                return null;
            }

            var usuarioReadDto = new UsuarioReadDto
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Nombre = usuario.Nombre
            };

            var permisosUsuario = (
                from up in _dbContext.UsuariosPermisos
                join p in _dbContext.Permisos on up.idPermiso equals p.idPermiso
                where up.idUsuario == usuario.Id
                select new { p.idPermiso, p.nombre, p.dataPermiso }
            ).ToList();

            var rolesUsuario = (
                from ur in _dbContext.UsuariosRoles
                join r in _dbContext.Roles on ur.idRol equals r.idRol
                where ur.idUsuario == usuario.Id
                select new { r.idRol, r.descripcion }
            ).ToList();

            var claims = new List<Claim>
    {
        new Claim("nombreUsuario", usuario.NombreUsuario),
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
    };

            foreach (var permiso in permisosUsuario)
            {
                claims.Add(new Claim("Permiso", permiso.dataPermiso));
            }

            foreach (var rol in rolesUsuario)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol.descripcion));
            }

            var manejaToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(keySecret);
            

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejaToken.CreateToken(tokenDescriptor);


            return new UsuarioLoginRespuestaDto
            {
                Usuario = usuarioReadDto,
                Permisos = permisosUsuario.Select(p => new Permiso
                {
                    idPermiso = p.idPermiso,
                    nombre = p.nombre,
                    dataPermiso = p.dataPermiso
                }).ToList(),
                Roles = rolesUsuario.Select(r => new Rol
                {
                    idRol = r.idRol,
                    descripcion = r.descripcion
                }).ToList(),
                Token = manejaToken.WriteToken(token)
            };
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
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            byte[] key = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                KeySize);

            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(key)}";
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split(':', 2);
            if (parts.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] key = Convert.FromBase64String(parts[1]);

            byte[] keyToCheck = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                KeySize);

            return CryptographicOperations.FixedTimeEquals(keyToCheck, key);
        }

        public Task<UsuarioLoginRespuestaDto> RefreshToken(RefreshTokenDto token)
        {
            throw new NotImplementedException();
        }

        private static string GenerateRefreshToken()
        {

            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}



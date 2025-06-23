using AutoMapper;
using GestionRepuestosAPI.Modelos;
using GestionRespuestosAPI.Modelos;
using GestionRespuestosAPI.Modelos.Dtos;
using GestionRespuestosAPI.Repository;
using GestionRespuestosAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestionRepuestosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        protected RespuestaAPI _respuestaAPI;

        public UsuariosController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _respuestaAPI = new RespuestaAPI();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetUsuarios()
        {

            var categorias = _usuarioRepository.GetUsuarios();
            if (categorias == null || !categorias.Any())
            {
                return NotFound("No se encontraron usuarios.");
            }
            var categoriasDto = _mapper.Map<IEnumerable<UsuarioReadDto>>(categorias);
            return Ok(categoriasDto);

        }


        [HttpGet("{id:int}", Name = "ObtenerUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetUsuario(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID del usuario debe ser mayor que cero.");
            }
            var usuario = _usuarioRepository.GetUsuario(id);
            if (usuario == null)
            {
                return NotFound($"No se encontró un usuario con el ID {id}.");
            }
            var usuarioDto = _mapper.Map<UsuarioReadDto>(usuario);
            return Ok(usuarioDto);
        }


        [HttpPost("registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UsuarioRegisterDto usuarioRegisterDto)
        {

            if (usuarioRegisterDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_usuarioRepository.ExisteUsuario(usuarioRegisterDto.NombreUsuario))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "El usuario ya existe.";
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuestaAPI);

            }
            var usuario = _mapper.Map<Usuario>(usuarioRegisterDto);
            var nuevoUsuario = await _usuarioRepository.Register(usuarioRegisterDto);
            if (nuevoUsuario == null)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "Error al registrar el usuario.";
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuestaAPI);
            }

            _respuestaAPI.Result = _mapper.Map<UsuarioReadDto>(nuevoUsuario);

            _respuestaAPI.StatusCode = HttpStatusCode.Created;
            _respuestaAPI.Message = "Usuario registrado correctamente.";
            _respuestaAPI.Success = true;
            return CreatedAtRoute("ObtenerUsuario", new { id = nuevoUsuario.Id }, _respuestaAPI);
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            if (usuarioLoginDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _usuarioRepository.Login(usuarioLoginDto);
            if (respuesta == null)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "Usuario o contraseña incorrectos.";
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuestaAPI);
            }
            _respuestaAPI.Result = respuesta;
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.Message = "Inicio de sesión exitoso.";
            _respuestaAPI.Success = true;
            return Ok(_respuestaAPI);

        }


    }
}

using AutoMapper;
using GestionRepuestosAPI.Modelos;
using GestionRespuestosAPI.Modelos.Dtos;
using GestionRespuestosAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionRespuestosAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetCategorias()
        {

            var categorias = _categoriaRepository.ObtenerCategorias();
            if (categorias == null || !categorias.Any())
            {
                return NotFound("No se encontraron categorías.");
            }
            var categoriasDto = _mapper.Map<IEnumerable<CategoriaReadDto>>(categorias);
            return Ok(categoriasDto);

        }


        [AllowAnonymous]
        [HttpGet("{id:int}", Name = "ObtenerCategoria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetCategoria(int id)
        {
            if (!_categoriaRepository.ExisteCategoria(id))
            {
                return NotFound($"No se encontró la categoría con ID {id}.");
            }
            var categoria = _categoriaRepository.ObtenerCategoria(id);
            var categoriaDto = _mapper.Map<CategoriaReadDto>(categoria);
            return Ok(categoriaDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public IActionResult CrearCategoria([FromBody] CategoriaCreateDto categoriaCreateDto)
        {
            if (categoriaCreateDto == null)
            {
                return BadRequest("Los datos de la categoría son inválidos.");
            }
            var categoria = _mapper.Map<Categoria>(categoriaCreateDto);
            if (!_categoriaRepository.CrearCategoria(categoria))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la categoría.");
            }
            var categoriaReadDto = _mapper.Map<CategoriaReadDto>(categoria);
            return CreatedAtRoute("ObtenerCategoria", new { id = categoriaReadDto.Id }, categoriaReadDto);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult ActualizarCategoria([FromBody] CategoriaReadDto categoriaUpdateDto)
        {
            if (categoriaUpdateDto == null || categoriaUpdateDto.Id <= 0)
            {
                return BadRequest("Los datos de la categoría son inválidos.");
            }
            if (!_categoriaRepository.ExisteCategoria(categoriaUpdateDto.Id))
            {
                return NotFound($"No se encontró la categoría con ID {categoriaUpdateDto.Id}.");
            }
            var categoria = _mapper.Map<Categoria>(categoriaUpdateDto);
            if (!_categoriaRepository.ActualizarCategoria(categoria))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar la categoría.");
            }
            return Ok("Categoría actualizada correctamente.");

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult EliminarCategoria(int id)
        {
            if (!_categoriaRepository.ExisteCategoria(id))
            {
                return NotFound($"No se encontró la categoría con ID {id}.");
            }
            if (!_categoriaRepository.EliminarCategoria(id))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar la categoría.");
            }
            return Ok("Categoría eliminada correctamente.");

        }

        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ExisteCategoria(int id)
        {
            var existe = _categoriaRepository.ExisteCategoria(id);
            if (!existe)
            {
                return NotFound($"No se encontró la categoría con ID {id}.");
            }
            return Ok(new { existe });
        }





    }
}


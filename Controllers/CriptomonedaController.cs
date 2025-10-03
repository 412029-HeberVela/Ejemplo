using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParcialWebApi.DTOs;
using ParcialWebApi.Models;
using ParcialWebApi.Repositories;

namespace ParcialWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriptomonedaController : Controller
    {
        private readonly ICriptomonedaRepository _repository;
        public CriptomonedaController(ICriptomonedaRepository repository)
        {
            _repository = repository;
        }

        //GET: GetAll
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var criptomoneda = _repository.GetAll();
                if (criptomoneda == null || criptomoneda.Count == 0)
                    return NotFound("No se encontraron datos.");
                else
                    return Ok(criptomoneda);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno.");
            }
        }

        //GET: GetBy
        [HttpGet("by-category")]
        public IActionResult GetBy(string cat)
        {
            try
            {
                var criptomoneda = _repository.GetBy(cat);
                if (criptomoneda == null || criptomoneda.Count == 0)
                    return NotFound("No se encontró ese tipo de criptomoneda.");
                else
                    return Ok(criptomoneda);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno.");
            }
        }

        //POST: create
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateRequest request)
        {
            if (request == null)
                return StatusCode(400, new { message = "Datos inválidos." });

            var nuevaCripto = new Criptomoneda
            {
                Nombre = request.Nombre,
                Simbolo = request.Simbolo,
                ValorActual = request.ValorActual,
                UltimaActualizacion = DateTime.Now,
                Categoria = request.Categoria,
                Estado = "H" // Alta lógica
            };

            var result = _repository.Create(nuevaCripto);

            if (result)
                return StatusCode(200, new { message = $"Se creó la criptomoneda {request.Nombre} ({request.Simbolo})." });

            return StatusCode(500, new { message = "No se pudo crear la criptomoneda." });
        }

        //PUT: Update
        [HttpPut("update/{simbolo}")]
        public IActionResult Update(string simbolo, [FromBody] CotizacionRequest request)
        {
            var result = _repository.Update(simbolo, request.ValorActual, request.UltimaActualizacion);

            if (result)
                return Ok(new { message = $"Se actualizó {simbolo} con valor {request.ValorActual}" });

            return StatusCode(400, new { message = $"No se pudo actualizar {simbolo}. La fecha no puede ser anterior a 1 día o no existe la cripto." });
        }


        //DELETE: baja lógica
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _repository.Delete(id);

            if (result)
                return Ok(new { message = $"Criptomoneda {id} dada de baja." });

            return NotFound(new { message = $"No se encontró la criptomoneda con id {id} o ya estaba dada de baja." });
        }

    }
}

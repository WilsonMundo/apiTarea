using HelloApi.Models.DTOs;
using HelloApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloApi.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _service;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(IItemService service, ILogger<ItemsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var items = await _service.GetAllAsync();
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo todos los items");
                return Problem("Ocurrió un error al obtener los items.", statusCode: 500);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var item = await _service.GetByIdAsync(id);
                return item is not null ? Ok(item) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo el item {Id}", id);
                return Problem($"Ocurrió un error al obtener el item {id}.", statusCode: 500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemCreateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            try
            {
                var created = await _service.CreateAsync(dto);
                return Ok(created);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Petición inválida al crear item");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando un item");
                return Problem("Ocurrió un error al crear el item.", statusCode: 500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ItemUpdateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            try
            {
                var updated = await _service.UpdateAsync(dto);
                return updated is not null ? Ok(updated) : NotFound();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Petición inválida al actualizar item {Id}", dto.Id);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando el item {Id}", dto.Id);
                return Problem($"Ocurrió un error al actualizar el item {dto.Id}.", statusCode: 500);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ok = await _service.DeleteAsync(id);
                return ok ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando el item {Id}", id);
                return Problem($"Ocurrió un error al eliminar el item {id}.", statusCode: 500);
            }
        }
    }
}

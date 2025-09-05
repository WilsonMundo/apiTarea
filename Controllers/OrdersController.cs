using HelloApi.Models.DTOs;
using HelloApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloApi.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController :ControllerBase
    {
        private readonly IOrderService _service;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService service, ILogger<OrdersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var orders = await _service.GetAllAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo todas las órdenes");
                return Problem("Ocurrió un error al obtener las órdenes.", statusCode: 500);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var order = await _service.GetByIdAsync(id);
                return order is not null ? Ok(order) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo la orden {Id}", id);
                return Problem($"Ocurrió un error al obtener la orden {id}.", statusCode: 500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            try
            {
                var created = await _service.CreateAsync(dto);
                return Ok(created);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Petición inválida al crear orden");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando una orden");
                return Problem("Ocurrió un error al crear la orden.", statusCode: 500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OrderUpdateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            try
            {
                var updated = await _service.UpdateAsync(dto);
                return updated is not null ? Ok(updated) : NotFound();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Petición inválida al actualizar orden {Id}", dto.Id);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando la orden {Id}", dto.Id);
                return Problem($"Ocurrió un error al actualizar la orden {dto.Id}.", statusCode: 500);
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
                _logger.LogError(ex, "Error eliminando la orden {Id}", id);
                return Problem($"Ocurrió un error al eliminar la orden {id}.", statusCode: 500);
            }
        }
    }
}

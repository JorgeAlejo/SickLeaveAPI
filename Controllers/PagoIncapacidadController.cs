using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PagoIncapacidadController : ControllerBase{
    private readonly PagoIncapacidadService _pagoService;

    public PagoIncapacidadController(PagoIncapacidadService pagoService){
        _pagoService = pagoService;
    }

    // Crear un nuevo pago de incapacidad
    [HttpPost]
    public async Task<IActionResult> CreatePagoIncapacidad([FromBody] PagoIncapacidad pago){
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _pagoService.CreatePagoIncapacidad(pago);
        if (!result) return BadRequest("Error al crear el pago. Verifique que los datos sean correctos.");

        return CreatedAtAction(nameof(GetPagoById), new { id = pago.IdPago }, pago);
    }

    // Obtener todos los pagos de incapacidades
    [HttpGet]
    public async Task<IActionResult> GetAllPagos(){
        var pagos = await _pagoService.GetAllPagos();
        return Ok(pagos);
    }

    // Obtener un pago por su ID
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPagoById(Guid id){
        var pago = await _pagoService.GetPagoById(id);
        if (pago == null) return NotFound("Pago no encontrado.");

        return Ok(pago);
    }

    // Actualizar un pago de incapacidad
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePagoIncapacidad(Guid id, [FromBody] PagoIncapacidad updatedPago){
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _pagoService.UpdatePagoIncapacidad(id, updatedPago);
        if (!result) return NotFound("No se pudo actualizar el pago. Verifique que el ID sea correcto.");

        return NoContent();
    }

    // Cambiar el estado de un pago a 'Pagado'
    [HttpPatch("{id:guid}/marcar-pagado")]
    public async Task<IActionResult> MarkAsPaid(Guid id){
        var existingPago = await _pagoService.GetPagoById(id);
        if (existingPago == null) return NotFound("Pago no encontrado.");

        existingPago.Estado = EstadoPago.Pagado;
        var result = await _pagoService.UpdatePagoIncapacidad(id, existingPago);
        if (!result) return BadRequest("Error al actualizar el estado del pago.");

        return NoContent();
    }

    // Eliminar un pago de incapacidad (borrado lógico)
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePagoIncapacidad(Guid id){
        var result = await _pagoService.DeletePagoIncapacidad(id);
        if (!result) return NotFound("No se pudo eliminar el pago. Verifique que el ID sea correcto.");

        return NoContent();
    }
}

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CobroJuridicoController : ControllerBase{
    private readonly CobroJuridicoService _cobroService;

    public CobroJuridicoController(CobroJuridicoService cobroService){
        _cobroService = cobroService;
    }

    // Crear un nuevo cobro de incapacidad
    [HttpPost]
    public async Task<IActionResult> CreateCobro([FromBody] CobroJuridico cobro){
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _cobroService.CreateCobro(cobro);
        if (!result) return BadRequest("Error al crear el cobro. Verifique que los datos sean correctos.");

        return CreatedAtAction(nameof(GetCobroById), new { id = cobro.IdCobro }, cobro);
    }

    // Obtener todos los cobros de incapacidades
    [HttpGet]
    public async Task<IActionResult> GetAllCobros(){
        var cobros = await _cobroService.GetAllCobros();
        return Ok(cobros);
    }

    // Obtener un cobro por su ID
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCobroById(Guid id){
        var cobro = await _cobroService.GetCobroById(id);
        if (cobro == null) return NotFound("cobro no encontrado.");

        return Ok(cobro);
    }

    // Actualizar un cobro de incapacidad
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCobro(Guid id, [FromBody] CobroJuridico updatedCobro){
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _cobroService.UpdateCobro(id, updatedCobro);
        if (!result) return NotFound("No se pudo actualizar el cobro. Verifique que el ID sea correcto.");

        return NoContent();
    }

    // Eliminar un cobro de incapacidad (borrado lógico)
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCobro(Guid id){
        var result = await _cobroService.DeleteCobro(id);
        if (!result) return NotFound("No se pudo eliminar el cobro. Verifique que el ID sea correcto.");

        return NoContent();
    }
}
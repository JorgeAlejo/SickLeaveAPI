using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class IncapacidadController : ControllerBase{
    private readonly IncapacidadService _incapacidadService;

    public IncapacidadController(IncapacidadService incapacidadService){
        _incapacidadService = incapacidadService;
    }

    // Crear una nueva incapacidad
    [HttpPost]
    public async Task<IActionResult> CreateIncapacidad([FromBody] Incapacidad nuevaIncapacidad){
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var result = await _incapacidadService.CreateIncapacidad(nuevaIncapacidad);
        if (!result) return BadRequest(new { message = "Error al crear la incapacidad. Verifique los datos ingresados." });
        
        return Ok(new { message = "Incapacidad creada exitosamente." });
    }

    // Obtener todas las incapacidades
    [HttpGet]
    public async Task<IActionResult> GetAllIncapacidades(){
        var incapacidades = await _incapacidadService.GetAllIncapacidades();
        return Ok(incapacidades);
    }

    // Obtener una incapacidad por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetIncapacidadById(Guid id){
        var incapacidad = await _incapacidadService.GetIncapacidadById(id);
        if (incapacidad == null) return NotFound(new { message = "Incapacidad no encontrada." });
        
        return Ok(incapacidad);
    }

    // Obtener una incapacidad por cedula COlaborador
    [HttpGet("Colaborador/{cedula}")]
    public async Task<IActionResult> GetIncapacidadByCedulaColaborador(long cedula){
        var incapacidad = await _incapacidadService.GetIncapacidadByCedulaCoraborador(cedula);
        if (incapacidad == null) return NotFound(new { message = "Incapacidad no encontrada." });
        
        return Ok(incapacidad);
    }

    // Obtener una incapacidad por cedula RH
    [HttpGet("RH/{cedula}")]
    public async Task<IActionResult> GetIncapacidadByCedulaRH(long cedula){
        var incapacidad = await _incapacidadService.GetIncapacidadByCedulaRH(cedula);
        if (incapacidad == null) return NotFound(new { message = "Incapacidad no encontrada." });
        
        return Ok(incapacidad);
    }

    // Actualizar una incapacidad
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIncapacidad(Guid id, [FromBody] Incapacidad updatedIncapacidad){
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var result = await _incapacidadService.UpdateIncapacidad(id, updatedIncapacidad);
        if (!result) return NotFound(new { message = "Incapacidad no encontrada." });
        
        return Ok(new { message = "Incapacidad actualizada exitosamente." });
    }

    // Cambiar el estado de una incapacidad
    [HttpPatch("{id}/estado")]
    public async Task<IActionResult> UpdateEstadoIncapacidad(Guid id, [FromQuery] EstadoIncapacidad nuevoEstado){
        var result = await _incapacidadService.UpdateEstadoIncapacidad(id, nuevoEstado);
        if (!result) return NotFound(new { message = "Incapacidad no encontrada." });
        
        return Ok(new { message = "Estado de la incapacidad actualizado exitosamente." });
    }

    // Eliminar una incapacidad (borrado lógico)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIncapacidad(Guid id){
        var result = await _incapacidadService.DeleteIncapacidad(id);
        if (!result) return NotFound(new { message = "Incapacidad no encontrada." });
        
        return Ok(new { message = "Incapacidad eliminada exitosamente." });
    }
}

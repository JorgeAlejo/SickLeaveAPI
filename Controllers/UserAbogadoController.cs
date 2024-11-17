using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserAbogadoController : ControllerBase{
    private readonly UserAbogadoService _userAbogadoService;

    public UserAbogadoController(UserAbogadoService userAbogadoService){
        _userAbogadoService = userAbogadoService;
    }

    // Crear un nuevo Abogado
    [HttpPost]
    public async Task<IActionResult> CreateUserAbogado([FromBody] UserAbogado abogado){
        if (!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        var result = await _userAbogadoService.CreateUserAbogado(abogado);
        if (!result){
            return Conflict(new { message = "Ya existe un Abogado con la misma cedula." });
        }

        return CreatedAtAction(nameof(GetByCedula), new { cedula = abogado.Cedula }, abogado);
    }

    // Obtener todos los Abogadoes activos
    [HttpGet]
    public async Task<IActionResult> GetAllActiveAbogadoes(){
        var Abogadoes = await _userAbogadoService.GetAllActiveAbogadoes();
        return Ok(Abogadoes);
    }

    // Obtener Abogado por cédula
    [HttpGet("{cedula}")]
    public async Task<IActionResult> GetByCedula(long cedula){
        var abogado = await _userAbogadoService.GetByCedula(cedula);
        if (abogado == null)
        {
            return NotFound(new { message = "No se encuentra el abogado o esta inactivo." });
        }

        return Ok(abogado);
    }

    // Actualizar Abogado
    [HttpPut("{cedula}")]
    public async Task<IActionResult> UpdateUserAbogado(long cedula, [FromBody] UserAbogado updatedAbogado){
        if (!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        var result = await _userAbogadoService.UpdateUserAbogado(cedula, updatedAbogado);
        if (!result) return NotFound(new { message = "No se encuentra el Abogado o esta inactivo." });

        return Ok(new { message = "Abogado actualizado correctamente." });
    }

    // Eliminar Abogado (borrado lógico)
    [HttpDelete("{cedula}")]
    public async Task<IActionResult> DeleteUserAbogado(long cedula){
        var result = await _userAbogadoService.DeleteUserAbogado(cedula);
        if (!result) return NotFound(new { message = "No se encuentra el Abogado o esta inactivo." });
        
        return Ok(new { message = "Se elimino el Abogado correctamente." });
    }
}
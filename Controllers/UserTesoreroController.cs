using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserTesoreroController : ControllerBase{
    private readonly UserTesoreroService _userTesoreroService;

    public UserTesoreroController(UserTesoreroService userTesoreroService){
        _userTesoreroService = userTesoreroService;
    }

    // Crear un nuevo Tesorero
    [HttpPost]
    public async Task<IActionResult> CreateUserTesorero([FromBody] UserTesorero tesorero){
        if (!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        var result = await _userTesoreroService.CreateUserTesorero(tesorero);
        if (!result){
            return Conflict(new { message = "Ya existe un Tesorero con la misma cedula." });
        }

        return CreatedAtAction(nameof(GetByCedula), new { cedula = tesorero.Cedula }, tesorero);
    }

    // Obtener todos los Tesoreroes activos
    [HttpGet]
    public async Task<IActionResult> GetAllActiveTesoreroes(){
        var Tesoreroes = await _userTesoreroService.GetAllActiveTesoreroes();
        return Ok(Tesoreroes);
    }

    // Obtener Tesorero por cédula
    [HttpGet("{cedula}")]
    public async Task<IActionResult> GetByCedula(long cedula){
        var tesorero = await _userTesoreroService.GetByCedula(cedula);
        if (tesorero == null)
        {
            return NotFound(new { message = "No se encuentra el tesorero o esta inactivo." });
        }

        return Ok(tesorero);
    }

    // Actualizar Tesorero
    [HttpPut("{cedula}")]
    public async Task<IActionResult> UpdateUserTesorero(long cedula, [FromBody] UserTesorero updatedTesorero){
        if (!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        var result = await _userTesoreroService.UpdateUserTesorero(cedula, updatedTesorero);
        if (!result) return NotFound(new { message = "No se encuentra el Tesorero o esta inactivo." });

        return Ok(new { message = "Tesorero actualizado correctamente." });
    }

    // Eliminar Tesorero (borrado lógico)
    [HttpDelete("{cedula}")]
    public async Task<IActionResult> DeleteUserTesorero(long cedula){
        var result = await _userTesoreroService.DeleteUserTesorero(cedula);
        if (!result) return NotFound(new { message = "No se encuentra el Tesorero o esta inactivo." });
        
        return Ok(new { message = "Se elimino el Tesorero correctamente." });
    }
}
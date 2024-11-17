using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserRHController : ControllerBase{
    private readonly UserRHService _userRHService;

    public UserRHController(UserRHService userRHService){
        _userRHService = userRHService;
    }

    // Crear un nuevo RH
    [HttpPost]
    public async Task<IActionResult> CreateUserRH([FromBody] UserRH rH){
        if (!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        var result = await _userRHService.CreateUserRH(rH);
        if (!result){
            return Conflict(new { message = "Ya existe un RH con la misma cedula." });
        }

        return CreatedAtAction(nameof(GetByCedula), new { cedula = rH.Cedula }, rH);
    }

    // Obtener todos los RHes activos
    [HttpGet]
    public async Task<IActionResult> GetAllActiveRHes(){
        var RHes = await _userRHService.GetAllActiveRHes();
        return Ok(RHes);
    }

    // Obtener RH por cédula
    [HttpGet("{cedula}")]
    public async Task<IActionResult> GetByCedula(long cedula){
        var rH = await _userRHService.GetByCedula(cedula);
        if (rH == null)
        {
            return NotFound(new { message = "No se encuentra el RH o esta inactivo." });
        }

        return Ok(rH);
    }

    // Actualizar RH
    [HttpPut("{cedula}")]
    public async Task<IActionResult> UpdateUserRH(long cedula, [FromBody] UserRH updatedRH){
        if (!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        var result = await _userRHService.UpdateUserRH(cedula, updatedRH);
        if (!result) return NotFound(new { message = "No se encuentra el RH o esta inactivo." });

        return Ok(new { message = "RH actualizado correctamente." });
    }

    // Eliminar RH (borrado lógico)
    [HttpDelete("{cedula}")]
    public async Task<IActionResult> DeleteUserRH(long cedula){
        var result = await _userRHService.DeleteUserRH(cedula);
        if (!result) return NotFound(new { message = "No se encuentra el RH o esta inactivo." });
        
        return Ok(new { message = "Se elimino el RH correctamente." });
    }
}
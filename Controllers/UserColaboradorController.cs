using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserColaboradorController : ControllerBase{
    private readonly UserColaboradorService _userColaboradorService;

    public UserColaboradorController(UserColaboradorService userColaboradorService){
        _userColaboradorService = userColaboradorService;
    }

    // Crear un nuevo colaborador
    [HttpPost]
    public async Task<IActionResult> CreateUserColaborador([FromBody] UserColaborador colaborador){
        if (!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        var result = await _userColaboradorService.CreateUserColaborador(colaborador);
        if (!result){
            return Conflict(new { message = "Ya existe un colaborador con la misma cedula." });
        }

        return CreatedAtAction(nameof(GetByCedula), new { cedula = colaborador.Cedula }, colaborador);
    }

    // Obtener todos los colaboradores activos
    [HttpGet]
    public async Task<IActionResult> GetAllActiveColaboradores(){
        var colaboradores = await _userColaboradorService.GetAllActiveColaboradores();
        return Ok(colaboradores);
    }

    // Obtener colaborador por cédula
    [HttpGet("{cedula}")]
    public async Task<IActionResult> GetByCedula(long cedula){
        var colaborador = await _userColaboradorService.GetByCedula(cedula);
        if (colaborador == null)
        {
            return NotFound(new { message = "No se encuentra el colaborador o esta inactivo." });
        }

        return Ok(colaborador);
    }

    // Actualizar colaborador
    [HttpPut("{cedula}")]
    public async Task<IActionResult> UpdateUserColaborador(long cedula, [FromBody] UserColaborador updatedColaborador){
        if (!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        var result = await _userColaboradorService.UpdateUserColaborador(cedula, updatedColaborador);
        if (!result) return NotFound(new { message = "No se encuentra el colaborador o esta inactivo." });

        return Ok(new { message = "Colaborador actualizado correctamente." });
    }

    // Eliminar colaborador (borrado lógico)
    [HttpDelete("{cedula}")]
    public async Task<IActionResult> DeleteUserColaborador(long cedula){
        var result = await _userColaboradorService.DeleteUserColaborador(cedula);
        if (!result) return NotFound(new { message = "No se encuentra el colaborador o esta inactivo." });
        
        return Ok(new { message = "Se elimino el colaborador correctamente." });
    }
}

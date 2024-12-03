using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserAdminController : ControllerBase{
    private readonly UserAdminService _userAdminService;

    public UserAdminController(UserAdminService userAdminService){
        _userAdminService = userAdminService;
    }

    // Obtener todos los administradores
    [HttpGet]
    public async Task<IActionResult> GetAllAdmins(){
        var admins = await _userAdminService.GetAllAdmins();
        return Ok(admins);
    }

    // Obtener un administrador por cedula
    [HttpGet("{cedula}")]
    public async Task<IActionResult> GetByCedula(long cedula)
    {
        var admin = await _userAdminService.GetByCedula(cedula);
        if (admin == null)
        {
            return NotFound(new { message = "Admin not found" });
        }
        return Ok(admin);
    }

    //Registrar admin
    [HttpPost]
    public async Task<IActionResult> RegisterAdmin([FromBody] UserAdmin admin){
        if (!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        var result = await _userAdminService.RegisterAdmin(admin);
        if (!result){
            return Conflict(new { message = "Ya existe un administrador con la misma cedula." });
        }

        return CreatedAtAction(nameof(GetByCedula), new { cedula = admin.Cedula }, admin);
    }

    // Actualizar un administrador
    [HttpPut("{cedula}")]
    public async Task<IActionResult> UpdateAdmin(long cedula, [FromBody] UserAdmin userAdmin)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userAdminService.UpdateUserAdmin(cedula, userAdmin);
        if (!result)
        {
            return NotFound(new { message = "Admin not found" });
        }

        return Ok(new { message = "Admin updated successfully" });
    }
}
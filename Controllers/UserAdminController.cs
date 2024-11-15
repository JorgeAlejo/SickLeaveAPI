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

     [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserAdmin userAdmin){
        var login = await _userAdminService.AdminLogin(userAdmin.Cedula, userAdmin.Password);
        if (login == null) return Unauthorized("Correo o contraseña incorrectos.");
        return Ok(login.Cedula);
    }
}
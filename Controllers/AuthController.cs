using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase{
    private readonly AuthenticationService _authenticationService;

    public AuthController(AuthenticationService authenticationService){
        _authenticationService = authenticationService;
    }

    // Endpoint para autenticación
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest){
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var authResult = await _authenticationService.Authenticate(
            loginRequest.Cedula, 
            loginRequest.Password, 
            loginRequest.Role);

        if (!authResult.IsAuthenticated) return Unauthorized(new { message = authResult.Message });
        
        return Ok(new {
            message = authResult.Message,
            userId = authResult.UserId,
            role = authResult.Role.ToString()
        });
    }
}

// Modelo para la solicitud de inicio de sesión
public class LoginRequest{
    [Required]
    public long Cedula { get; set; }

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public UserRole Role { get; set; }
}

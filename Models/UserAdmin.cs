using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Email), IsUnique = true)]
public class UserAdmin{

    [Key]
    public long Cedula { get; set; }

    [Required(ErrorMessage = "Se requiere el primer nombre.")]
    public string PrimerNombre { get; set; } = string.Empty;
    public string? SegundoNombre {get; set; }
    
    [Required(ErrorMessage = "Se requiere el primer apellido.")]
    public string PrimerApellido { get; set; } = string.Empty;
    public string? SegundoApellido { get; set; }

    [Required(ErrorMessage = "Se requiere un correo electronico.")]
    [EmailAddress(ErrorMessage = "El fromato de correo no es valido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = string.Empty;
}
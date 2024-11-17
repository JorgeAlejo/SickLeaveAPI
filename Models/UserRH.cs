using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserRH
{
    [Key]
    public long Cedula { get; set; }

    [ForeignKey("UserAdmin")]
    public long CedulaAdministrador { get; set; }

    [Required]
    [MaxLength(50)]
    public string PrimerNombre { get; set; } = null!;

    [MaxLength(50)]
    public string? SegundoNombre { get; set; }

    [Required]
    [MaxLength(50)]
    public string PrimerApellido { get; set; } = null!;

    [MaxLength(50)]
    public string? SegundoApellido { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "El fromato de correo no es valido.")]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public DateTime FechaDeCreacion { get; set; } = DateTime.UtcNow;

    //Borrado Logico
     public bool IsActive { get; set; } = true;
}
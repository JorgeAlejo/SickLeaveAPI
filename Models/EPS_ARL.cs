using System.ComponentModel.DataAnnotations;

public class EPS_ARL{
    public Guid Id { get; set; } = Guid.NewGuid();
     
    [Required(ErrorMessage = "Se requiere un nombre.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Se requiere un numero de contacto.")]
    [Phone]
    public string NumeroContacto { get; set; } = null!;

    [Required(ErrorMessage = "Se requiere un correo.")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Se requiere un tipo.")]
    [MaxLength(3)]
    public string Tipo { get; set; } = null!; // Valores posibles: "EPS" o "ARL"
}
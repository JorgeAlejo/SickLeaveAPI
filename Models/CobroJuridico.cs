using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CobroJuridico{
    [Key]
    public Guid IdCobro { get; set; } = Guid.NewGuid();

    [Required]
    [ForeignKey("UserAbogado")]
    public long CedulaAbogado { get; set; }

    [Required]
    [ForeignKey("Incapacidad")]
    public Guid IdIncapacidad { get; set; }

    [Required]
    [ForeignKey("EPS_ARL")]
    public Guid IdEpsArl { get; set; }

    [Required]
    public DateTime FechaCobro { get; set; } = DateTime.UtcNow;

    [Required]
    public string DerechoPeticion { get; set; } = string.Empty;

    [Required]
    public string Tutela { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
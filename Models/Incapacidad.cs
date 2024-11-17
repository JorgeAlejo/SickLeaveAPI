using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Incapacidad
{
    [Key]
    public Guid IDIncapacidad { get; set; } = Guid.NewGuid();

    [Required]
    [ForeignKey("UserColaborador")]
    public long CedulaColaborador { get; set; } // FK con UserColaborador

    [Required]
    [ForeignKey("EPS_ARL")]
    public Guid IdEpsArl { get; set; } // FK con EPS_ARL

    [Required]
    [ForeignKey("UserRH")]
    public long CedulaRH { get; set; } // FK con UserRH

    [Required]
    public DateTime FechaInicio { get; set; } // Fecha de inicio de la incapacidad

    [Required]
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow; // Fecha de registro automático

    [Required]
    [MaxLength(50)]
    public TipoIncapacidad Tipo { get; set; } // Enfermedad General, Accidente, etc.

    [MaxLength(500)]
    public string? Descripcion { get; set; } // Descripción de la incapacidad

    [Required]
    [MaxLength(50)]
    public EstadoIncapacidad Estado { get; set; } = EstadoIncapacidad.Pendiente; // Estado (Pendiente, Aprobada, Rechazada)

    [Required]
    public string DocumentoAdjunto { get; set; } = string.Empty; // Ruta o URL al PDF adjunto

    public bool IsActive { get; set; } = true;
}

public enum TipoIncapacidad
{
    EnfermedadGeneral,
    AccidenteTransito,
    AccidenteLaboral,
    LicenciaMaternidad,
    LicenciaPaternidad
}

public enum EstadoIncapacidad
{
    Pendiente,
    Aprobada,
    Rechazada
}

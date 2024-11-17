using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PagoIncapacidad
{
    [Key]
    public Guid IdPago { get; set; } = Guid.NewGuid(); // Identificador único del pago

    [Required]
    [ForeignKey("UserTesorero")]
    public long CedulaTesorero { get; set; } // FK con el tesorero responsable del pago

    [Required]
    [ForeignKey("Incapacidad")]
    public Guid IdIncapacidad { get; set; } // FK con la incapacidad asociada

    [Required]
    public DateTime FechaRadicacion { get; set; } = DateTime.UtcNow; // Fecha de radicación del pago

    [Required]
    [MaxLength(20)]
    public EstadoPago Estado { get; set; } = EstadoPago.NoPagado; // Estado del pago

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "El valor debe ser mayor o igual a 0.")]
    public decimal Valor { get; set; } // Valor del pago

    public bool IsActive { get; set; } = true;
}

// Enum para el estado del pago
public enum EstadoPago
{
    Pagado,
    NoPagado
}

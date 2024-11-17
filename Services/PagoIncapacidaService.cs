using Microsoft.EntityFrameworkCore;

public class PagoIncapacidadService{
    private readonly SickLeaveDbContext _context;

    public PagoIncapacidadService(SickLeaveDbContext context){
        _context = context;
    }

    // Crear un nuevo pago de incapacidad
    public async Task<bool> CreatePagoIncapacidad(PagoIncapacidad pago){
        // Validar que el tesorero y la incapacidad existan
        if (!await _context.UserTesoreros.AnyAsync(t => t.Cedula == pago.CedulaTesorero) ||
            !await _context.Incapacidades.AnyAsync(i => i.IDIncapacidad == pago.IdIncapacidad && i.IsActive)){
            return false; // Falla la validación
        }

        _context.PagosIncapacidades.Add(pago);
        await _context.SaveChangesAsync();
        return true;
    }

    // Obtener todos los pagos de incapacidades
    public async Task<List<PagoIncapacidad>> GetAllPagos(){
        return await _context.PagosIncapacidades.Where(p => p.IsActive)
                                                .Include(p => p.CedulaTesorero)
                                                .Include(p => p.IdIncapacidad)
                                                .ToListAsync();
    }

    // Obtener un pago por su ID
    public async Task<PagoIncapacidad?> GetPagoById(Guid id){
        return await _context.PagosIncapacidades
                             .Include(p => p.CedulaTesorero)
                             .Include(p => p.IdIncapacidad)
                             .FirstOrDefaultAsync(p => p.IdPago == id && p.IsActive);
    }

    // Actualizar un pago de incapacidad
    public async Task<bool> UpdatePagoIncapacidad(Guid id, PagoIncapacidad updatedPago){
        var existingPago = await _context.PagosIncapacidades.FirstOrDefaultAsync(p => p.IdPago == id && p.IsActive);
        if (existingPago == null) return false;

        existingPago.FechaRadicacion = updatedPago.FechaRadicacion;
        existingPago.Estado = updatedPago.Estado;
        existingPago.Valor = updatedPago.Valor;

        await _context.SaveChangesAsync();
        return true;
    }

    // Eliminar un pago de incapacidad (borrado lógico)
    public async Task<bool> DeletePagoIncapacidad(Guid id){
        var existingPago = await _context.PagosIncapacidades.FirstOrDefaultAsync(i => i.IdPago == id && i.IsActive);
        if (existingPago == null) return false;

        existingPago.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }
}

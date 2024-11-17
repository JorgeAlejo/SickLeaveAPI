using Microsoft.EntityFrameworkCore;

public class CobroJuridicoService{
     private readonly SickLeaveDbContext _context;

    public CobroJuridicoService(SickLeaveDbContext context){
        _context = context;
    }

    // Crear un nuevo coboro
    public async Task<bool> CreateCobro(CobroJuridico cobro){
        // Validar que el abogado y la incapacidad existan
        if (!await _context.UserAbogados.AnyAsync(t => t.Cedula == cobro.CedulaAbogado) ||
            !await _context.Incapacidades.AnyAsync(i => i.IDIncapacidad == cobro.IdIncapacidad && i.IsActive)){
            return false; // Falla la validación
        }

        _context.CobrosJuridicos.Add(cobro);
        await _context.SaveChangesAsync();
        return true;
    }

    // Obtener todos los cobros
    public async Task<List<CobroJuridico>> GetAllCobros(){
        return await _context.CobrosJuridicos.Where(c => c.IsActive)
                                                .Include(c => c.CedulaAbogado)
                                                .Include(c => c.IdIncapacidad)
                                                .ToListAsync();
    }

    // Obtener un cobro por su ID
    public async Task<CobroJuridico?> GetCobroById(Guid id){
        return await _context.CobrosJuridicos
                             .Include(c => c.CedulaAbogado)
                             .Include(c => c.IdIncapacidad)
                             .FirstOrDefaultAsync(c => c.IdCobro == id && c.IsActive);
    }

    // Actualizar un cobro
    public async Task<bool> UpdateCobro(Guid id, CobroJuridico updatedCobro){
        var existingCobro = await _context.CobrosJuridicos.FirstOrDefaultAsync(c => c.IdCobro == id && c.IsActive);
        if (existingCobro == null) return false;

        existingCobro.FechaCobro = updatedCobro.FechaCobro;
        existingCobro.DerechoPeticion = updatedCobro.DerechoPeticion;
        existingCobro.Tutela = updatedCobro.Tutela;

        await _context.SaveChangesAsync();
        return true;
    }

    // Eliminar un pago de incapacidad (borrado lógico)
    public async Task<bool> DeleteCobro(Guid id){
        var existingCobro = await _context.CobrosJuridicos.FirstOrDefaultAsync(c => c.IdCobro == id && c.IsActive);
        if (existingCobro == null) return false;

        existingCobro.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }
}
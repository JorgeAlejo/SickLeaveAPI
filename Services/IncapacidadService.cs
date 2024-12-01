using Microsoft.EntityFrameworkCore;

public class IncapacidadService{
    private readonly SickLeaveDbContext _context;

    public IncapacidadService(SickLeaveDbContext context){
        _context = context;
    }

    // Crear una nueva incapacidad
    public async Task<bool> CreateIncapacidad(Incapacidad incapacidad){
        // Validar que el colaborador, EPS/ARL y RH existan
        if (!await _context.UserColaboradors.AnyAsync(c => c.Cedula == incapacidad.CedulaColaborador && c.IsActive) ||
            !await _context.EPS_ARLs.AnyAsync(e => e.Id == incapacidad.IdEpsArl) ||
            !await _context.UserRHs.AnyAsync(r => r.Cedula == incapacidad.CedulaRH && r.IsActive)){
            return false; // Falla la validación
        }

        _context.Incapacidades.Add(incapacidad);
        await _context.SaveChangesAsync();
        return true;
    }

    // Obtener todas las incapacidades
    public async Task<List<Incapacidad>> GetAllIncapacidades(){
        return await _context.Incapacidades.Where(i => i.IsActive)
                                           .Include(i => i.CedulaColaborador)
                                           .Include(i => i.IdEpsArl)
                                           .Include(i => i.CedulaRH)
                                           .ToListAsync();
    }

    // Obtener una incapacidad por ID
    public async Task<Incapacidad?> GetIncapacidadById(Guid id){
        return await _context.Incapacidades.Include(i => i.CedulaColaborador)
                                           .Include(i => i.IdEpsArl)
                                           .Include(i => i.CedulaRH)
                                           .FirstOrDefaultAsync(i => i.IDIncapacidad == id && i.IsActive);
    }

    // Obtener una incapacidad por CedulaColaborador
    public async Task<Incapacidad?> GetIncapacidadByCedulaCoraborador(long cedula){
        return await _context.Incapacidades.Include(i => i.CedulaColaborador)
                                           .Include(i => i.IdEpsArl)
                                           .Include(i => i.CedulaRH)
                                           .FirstOrDefaultAsync(i => i.CedulaColaborador== cedula && i.IsActive);
    }

    // Obtener una incapacidad por CedulaColaborador
    public async Task<Incapacidad?> GetIncapacidadByCedulaRH(long cedula){
        return await _context.Incapacidades.Include(i => i.CedulaColaborador)
                                           .Include(i => i.IdEpsArl)
                                           .Include(i => i.CedulaRH)
                                           .FirstOrDefaultAsync(i => i.CedulaRH== cedula && i.IsActive);
    }

    // Actualizar una incapacidad
    public async Task<bool> UpdateIncapacidad(Guid id, Incapacidad updatedIncapacidad){
        var existingIncapacidad = await _context.Incapacidades.FirstOrDefaultAsync(i => i.IDIncapacidad == id && i.IsActive);
        if (existingIncapacidad == null) return false;

        existingIncapacidad.Tipo = updatedIncapacidad.Tipo;
        existingIncapacidad.Descripcion = updatedIncapacidad.Descripcion ?? existingIncapacidad.Descripcion;
        existingIncapacidad.DocumentoAdjunto = updatedIncapacidad.DocumentoAdjunto ?? existingIncapacidad.DocumentoAdjunto;

        await _context.SaveChangesAsync();
        return true;
    }

    // Cambiar el estado de una incapacidad
    public async Task<bool> UpdateEstadoIncapacidad(Guid id, EstadoIncapacidad nuevoEstado){
        var existingIncapacidad = await _context.Incapacidades.FirstOrDefaultAsync(i => i.IDIncapacidad == id && i.IsActive);
        if (existingIncapacidad == null) return false;

        existingIncapacidad.Estado = nuevoEstado;
        await _context.SaveChangesAsync();
        return true;
    }

    // Eliminar una incapacidad (borrado logico)
    public async Task<bool> DeleteIncapacidad(Guid id){
        var existingIncapacidad = await _context.Incapacidades.FirstOrDefaultAsync(i => i.IDIncapacidad == id && i.IsActive);
        if (existingIncapacidad == null) return false;

        existingIncapacidad.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }
}

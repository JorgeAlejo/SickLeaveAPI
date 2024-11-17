using Microsoft.EntityFrameworkCore;

public class EPS_ARLService{
    private readonly SickLeaveDbContext _context;

    public EPS_ARLService(SickLeaveDbContext context){
        _context = context;
    }

    // Crear una nueva EPS/ARL
    public async Task<bool> Create(EPS_ARL epsArl){
        if (await _context.EPS_ARLs.AnyAsync(e => e.Email == epsArl.Email)) return false; // Ya existe una EPS/ARL con este correo
        
        _context.EPS_ARLs.Add(epsArl);
        await _context.SaveChangesAsync();
        return true;
    }

    // Obtener todas las EPS/ARL
    public async Task<List<EPS_ARL>> GetAll(){
        return await _context.EPS_ARLs.ToListAsync();
    }

    // Obtener EPS/ARL por ID
    public async Task<EPS_ARL?> GetById(Guid id){
        return await _context.EPS_ARLs.FirstOrDefaultAsync(e => e.Id == id);
    }

    // Actualizar una EPS/ARL
    public async Task<bool> Update(Guid id, EPS_ARL updatedEpsArl){
        var existingEpsArl = await _context.EPS_ARLs.FirstOrDefaultAsync(e => e.Id == id);
        if (existingEpsArl == null) return false;
        
        existingEpsArl.Name = updatedEpsArl.Name ?? existingEpsArl.Name;
        existingEpsArl.NumeroContacto = updatedEpsArl.NumeroContacto ?? existingEpsArl.NumeroContacto;
        existingEpsArl.Email = updatedEpsArl.Email ?? existingEpsArl.Email;
        existingEpsArl.Tipo = updatedEpsArl.Tipo ?? existingEpsArl.Tipo;

        await _context.SaveChangesAsync();
        return true;
    }

    // Eliminar una EPS/ARL
    public async Task<bool> Delete(Guid id){
        var epsArl = await _context.EPS_ARLs.FirstOrDefaultAsync(e => e.Id == id);
        if (epsArl == null) return false;
        
        _context.EPS_ARLs.Remove(epsArl);
        await _context.SaveChangesAsync();
        return true;
    }
}

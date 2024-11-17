using Microsoft.EntityFrameworkCore;

public class UserRHService
{
    private readonly SickLeaveDbContext _context;

    public UserRHService(SickLeaveDbContext context){
        _context = context;
    }

    // Crear un nuevo RH
    public async Task<bool> CreateUserRH(UserRH rH){
        if (await _context.UserRHs.AnyAsync(c => c.Cedula == rH.Cedula))
            return false; // Ya existe un RH con esta cédula

        // Hash de la contraseña
        rH.Password = BCrypt.Net.BCrypt.HashPassword(rH.Password);

        _context.UserRHs.Add(rH);
        await _context.SaveChangesAsync();
        return true;
    }

    // Obtener todos los RHes activos
    public async Task<List<UserRH>> GetAllActiveRHes(){
        return await _context.UserRHs.Where(c => c.IsActive).ToListAsync();
    }

    // Obtener RH por cédula
    public async Task<UserRH?> GetByCedula(long cedula){
        return await _context.UserRHs.FirstOrDefaultAsync(c => c.Cedula == cedula && c.IsActive);
    }

    // Actualizar RH
    public async Task<bool> UpdateUserRH(long cedula, UserRH updatedRH){
        var existingRH = await _context.UserRHs.FirstOrDefaultAsync(c => c.Cedula == cedula && c.IsActive);
        if (existingRH == null) return false;

        existingRH.PrimerNombre = updatedRH.PrimerNombre ?? existingRH.PrimerNombre;
        existingRH.SegundoNombre = updatedRH.SegundoNombre;
        existingRH.PrimerApellido = updatedRH.PrimerApellido ?? existingRH.PrimerApellido;
        existingRH.SegundoApellido = updatedRH.SegundoApellido;
        existingRH.Email = updatedRH.Email ?? existingRH.Email;

        // Actualizar contraseña (si se proporciona)
        if (!string.IsNullOrWhiteSpace(updatedRH.Password)){ 
            existingRH.Password = BCrypt.Net.BCrypt.HashPassword(updatedRH.Password);
        }
        
        await _context.SaveChangesAsync();
        return true;
    }

    // Eliminar RH (borrado lógico)
    public async Task<bool> DeleteUserRH(long cedula){
        var existingRH = await _context.UserRHs.FirstOrDefaultAsync(c => c.Cedula == cedula && c.IsActive);
        if (existingRH == null) return false;

        existingRH.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }
}
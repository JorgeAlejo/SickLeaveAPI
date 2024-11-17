// TODO: crear controlador y poner en db
using Microsoft.EntityFrameworkCore;

public class UserTesoreroService
{
    private readonly SickLeaveDbContext _context;

    public UserTesoreroService(SickLeaveDbContext context){
        _context = context;
    }

    // Crear un nuevo tesorero
    public async Task<bool> CreateUserTesorero(UserTesorero tesorero){
        if (await _context.UserTesoreros.AnyAsync(c => c.Cedula == tesorero.Cedula))
            return false; // Ya existe un tesorero con esta cédula

        // Hash de la contraseña
        tesorero.Password = BCrypt.Net.BCrypt.HashPassword(tesorero.Password);

        _context.UserTesoreros.Add(tesorero);
        await _context.SaveChangesAsync();
        return true;
    }

    // Obtener todos los tesoreroes activos
    public async Task<List<UserTesorero>> GetAllActiveTesoreroes(){
        return await _context.UserTesoreros.Where(c => c.IsActive).ToListAsync();
    }

    // Obtener tesorero por cédula
    public async Task<UserTesorero?> GetByCedula(long cedula){
        return await _context.UserTesoreros.FirstOrDefaultAsync(c => c.Cedula == cedula && c.IsActive);
    }

    // Actualizar tesorero
    public async Task<bool> UpdateUserTesorero(long cedula, UserTesorero updatedtesorero){
        var existingtesorero = await _context.UserTesoreros.FirstOrDefaultAsync(c => c.Cedula == cedula && c.IsActive);
        if (existingtesorero == null) return false;

        existingtesorero.PrimerNombre = updatedtesorero.PrimerNombre ?? existingtesorero.PrimerNombre;
        existingtesorero.SegundoNombre = updatedtesorero.SegundoNombre;
        existingtesorero.PrimerApellido = updatedtesorero.PrimerApellido ?? existingtesorero.PrimerApellido;
        existingtesorero.SegundoApellido = updatedtesorero.SegundoApellido;
        existingtesorero.Email = updatedtesorero.Email ?? existingtesorero.Email;

        // Actualizar contraseña (si se proporciona)
        if (!string.IsNullOrWhiteSpace(updatedtesorero.Password)){ 
            existingtesorero.Password = BCrypt.Net.BCrypt.HashPassword(updatedtesorero.Password);
        }
        
        await _context.SaveChangesAsync();
        return true;
    }

    // Eliminar tesorero (borrado lógico)
    public async Task<bool> DeleteUserTesorero(long cedula){
        var existingtesorero = await _context.UserTesoreros.FirstOrDefaultAsync(c => c.Cedula == cedula && c.IsActive);
        if (existingtesorero == null) return false;

        existingtesorero.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }
}
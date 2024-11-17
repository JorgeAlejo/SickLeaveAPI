using Microsoft.EntityFrameworkCore;

public class UserAbogadoService
{
    private readonly SickLeaveDbContext _context;

    public UserAbogadoService(SickLeaveDbContext context){
        _context = context;
    }

    // Crear un nuevo Abogado
    public async Task<bool> CreateUserAbogado(UserAbogado abogado){
        if (await _context.UserAbogados.AnyAsync(c => c.Cedula == abogado.Cedula))
            return false; // Ya existe un Abogado con esta cédula

        // Hash de la contraseña
        abogado.Password = BCrypt.Net.BCrypt.HashPassword(abogado.Password);

        _context.UserAbogados.Add(abogado);
        await _context.SaveChangesAsync();
        return true;
    }

    // Obtener todos los Abogadoes activos
    public async Task<List<UserAbogado>> GetAllActiveAbogadoes(){
        return await _context.UserAbogados.Where(c => c.IsActive).ToListAsync();
    }

    // Obtener Abogado por cédula
    public async Task<UserAbogado?> GetByCedula(long cedula){
        return await _context.UserAbogados.FirstOrDefaultAsync(c => c.Cedula == cedula && c.IsActive);
    }

    // Actualizar Abogado
    public async Task<bool> UpdateUserAbogado(long cedula, UserAbogado updatedAbogado){
        var existingAbogado = await _context.UserAbogados.FirstOrDefaultAsync(c => c.Cedula == cedula && c.IsActive);
        if (existingAbogado == null) return false;

        existingAbogado.PrimerNombre = updatedAbogado.PrimerNombre ?? existingAbogado.PrimerNombre;
        existingAbogado.SegundoNombre = updatedAbogado.SegundoNombre;
        existingAbogado.PrimerApellido = updatedAbogado.PrimerApellido ?? existingAbogado.PrimerApellido;
        existingAbogado.SegundoApellido = updatedAbogado.SegundoApellido;
        existingAbogado.Email = updatedAbogado.Email ?? existingAbogado.Email;

        // Actualizar contraseña (si se proporciona)
        if (!string.IsNullOrWhiteSpace(updatedAbogado.Password)){ 
            existingAbogado.Password = BCrypt.Net.BCrypt.HashPassword(updatedAbogado.Password);
        }
        
        await _context.SaveChangesAsync();
        return true;
    }

    // Eliminar Abogado (borrado lógico)
    public async Task<bool> DeleteUserAbogado(long cedula){
        var existingAbogado = await _context.UserAbogados.FirstOrDefaultAsync(c => c.Cedula == cedula && c.IsActive);
        if (existingAbogado == null) return false;

        existingAbogado.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }
}
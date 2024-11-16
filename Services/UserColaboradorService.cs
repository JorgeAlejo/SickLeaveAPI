using Microsoft.EntityFrameworkCore;

public class UserColaboradorService
{
    private readonly SickLeaveDbContext _context;

    public UserColaboradorService(SickLeaveDbContext context){
        _context = context;
    }

    // Crear un nuevo colaborador
    public async Task<bool> CreateUserColaborador(UserColaborador colaborador){
        if (await _context.UserColaboradors.AnyAsync(c => c.Cedula == colaborador.Cedula))
            return false; // Ya existe un colaborador con esta cédula

        // Hash de la contraseña
        colaborador.Password = BCrypt.Net.BCrypt.HashPassword(colaborador.Password);

        _context.UserColaboradors.Add(colaborador);
        await _context.SaveChangesAsync();
        return true;
    }

    // Obtener todos los colaboradores activos
    public async Task<List<UserColaborador>> GetAllActiveColaboradores(){
        return await _context.UserColaboradors.Where(c => c.IsActive).ToListAsync();
    }

    // Obtener colaborador por cédula
    public async Task<UserColaborador?> GetByCedula(long cedula){
        return await _context.UserColaboradors.FirstOrDefaultAsync(c => c.Cedula == cedula && c.IsActive);
    }

    // Actualizar colaborador
    public async Task<bool> UpdateUserColaborador(long cedula, UserColaborador updatedColaborador){
        var existingColaborador = await _context.UserColaboradors.FirstOrDefaultAsync(c => c.Cedula == cedula && c.IsActive);
        if (existingColaborador == null) return false;

        existingColaborador.PrimerNombre = updatedColaborador.PrimerNombre ?? existingColaborador.PrimerNombre;
        existingColaborador.SegundoNombre = updatedColaborador.SegundoNombre;
        existingColaborador.PrimerApellido = updatedColaborador.PrimerApellido ?? existingColaborador.PrimerApellido;
        existingColaborador.SegundoApellido = updatedColaborador.SegundoApellido;
        existingColaborador.Rol = updatedColaborador.Rol ?? existingColaborador.Rol;
        existingColaborador.Email = updatedColaborador.Email ?? existingColaborador.Email;

        // Actualizar contraseña (si se proporciona)
        if (!string.IsNullOrWhiteSpace(updatedColaborador.Password)){ 
            existingColaborador.Password = BCrypt.Net.BCrypt.HashPassword(updatedColaborador.Password);
        }
        
        await _context.SaveChangesAsync();
        return true;
    }

    // Eliminar colaborador (borrado lógico)
    public async Task<bool> DeleteUserColaborador(long cedula){
        var existingColaborador = await _context.UserColaboradors.FirstOrDefaultAsync(c => c.Cedula == cedula && c.IsActive);
        if (existingColaborador == null) return false;

        existingColaborador.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }
}

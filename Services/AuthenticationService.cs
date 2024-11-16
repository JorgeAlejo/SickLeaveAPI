using Microsoft.EntityFrameworkCore;

public class AuthenticationService{
    private readonly SickLeaveDbContext _context;

    public AuthenticationService(SickLeaveDbContext context){
        _context = context;
    }

    // Método principal de autenticación
    public async Task<AuthenticationResult> Authenticate(long cedula, string password, UserRole role){
        switch (role){
            case UserRole.Admin:
                return await AuthenticateAdmin(cedula, password);

            case UserRole.Colaborador:
                return await AuthenticateColaborador(cedula, password);

            default:
                return new AuthenticationResult{
                    IsAuthenticated = false,
                    Message = "Rol inexistente."
                };
        }
    }

    // Autenticación de administradores
    private async Task<AuthenticationResult> AuthenticateAdmin(long cedula, string password){
        var admin = await _context.UserAdmins.FirstOrDefaultAsync(u => u.Cedula == cedula);
        if (admin == null || !BCrypt.Net.BCrypt.Verify(password, admin.Password)){
            return new AuthenticationResult{
                IsAuthenticated = false,
                Message = "Cedula o contaseña erronea."
            };
        }

        return new AuthenticationResult{
            IsAuthenticated = true,
            UserId = admin.Cedula,
            Role = UserRole.Admin,
            Message = "Ingreso existos."
        };
    }

    // Autenticación de colaboradores
    private async Task<AuthenticationResult> AuthenticateColaborador(long cedula, string password){
        var colaborador = await _context.UserColaboradors.FirstOrDefaultAsync(c => c.Cedula == cedula && c.IsActive);
        if (colaborador == null || !BCrypt.Net.BCrypt.Verify(password, colaborador.Password)){
            return new AuthenticationResult{
                IsAuthenticated = false,
                Message = "Cedula o contaseña erronea."
            };
        }

        return new AuthenticationResult{
            IsAuthenticated = true,
            UserId = colaborador.Cedula,
            Role = UserRole.Colaborador,
            Message = "Ingreso existos."
        };
    }
}

// Modelo para los resultados de autenticación
public class AuthenticationResult{
    public bool IsAuthenticated { get; set; }
    public long? UserId { get; set; }
    public UserRole? Role { get; set; }
    public string Message { get; set; } = string.Empty;
}

// Enum para roles de usuario
public enum UserRole{
    Admin,
    Colaborador
}

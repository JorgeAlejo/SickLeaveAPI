using Microsoft.EntityFrameworkCore;

public class UserAdminService{
    private readonly SickLeaveDbContext _context;

    public UserAdminService(SickLeaveDbContext context){
        _context = context;
    }

    //Get all admins
    public async Task<List<UserAdmin>> GetAllAdmins(){
        return await _context.UserAdmins.ToListAsync();
    }

    //Get Admin por cedula
    public async Task<UserAdmin?> GetByCedula(long cedula){
        return await _context.UserAdmins.FirstOrDefaultAsync(u => u.Cedula == cedula);
    } 

    //Registro de Administrador
    /*
    public async Task<bool> RegisterAdmin(UserAdmin userAdmin){
        if(await _context.UserAdmins.AnyAsync(u => u.Cedula == userAdmin.Cedula)) return false;
        //password Hashing
        userAdmin.Password = BCrypt.Net.BCrypt.HashPassword(userAdmin.Password);

        _context.UserAdmins.Add(userAdmin);
        await _context.SaveChangesAsync();
        return true;
    }
    */

    //Login de administrador
    public async Task<UserAdmin?> AdminLogin(long cedula, string password){
        var userAdmin = await _context.UserAdmins.FirstOrDefaultAsync(u => u.Cedula == cedula);
        if(userAdmin == null) return null;

        //verify password
        if(BCrypt.Net.BCrypt.Verify(password, userAdmin.Password)) return userAdmin;
        return null;
    }

    // TODO: Update admin 
}
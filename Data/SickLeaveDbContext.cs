using Microsoft.EntityFrameworkCore;

public class SickLeaveDbContext: DbContext{
    public SickLeaveDbContext(DbContextOptions<SickLeaveDbContext> options):base(options){}
    public required DbSet<UserAdmin> UserAdmins { get; set; }
    public required DbSet<UserColaborador> UserColaboradors { get; set; }
    public required DbSet<UserTesorero> UserTesoreros { get; set; }
}
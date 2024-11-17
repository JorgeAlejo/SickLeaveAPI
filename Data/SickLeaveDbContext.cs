using Microsoft.EntityFrameworkCore;

public class SickLeaveDbContext: DbContext{
    public SickLeaveDbContext(DbContextOptions<SickLeaveDbContext> options):base(options){}
    public required DbSet<UserAdmin> UserAdmins { get; set; }
    public required DbSet<UserColaborador> UserColaboradors { get; set; }
    public required DbSet<UserTesorero> UserTesoreros { get; set; }
    public required DbSet<UserAbogado> UserAbogados { get; set; }
    public required DbSet<UserRH> UserRHs { get; set; }
    public required DbSet<EPS_ARL> EPS_ARLs { get; set; }
    public required DbSet<Incapacidad> Incapacidades { get; set; }
    public required DbSet<PagoIncapacidad> PagosIncapacidades { get; set; }
    public required DbSet<CobroJuridico> CobrosJuridicos { get; set; }
}
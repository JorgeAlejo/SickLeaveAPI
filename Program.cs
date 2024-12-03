using Microsoft.OpenApi.Models;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = $"Host=host.docker.internal;"+
                       $"Port={Environment.GetEnvironmentVariable("DB_PORT")};"+
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                       $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                       $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

// Add services to the container.
builder.Services.AddDbContext<SickLeaveDbContext>(options => 
    options.UseNpgsql(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sick Leave API", Version = "v1" });
});
builder.Services.AddCors(options =>{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Origen permitido
              .AllowAnyMethod()                    // Métodos HTTP permitidos
              .AllowAnyHeader();                   // Encabezados permitidos
    });
});
builder.Services.AddControllers();
builder.Services.AddScoped<UserAdminService>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<UserColaboradorService>();
builder.Services.AddScoped<UserAbogadoService>();
builder.Services.AddScoped<UserRHService>();
builder.Services.AddScoped<UserTesoreroService>();
builder.Services.AddScoped<EPS_ARLService>();
builder.Services.AddScoped<IncapacidadService>();
builder.Services.AddScoped<PagoIncapacidadService>();
builder.Services.AddScoped<CobroJuridicoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sick Leave API v1");
        c.RoutePrefix = string.Empty; // Esto hace que Swagger esté en la ruta principal 
    });
}

// Usa CORS
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

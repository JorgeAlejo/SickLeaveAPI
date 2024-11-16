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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vet API", Version = "v1" });
});
builder.Services.AddControllers();
builder.Services.AddScoped<UserAdminService>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<UserColaboradorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vet API v1");
        c.RoutePrefix = string.Empty; // Esto hace que Swagger esté en la ruta principal 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

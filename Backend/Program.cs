using Backend.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configuración
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true);

// Servicios
builder.Services.AddSingleton<IEmailService>(sp =>
{
    return new EmailService(sp.GetRequiredService<IConfiguration>());
});



builder.Services.AddControllers();

// ===== CORS dinámico según entorno =====

var frontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL")
                  ?? "https://localhost:7125"; 

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins(frontendUrl)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


var app = builder.Build();


// Middleware
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

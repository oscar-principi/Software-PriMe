using Backend.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configuración
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true);

// Servicios
builder.Services.AddSingleton<IEmailService, EmailService>();

builder.Services.AddControllers();

// CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7125")
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

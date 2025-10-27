var builder = WebApplication.CreateBuilder(args);


//Servicios
builder.Services.AddSingleton<EmailService>();
builder.Services.AddControllers();


var app = builder.Build();


//Middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

using CDS_DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CDS API",
        Version = "v1"
    });
});

// Add database context
//builder.Services.AddDbContext<LogistContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("BD_LOGIST")));
var connectionString = Environment.GetEnvironmentVariable("RAILWAY_DATABASE_URL")
    ?? builder.Configuration.GetConnectionString("BD_LOGIST");

builder.Services.AddDbContext<LogistContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<CDS_BLL.Interfaces.IVendedorService, CDS_BLL.Services.VendedorService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.IProductoService, CDS_BLL.Services.ProductoService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.ILoginService, CDS_BLL.Services.LoginService>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:3000",
                    "https://f521-38-25-17-121.ngrok-free.app", // Agregar la nueva URL de ngrok
                    "https://*.netlify.app", // Permitir cualquier subdominio de Netlify
                    "https://*.netlify.com",  // También permitir netlify.com
                    "http://localhost:5173", // Puerto de Vite
                    "https://localhost:5173" // HTTPS
                    )
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Configuración de Kestrel para HTTP y HTTPS
var portHttp = 5107;
var portHttps = 7002;

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(portHttp); // HTTP
    serverOptions.ListenAnyIP(portHttps, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS en 7002
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CDS API v1");
    c.RoutePrefix = "swagger"; // Swagger UI en /swagger
});

app.UseHttpsRedirection(); // Redirige HTTP a HTTPS

// Usar CORS antes de Authorization
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();

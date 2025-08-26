using CDS_DAL;
using CDS_Models;
using CDS_Models.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

var renderConn = Environment.GetEnvironmentVariable("CONNECTION_STRING");
Console.WriteLine($"[DEBUG][Program.cs] CONNECTION_STRING (Render): {(string.IsNullOrEmpty(renderConn) ? "NO DEFINIDA" : "DEFINIDA")}");

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

// Configuración de AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(ProyectoProfile), typeof(MonedaProfile), typeof(ClienteProfile), typeof(VendedorProfile), typeof(TipoDocumentoProfile), typeof(MarcaProfile), typeof(SubTipoNegocioProfile), typeof(TipoNegocioProfile), typeof(SubSubTipoNegocioProfile), typeof(StatusOpProfile), typeof(FormaPagoProfile), typeof(CatFormaPagoProfile));

// Lógica para usar BD_LOGISTICA_LOCAL en local, y Railway/Azure en producción
string connectionString;
if (builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetConnectionString("DB_CISAC_LOGIST");
}
else
{
    connectionString = Environment.GetEnvironmentVariable("RAILWAY_DB_URL");
}

if (!string.IsNullOrEmpty(renderConn))
{
    builder.Services.AddDbContext<CDS_DAL.LogistContext>(options =>
        options.UseSqlServer(renderConn));
}
else
{
    // Usar la configuración local (appsettings.json)
    var localConn = builder.Configuration.GetConnectionString("DB_CISAC_LOGIST");
    builder.Services.AddDbContext<CDS_DAL.LogistContext>(options =>
        options.UseSqlServer(localConn));
}

builder.Services.AddScoped<CDS_BLL.Interfaces.IVendedorService, CDS_BLL.Services.VendedorService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.IProductoService, CDS_BLL.Services.ProductoService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.ILoginService, CDS_BLL.Services.LoginService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.IClienteService, CDS_BLL.Services.ClienteService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.IMonedaService, CDS_BLL.Services.MonedaService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.ITipoDocumentoService, CDS_BLL.Services.TipoDocumentoService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.IMarcaService, CDS_BLL.Services.MarcaService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.ISubTipoNegocioService, CDS_BLL.Services.SubTipoNegocioService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.ITipoNegocioService, CDS_BLL.Services.TipoNegocioService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.ISubSubTipoNegocioService, CDS_BLL.Services.SubSubTipoNegocioService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.IStatusOpService, CDS_BLL.Services.StatusOpService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.IFormaPagoService, CDS_BLL.Services.FormaPagoService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.ICatFormaPagoService, CDS_BLL.Services.CatFormaPagoService>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:3000",
                    "https://cds-clientapp.netlify.app", // Netlify principal
                    "https://cds-api.onrender.com", // Dominio del backend en Render
                    "https://*.netlify.app", // Subdominios de Netlify
                    "https://*.netlify.com",  // También permitir netlify.com
                    "http://localhost:5173", // Puerto de Vite
                    "https://localhost:5173",
                    "http://localhost:5175",
                    "https://localhost:5175"
                    )
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Configuración de Kestrel para Railway y local
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ListenAnyIP(int.Parse(port));
    });
}


var app = builder.Build();
// Solo redirige a HTTPS en desarrollo local
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

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

app.MapControllers();

app.Run();

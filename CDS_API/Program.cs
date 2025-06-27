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
builder.Services.AddDbContext<LogistContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BD_LOGIST")));

builder.Services.AddScoped<CDS_BLL.Interfaces.IVendedorService, CDS_BLL.Services.VendedorService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.IProductoService, CDS_BLL.Services.ProductoService>();
builder.Services.AddScoped<CDS_BLL.Interfaces.ILoginService, CDS_BLL.Services.LoginService>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .WithOrigins(
                    "http://localhost:3000",
                    "https://cc4d-190-107-182-178.ngrok-free.app"
                    )
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Para uso del puerto
var port = Environment.GetEnvironmentVariable("PORT") ?? "7002";

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port));
});


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "CDS API v1");
        c.RoutePrefix = "swagger"; // Swagger UI en /swagger
    });
}

app.UseHttpsRedirection();

// Usar CORS antes de Authorization
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();

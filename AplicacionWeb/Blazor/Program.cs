using Blazor;
using Blazor.Interfaces;
using Blazor.Servicios;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authentication.Cookies;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Configuración para la CadenaConexion para poderla inyectar en cualquier parte del código de Blazor
Config cadenaConexion = new Config(builder.Configuration.GetConnectionString("MySQL"));
builder.Services.AddSingleton(cadenaConexion); // Permite reutilizar el servicio en la aplicación

builder.Services.AddScoped<ILogInServicio, LogInServicio>(); // Configuración LoginServicio
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>(); // Configuración UsuarioServicio
builder.Services.AddScoped<IProductoServicio, ProductoServicio>(); // Configuración ProductoServicio
builder.Services.AddScoped<IFacturaServicio, FacturaServicio>(); // Configuración FacturaServicio
builder.Services.AddScoped<IDetalleFacturaServicio, DetalleFacturaServicio>(); // Configuración DetalleFacturaServicio
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(); // Configurar autenticación

builder.Services.AddSweetAlert2(); // Configurar Sweet Alert

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

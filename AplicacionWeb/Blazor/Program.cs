using Blazor;
using Blazor.Interfaces;
using Blazor.Servicios;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Configuraci�n para la CadenaConexion para poderla inyectar en cualquier parte del c�digo de Blazor
Config cadenaConexion = new Config(builder.Configuration.GetConnectionString("MySQL"));
builder.Services.AddSingleton(cadenaConexion); // Permite reutilizar el servicio en la aplicaci�n

builder.Services.AddScoped<ILogInServicio, LogInServicio>(); // Configuraci�n LoginServicio
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>(); // Configuraci�n UsuarioServicio
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(); // Configurar autenticaci�n

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

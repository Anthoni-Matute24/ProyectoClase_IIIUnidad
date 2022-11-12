using Blazor;
using Blazor.Interfaces;
using Blazor.Servicios;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Configuración para la CadenaConexion para poderla inyectar en cualquier parte del código de Blazor
Config cadenaConexion = new Config(builder.Configuration.GetConnectionString("MySQL"));
builder.Services.AddSingleton(cadenaConexion); // Permite reutilizar el servicio en la aplicación

builder.Services.AddScoped<ILogInServicio, LogInServicio>(); // Configuración LoginServicio

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

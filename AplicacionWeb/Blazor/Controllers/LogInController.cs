using Datos.Interfaces;
using Datos.Repositorios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using System.Security.Claims;

namespace Blazor.Controllers
{
    public class LogInController : Controller
    {
        // Solo permite leer el contenido de la propiedad "_configuracion"
        private readonly Config _configuracion;

        // Llamar la Interfaz del Repositorio
        private ILogInRepositorio _logInRepositorio; // Nombre Objeto: _logInRepositorio
        private IUsuarioRepositorio _usuarioRepositorio; // Nombre Objeto: _usuarioRepositorio

        public LogInController(Config config)
        {
            _configuracion = config;
            _logInRepositorio = new LogInRepositorio(config.CadenaConexion);
            _usuarioRepositorio = new UsuarioRepositorio(config.CadenaConexion);
        }

        // Verbos HTTP
        [HttpPost("/account/login")]

        // IActionResult: permite regresar el estatus de dicha petición, es decir, si fue exitosa o no fue exitosa 
        public async Task <IActionResult> LogIn(LogIn logIn)
        {
            string Rol = string.Empty;

            try
            {
                // Devuelve True si el usuario está registrado
                bool usuarioValido = await _logInRepositorio.ValidarUsuario(logIn); 

                if (usuarioValido) // Si está registrado, entonces devolverá los datos de ese registro
                {
                    Usuario user = await _usuarioRepositorio.GetPorCodigo(logIn.Codigo);

                    if (user.EstaActivo) // Validar si está activo
                    {
                        Rol = user.Rol;

                        var claims = new[] // Arreglo de Claims para pasar el nombre y rol del usuario.
                        {
                            new Claim(ClaimTypes.Name, user.Codigo),
                            new Claim(ClaimTypes.Role, Rol)
                        };

                        // Tipo de autenticación: Cookies
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        /* Método de iniciar sesión
                         * IsPersistent = true: la sesión siempre estará activa
                         * ExpiresUtc = DateTime.UtcNow.AddMinutes(#): Tiempo que durará la sesión mientras el usuario está inactivo
                        */
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddMinutes(5) });
                    }
                    else
                    {   // Si el usuario no está activo, lo direccionará nuevamente al LogIn
                        return LocalRedirect("/logIn/El usuario no está activo");
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return 
        }
    }
}

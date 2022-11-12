using Blazor.Interfaces;
using Datos.Interfaces;
using Datos.Repositorios;
using Modelos;

namespace Blazor.Servicios
{
    public class LogInServicio : ILogInServicio
    {
        // Solo permite leer el contenido de la propiedad "_configuracion"
        private readonly Config _configuracion;

        // Llamar la Interfaz del Repositorio
        private ILogInRepositorio logInRepositorio; // Nombre Objeto: logInRepositorio

        public LogInServicio(Config config) // Constructor
        {
            _configuracion = config;
            // Configuración del servicio
            // Del objeto "Config" se toma la CadenaConexion
            logInRepositorio = new LogInRepositorio(config.CadenaConexion);
        }
        public async Task<bool> ValidarUsuario(LogIn login)
        {
            return await logInRepositorio.ValidarUsuario(login);
        }
    }
}

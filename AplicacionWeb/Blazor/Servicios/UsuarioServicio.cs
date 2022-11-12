using Blazor.Interfaces;
using Datos.Interfaces;
using Datos.Repositorios;
using Modelos;

namespace Blazor.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        // Solo permite leer el contenido de la propiedad "_configuracion"
        private readonly Config _configuracion;

        // Llamar la Interfaz del Repositorio
        private IUsuarioRepositorio _usuarioRepositorio; // Nombre Objeto: logInRepositorio

        public UsuarioServicio(Config config) // Constructor
        {
            _configuracion = config;
            // Configuración del servicio
            // Del objeto "Config" se toma la CadenaConexion
            _usuarioRepositorio = new UsuarioRepositorio(config.CadenaConexion);
        }

        public async Task<bool> ActualizarUsuario(Usuario usuario)
        {
            return await _usuarioRepositorio.ActualizarUsuario(usuario);
        }

        public async Task<bool> EliminarUsuario(string codigo)
        {
            return await _usuarioRepositorio.EliminarUsuario(codigo);
        }

        public async Task<IEnumerable<Usuario>> GetLista()
        {
            return await _usuarioRepositorio.GetLista();
        }

        public async Task<Usuario> GetPorCodigo(string codigo)
        {
           return await _usuarioRepositorio.GetPorCodigo(codigo);
        }

        public async Task<bool> NuevoUsuario(Usuario usuario)
        {
            return await _usuarioRepositorio.NuevoUsuario(usuario);
        }
    }
}

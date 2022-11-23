using Blazor.Interfaces;
using Datos.Interfaces;
using Datos.Repositorios;
using Modelos;

namespace Blazor.Servicios
{
    public class ProductoServicio : IProductoServicio
    {
        // Solo permite leer el contenido de la propiedad "_configuracion"
        private readonly Config _configuracion;

        // Llamar la Interfaz del Repositorio
        private IProductoRepositorio _productoRepositorio; // Nombre Objeto: logInRepositorio

        public ProductoServicio(Config config) // Constructor
        {
            _configuracion = config;
            // Configuración del servicio
            // Del objeto "Config" se toma la CadenaConexion
            _productoRepositorio = new ProductoRepositorio(config.CadenaConexion);
        }

        public async Task<bool> ActualizarProducto(Producto producto)
        {
            return await _productoRepositorio.ActualizarProducto(producto);
        }

        public async Task<bool> EliminarProducto(int codigo)
        {
            return await _productoRepositorio.EliminarProducto(codigo);
        }

        public async Task<IEnumerable<Producto>> GetListaProductos()
        {
            return await _productoRepositorio.GetListaProductos();
        }

        public async Task<Producto> GetPorCodigo(int codigo)
        {
            return await _productoRepositorio.GetPorCodigo(codigo);
        }

        public async Task<bool> NuevoProducto(Producto producto)
        {
            return await _productoRepositorio.NuevoProducto(producto);
        }
    }
}

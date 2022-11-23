using Modelos;

namespace Blazor.Interfaces
{
    public interface IProductoServicio
    {
        Task<bool> NuevoProducto(Producto producto);
        Task<bool> ActualizarProducto(Producto producto);
        Task<bool> EliminarProducto(int codigo);
        Task<IEnumerable<Producto>> GetListaProductos();
        Task<Producto> GetPorCodigo(int codigo);
    }
}

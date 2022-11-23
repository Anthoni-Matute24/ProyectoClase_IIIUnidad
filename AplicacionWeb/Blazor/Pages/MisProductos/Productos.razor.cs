using Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor.Pages.MisProductos
{
    public partial class Productos
    {
        // Inyectar los servicios de IProductoServicio
        [Inject] IProductoServicio productoServicio { get; set; }

        // Lista para capturar todos los elementos de la tabla Producto de la BD
        IEnumerable<Producto> ListaProductos { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ListaProductos = await productoServicio.GetListaProductos();
        }
    }
}

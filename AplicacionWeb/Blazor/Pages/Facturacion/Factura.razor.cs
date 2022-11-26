using Blazor.Interfaces;
using Blazor.Pages.MisProductos;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor.Pages.Facturacion
{
	public partial class Factura
	{
		[Inject] private IFacturaServicio facturaServicio { get; set; }
		[Inject] private IDetalleFacturaServicio detalleFacturaServicio { get; set; }
		[Inject] private IProductoServicio productoServicio { get; set;}
		[Inject] private NavigationManager navigationManager { get; set; }
		[Inject] private SweetAlertService Swal { get; set; }
		// Contiene el codigo de usuario que esta dentro de la sesion 
		[Inject] private IHttpContextAccessor httpContextAccessor { get; set; }

		private Factura factura = new Factura();
		private List<DetalleFactura> ListaDetalleFactura = new List<DetalleFactura>(); // Lista para almacenar los productos de la factura
		private Producto producto = new Producto();
		private string cantidadProductos { get; set; } // Almacena temporalmente la cantidad de productos a vender
		private string CodigoUsuario { get; set; } // Almacena el usuario que esta en la sesion.

		protected override async Task OnInitializedAsync()
		{
			// Extraer y almacenar el nombre/codigo del usuario que esta en la sesion.
			CodigoUsuario = httpContextAccessor.HttpContext.User.Identity.Name;
		}
	}
}

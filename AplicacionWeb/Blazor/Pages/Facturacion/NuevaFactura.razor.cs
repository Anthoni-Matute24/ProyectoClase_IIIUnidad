using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Modelos;

namespace Blazor.Pages.Facturacion
{
    public partial class NuevaFactura
    {
		[Inject] private IFacturaServicio facturaServicio { get; set; }
		[Inject] private IDetalleFacturaServicio detalleFacturaServicio { get; set; }
		[Inject] private IProductoServicio productoServicio { get; set; }
		[Inject] private NavigationManager navigationManager { get; set; }
		[Inject] private SweetAlertService Swal { get; set; }
		// Contiene el codigo de usuario que esta dentro de la sesion 
		[Inject] private IHttpContextAccessor httpContextAccessor { get; set; }

		private Factura factura = new Factura();
		private List<DetalleFactura> ListaDetalleFactura = new List<DetalleFactura>(); // Lista para almacenar los productos de la factura
		private Producto producto = new Producto();
		private int cantidadProductos { get; set; } // Almacena temporalmente la cantidad de productos a vender
		private string CodigoProducto { get; set; } // Almacena el codigo del producto para buscarlo entre los productos.

		protected override async Task OnInitializedAsync()
		{
			factura.Fecha = DateTime.Now;
		}

		private async void BuscarProducto()
		{
			producto = await productoServicio.GetPorCodigo(Convert.ToInt32(CodigoProducto));
		}
		protected async Task AgregarProducto(MouseEventArgs args)
		{
			if (args.Detail != 0)
			{
				if (producto != null)
				{
					DetalleFactura detalle = new DetalleFactura();
					detalle.Producto = producto.Descripcion;
					detalle.CodigoProducto = producto.Codigo;
					detalle.Cantidad = Convert.ToInt32(cantidadProductos);
					detalle.Precio = producto.Precio;
					detalle.Total = producto.Precio * Convert.ToInt32(cantidadProductos);
					ListaDetalleFactura.Add(detalle);

					producto.Codigo = 0;
					producto.Descripcion = string.Empty;
					producto.Precio = 0;
					producto.Existencia = 0;
					cantidadProductos = 0;
					CodigoProducto = "0";

					factura.Subtotal = factura.Subtotal + detalle.Total;
					factura.ISV = factura.Subtotal * 0.15M;
					factura.Total = factura.Subtotal + factura.ISV - factura.Descuento;
				}
			}
		}

		protected async Task Guardar()
		{
			factura.CodigoUsuario = httpContextAccessor.HttpContext.User.Identity.Name;
			int idFactura = await facturaServicio.NuevaFactura(factura);
			if (idFactura != 0)
			{
				foreach (var item in ListaDetalleFactura)
				{
					item.IdFactura = idFactura;
					await detalleFacturaServicio.NuevoDetalle(item);
				}
				await Swal.FireAsync("Correcto", "Factura guardada satisfactoriamente", SweetAlertIcon.Success);
			}
			else
			{
				await Swal.FireAsync("Error", "No se guardó la factura", SweetAlertIcon.Error);
			}
		}
	}
}

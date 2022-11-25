using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor.Pages.MisProductos
{
	public partial class NuevoProducto
	{
		[Inject] private IProductoServicio productoServicio { get; set; }

		private Producto products = new Producto();
		[Inject] private SweetAlertService Swal { get; set; }

		[Inject] NavigationManager _navigationManager { get; set; }

		string imgUrl = string.Empty;

		private async Task SeleccionarImagen(InputFileChangeEventArgs e)
		{
			IBrowserFile imgFile = e.File;
			var buffers = new byte[imgFile.Size];
			products.Imagen = buffers;
			await imgFile.OpenReadStream().ReadAsync(buffers);
			string imageType = imgFile.ContentType;
			imgUrl = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";
		}

		protected async Task Guardar()
		{
			// Validar que los campos: 'Codigo y Descripcion' no esten vacios
			if (string.IsNullOrEmpty(products.Codigo.ToString()) || string.IsNullOrEmpty(products.Descripcion))
			{
				return;
			}

			products.FechaCreacion = DateTime.Now; // Captura la fecha del sistema.
			Producto productoExistente = new Producto();

			productoExistente = await productoServicio.GetPorCodigo(products.Codigo);

			// Validar que el 'Codigo' de un producto no sea el mismo para varios productos
			if (productoExistente != null)
			{
				// Si extiste un producto con el mismo 'Codigo', mostrara una advertencia
				if (productoExistente.Codigo == products.Codigo)
				{
					await Swal.FireAsync("Advertencia", "Ya existe un producto con este código", SweetAlertIcon.Warning);
					return;
				}
			}

			bool inserto = await productoServicio.NuevoProducto(products);

			// Si no existe ningun problema en los campos de registro. Muestra una alerta de confirmacion
			if (inserto)
			{
				await Swal.FireAsync("Advertencia", "Producto guardado con exito", SweetAlertIcon.Success);
				_navigationManager.NavigateTo("/Productos");
			}
			else
			{   // De lo contrario, no se guardara el registro
				await Swal.FireAsync("Advertencia", "No se pudo guardar el producto", SweetAlertIcon.Error);
			}
		}
		protected async Task Cancelar()
		{
			_navigationManager.NavigateTo("/Productos");
		}
	}
}

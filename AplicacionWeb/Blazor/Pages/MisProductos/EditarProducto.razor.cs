using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Modelos;

namespace Blazor.Pages.MisProductos
{
    public partial class EditarProducto
    {
        [Inject] private IProductoServicio productoServicio { get; set; }
        private Producto products = new Producto();
        [Inject] private SweetAlertService Swal { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }

		string imgUrl = string.Empty; // Contiene el arreglo de bytes, que se pasara a la etiqueta 'img' del componenente 'razor'

		[Parameter] public string Codigo { get; set; }

        // Metodo para consultar el producto
        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Codigo))
            {
                products = await productoServicio.GetPorCodigo(Convert.ToInt32(Codigo));
            }
        }

		private async Task SeleccionarIMagen(InputFileChangeEventArgs e)
		{
			IBrowserFile imgFile = e.File; // imgFile: guarda el argumento/parametro/evento de la variable 'e'
			var buffers = new byte[imgFile.Size]; // buffers: es el que se registra en la BD. Se crea un nuevo [] y se pasa como parametro 'imgFile' 
			products.Imagen = buffers; // Al objeto 'prod' en la propiedad 'Imagen', se asigna el contenido de la variable 'buffers'
			await imgFile.OpenReadStream().ReadAsync(buffers); // imgFile genera el metodo 'OpenReadStream()' para leer el arreglo de bytes de 'buffers'. 
			string imageType = imgFile.ContentType; // Capturar el tipo de imagen a guardar
			imgUrl = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";
			/*
             * data: Tipo de imagen
             * base64: arreglo de 64 bits
             * {Convert.ToBase64String(buffers)}: Convierte el 'arreglo de bytes' en 'Imagen'
             */
		}

		protected async Task Guardar()
		{
			// Validar que los campos: 'Codigo y Descripcion' no esten vacios
			if (string.IsNullOrEmpty(products.Descripcion))
			{
				return;
			}

			products.FechaCreacion = DateTime.Now; // Captura la fecha del sistema.

			bool edito = await productoServicio.ActualizarProducto(products);

			// Si no existe ningun problema en los campos de registro. Muestra una alerta de confirmacion
			if (edito)
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

		protected async Task Eliminar()
		{
			bool elimino = false;

			SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
			{
				Title = "¿Seguro que desea eliminar el registro?",
				Icon = SweetAlertIcon.Question,
				ShowCancelButton = true,
				ConfirmButtonText = "Aceptar",
				CancelButtonText = "Cancelar"
			});

			if (!string.IsNullOrEmpty(result.Value))
			{
				elimino = await productoServicio.EliminarProducto(Convert.ToInt32(Codigo));

				if (elimino)
				{
					await Swal.FireAsync("Advertencia", "Producto Eliminado", SweetAlertIcon.Success);
					_navigationManager.NavigateTo("/Productos");
				}
				else
				{
					await Swal.FireAsync("Error", "No se pudo Eliminar el producto", SweetAlertIcon.Error);
				}
			}
		}
	}
}

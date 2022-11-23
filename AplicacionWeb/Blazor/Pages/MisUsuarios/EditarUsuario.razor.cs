using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Modelos;
using Org.BouncyCastle.Asn1.Mozilla;

namespace Blazor.Pages.MisUsuarios
{
    public partial class EditarUsuario
    {
        [Inject] private IUsuarioServicio usuarioServicio { get; set; }

        // Inyectar NavigationManager para poder regresar a la ruta de Usuarios y mostrarlos nuevamente 
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private SweetAlertService Swal { get; set; } // Servicio de Alerta para validar una acción

        private Usuario user = new Usuario(); // Instanciar objeto de la clase Usuario

        [Parameter] public string Codigo { get; set; }

         // Método para validar que si existe el registro/usuario
        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Codigo))
            {
                user = await usuarioServicio.GetPorCodigo(Codigo);
            }
        }

        protected async void Guardar()
        {
            // Validar que el usuario haya llenado todos los campos que sean obligatorios
            if (string.IsNullOrEmpty(user.Codigo) || string.IsNullOrEmpty(user.Nombre) || string.IsNullOrEmpty(user.Clave)
                || string.IsNullOrEmpty(user.Rol) || user.Rol == "Seleccionar")
            {
                return;
            }

            // Contiene los datos del nuevo usuario.
            bool edito = await usuarioServicio.ActualizarUsuario(user);

            if (edito)
            {
                await Swal.FireAsync("¡Bien hecho!", "¡Usuario modificado exitosamente!", SweetAlertIcon.Success);
            }
            else
            {
                await Swal.FireAsync("Hubo un eror", "No se modificó el usuario", SweetAlertIcon.Error);
            }

            navigationManager.NavigateTo("/Usuarios"); // Redirige a la lista de usuarios 
        }

        protected void Cancelar()
        {
            navigationManager.NavigateTo("/Usuarios"); // Redirige a la lista de usuarios 
        }

        protected async void EliminarUsuario()
        {
            bool elimino = false;

            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {   // Constructor
                Title = "¿Seguro que desea eliminar el registro?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = "Aceptar",
                CancelButtonText = "Cancelar"
            });

            // Validar si el usuario presionó el botón de aceptar.
            if (!string.IsNullOrEmpty(result.Value))
            {
                elimino = await usuarioServicio.EliminarUsuario(Codigo);

                if (elimino)
                {
                    await Swal.FireAsync("¡Bien hecho!", "¡Usuario eliminado exitosamente!", SweetAlertIcon.Success);
                    navigationManager.NavigateTo("/Usuarios"); // Redirige a la lista de usuarios 
                }
                else
                {
                    await Swal.FireAsync("Hubo un eror", "No se eliminó el usuario", SweetAlertIcon.Error);
                }
            }
        }
    }
}

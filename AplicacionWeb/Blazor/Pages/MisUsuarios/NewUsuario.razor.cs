using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor.Pages.MisUsuarios
{
    public partial class NewUsuario
    {
        [Inject] private IUsuarioServicio usuarioServicio { get; set; }

        // Inyectar NavigationManager para poder regresar a la ruta de Usuarios y mostrarlos nuevamente 
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private SweetAlertService Swal { get; set; } // Servicio de Alerta para validar una acción

        private Usuario user = new Usuario(); // Instanciar objeto de la clase Usuario

        protected async void Guardar()
        {
            // Validar que el usuario haya llenado todos los campos que sean obligatorios
            if (string.IsNullOrEmpty(user.Codigo) || string.IsNullOrEmpty(user.Nombre) || string.IsNullOrEmpty(user.Clave)
                || string.IsNullOrEmpty(user.Rol) || user.Rol == "Seleccionar")
            {
                return;
            }

            // Contiene los datos del nuevo usuario.
            bool inserto = await usuarioServicio.NuevoUsuario(user);

            if (inserto) 
            {
                await Swal.FireAsync("¡Bien hecho!", "¡Usuario guardado exitosamente!", SweetAlertIcon.Success);
            }
            else
            {
                await Swal.FireAsync("Hubo un eror", "No se guardó el usuario", SweetAlertIcon.Error);
            }

            navigationManager.NavigateTo("/Usuarios"); // Redirige a la lista de usuarios 
        }

        protected void Cancelar()
        {
            navigationManager.NavigateTo("/Usuarios"); // Redirige a la lista de usuarios 
        }
    }
}

// Valores estáticos para el ComboBox
enum Roles
{
    Selecccionar,
    Administrador,
    Usuario
}

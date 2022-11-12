using Modelos;

namespace Blazor.Interfaces
{
    public interface ILogInServicio
    {
        Task<bool> ValidarUsuario(LogIn logIn);
    }
}

using Modelos;

namespace Datos.Interfaces
{
    public interface ILogInRepositorio
    {
        Task<bool> ValidarUsuario(LogIn logIn);
    }
}

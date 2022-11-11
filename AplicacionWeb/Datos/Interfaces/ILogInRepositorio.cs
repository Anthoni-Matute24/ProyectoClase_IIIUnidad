using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos;

namespace Datos.Interfaces
{
    public interface ILogInRepositorio
    {
        Task<bool> ValidarUsuario(LogIn login);
    }
}

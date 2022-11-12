using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios
{
    // Implementar una interfaz dentro de una clase.
    public class LogInRepositorio : ILogInRepositorio
    {
        // Variable para capturar la información al llamar "ILogInRepositorio" en el servicio de Blazor
        private string CadenaConexion;

        // Constructor para pasar valores a "CadenaConexion"
        public LogInRepositorio(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion;
        }

        // Método para establecer una conexión de MySQL
        private MySqlConnection EstablecerConexion()
        {
            return new MySqlConnection(CadenaConexion); // Retorna un nuevo objeto de conexión
            // Contiene la IP del servidor, el puerto, la BD, la contraseña, el usuario, etc.
        }

        public async Task<bool> ValidarUsuario(LogIn login)
        {
            bool valido = false;
            try
            {
                // Abrir una nueva conexión 
                using MySqlConnection conexion = EstablecerConexion();
                await conexion.OpenAsync();
                string sql = "SELECT 1 FROM usuario WHERE Codigo = @Codigo AND Clave = @Clave;";
                valido = await conexion.ExecuteScalarAsync<bool>(sql, new {login.Codigo, login.Clave});
            }
            catch (Exception ex)
            {
            }
            return valido;
        }
    }
}

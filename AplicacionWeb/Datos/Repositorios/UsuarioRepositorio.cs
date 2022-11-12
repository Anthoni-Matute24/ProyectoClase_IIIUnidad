using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        // Variable para capturar la información al llamar "ILogInRepositorio" en el servicio de Blazor
        private string CadenaConexion;

        // Constructor para pasar valores a "CadenaConexion"
        public UsuarioRepositorio(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion;
        }

        // Método para establecer una conexión de MySQL
        private MySqlConnection EstablecerConexion()
        {
            return new MySqlConnection(CadenaConexion); // Retorna un nuevo objeto de conexión
            // Contiene la IP del servidor, el puerto, la BD, la contraseña, el usuario, etc.
        }

        public Task<bool> ActualizarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> GetLista(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> GetPorCodigo(string codigo)
        {
            Usuario user = new Usuario();
            try
            {
                // Abrir una nueva conexión 
                using MySqlConnection conexion = EstablecerConexion();
                await conexion.OpenAsync();
                string sql = "SELECT * FROM usuario WHERE Codigo = @Codigo;";
                // Trae un solo registro
                user = await conexion.QueryFirstAsync<Usuario>(sql, new { codigo });
            }
            catch (Exception ex)
            {
            }
            return user;
        }

        public Task<bool> NuevoUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}

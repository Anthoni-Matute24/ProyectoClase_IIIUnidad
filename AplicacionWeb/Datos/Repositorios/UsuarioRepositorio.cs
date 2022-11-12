using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<bool> ActualizarUsuario(Usuario usuario)
        {
            bool resultado = false;
            try
            {
                // Abrir una nueva conexión 
                using MySqlConnection conexion = EstablecerConexion();
                await conexion.OpenAsync();
                string sql = @"UPDATE usuario SET Nombre = @Nombre, CLave = @CLave, Correo = @Correo, ROL = @ROL, 
                               EstaActivo = @EstaActivo WHERE Codigo = @Codigo;";
                // Guardar los datos en caso de que se haya hecho un nuevo registro.
                resultado = Convert.ToBoolean(await conexion.ExecuteAsync(sql, usuario));
            }
            catch (Exception ex)
            {
            }
            return resultado;
        }

        public async Task<bool> EliminarUsuario(string codigo)
        {
            bool resultado = false;
            try
            {
                // Abrir una nueva conexión 
                using MySqlConnection conexion = EstablecerConexion();
                await conexion.OpenAsync();
                string sql = @"DELETE FROM usuario WHERE Codigo = @Codigo;";
                // Guardar los datos en caso de que se haya hecho un nuevo registro.
                resultado = Convert.ToBoolean(await conexion.ExecuteAsync(sql, new { codigo }));
            }
            catch (Exception ex)
            {
            }
            return resultado;
        }

        public async Task<IEnumerable<Usuario>> GetLista()
        {
           IEnumerable < Usuario > lista = new List<Usuario >();
            try
            {
                // Abrir una nueva conexión 
                using MySqlConnection conexion = EstablecerConexion();
                await conexion.OpenAsync();
                string sql = "SELECT * FROM usuario;";
                // Trae un solo registro
                lista = await conexion.QueryAsync<Usuario>(sql);
            }
            catch (Exception ex)
            {
            }
            return lista;
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

        public async Task<bool> NuevoUsuario(Usuario usuario)
        {
            bool resultado = false;
            try
            {
                // Abrir una nueva conexión 
                using MySqlConnection conexion = EstablecerConexion();
                await conexion.OpenAsync();
                string sql = "INSERT INTO usuario (Codigo, Nombre, CLave, Correo, ROL, EstaActivo) VALUES (@Codigo, @Nombre, @CLave, @Correo, @ROL, @EstaActivo);";
                // Guardar los datos en caso de que se haya hecho un nuevo registro.
                resultado = Convert.ToBoolean(await conexion.ExecuteAsync(sql, usuario));
            }
            catch (Exception ex)
            {
            }
            return resultado;
        }
    }
}

using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        // Variable para capturar la información al llamar "ILogInRepositorio" en el servicio de Blazor
        private string CadenaConexion;

        // Constructor para pasar valores a "CadenaConexion"
        public ProductoRepositorio(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion;
        }

        // Método para establecer una conexión de MySQL
        private MySqlConnection EstablecerConexion()
        {
            return new MySqlConnection(CadenaConexion); // Retorna un nuevo objeto de conexión
            // Contiene la IP del servidor, el puerto, la BD, la contraseña, el usuario, etc.
        }

        public async Task<bool> ActualizarProducto(Producto producto)
        {
            bool resultado = false;
            try
            {
                // Abrir una nueva conexión 
                using MySqlConnection conexion = EstablecerConexion();
                await conexion.OpenAsync();
                string sql = @"UPDATE producto SET Descripcion = @Descripcion, Existencia = @Existencia, Precio = @Precio, FechaCreacion = @FechaCreacion, 
                               Imagen = @Imagen WHERE Codigo = @Codigo;";
                // Guardar los datos en caso de que se haya hecho un nuevo registro.
                resultado = Convert.ToBoolean(await conexion.ExecuteAsync(sql, producto));
            }
            catch (Exception ex)
            {
            }
            return resultado;
        }

        public async Task<bool> EliminarProducto(int codigo)
        {
            bool resultado = false;
            try
            {
                // Abrir una nueva conexión 
                using MySqlConnection conexion = EstablecerConexion();
                await conexion.OpenAsync();
                string sql = @"DELETE FROM producto WHERE Codigo = @Codigo;";
                // Guardar los datos en caso de que se haya hecho un nuevo registro.
                resultado = Convert.ToBoolean(await conexion.ExecuteAsync(sql, new { codigo }));
            }
            catch (Exception ex)
            {
            }
            return resultado;
        }

        public async Task<IEnumerable<Producto>> GetListaProductos()
        {
            IEnumerable<Producto> lista = new List<Producto>();
            try
            {
                // Abrir una nueva conexión 
                using MySqlConnection conexion = EstablecerConexion();
                await conexion.OpenAsync();
                string sql = "SELECT * FROM producto;";
                // Trae un solo registro
                lista = await conexion.QueryAsync<Producto>(sql);
            }
            catch (Exception ex)
            {
            }
            return lista;
        }

        public async Task<Producto> GetPorCodigo(int codigo)
        {
            Producto producto = new Producto();
            try
            {
                // Abrir una nueva conexión 
                using MySqlConnection conexion = EstablecerConexion();
                await conexion.OpenAsync();
                string sql = "SELECT * FROM producto WHERE Codigo = @Codigo;";
                // Trae un solo registro
                producto = await conexion.QueryFirstAsync<Producto>(sql, new { codigo });
            }
            catch (Exception ex)
            {
            }
            return producto;
        }

        public async Task<bool> NuevoProducto(Producto producto)
        {
            bool resultado = false;
            try
            {
                // Abrir una nueva conexión 
                using MySqlConnection conexion = EstablecerConexion();
                await conexion.OpenAsync();
                string sql = @"INSERT INTO producto (Codigo, Descripcion, Existencia, Precio, FechaCreacion, Imagen) 
                                VALUES (@Codigo, @Descripcion, @Existencia, @Precio, @FechaCreacion, @Imagen);";
                // Guardar los datos en caso de que se haya hecho un nuevo registro.
                resultado = Convert.ToBoolean(await conexion.ExecuteAsync(sql, producto));
            }
            catch (Exception ex)
            {
            }
            return resultado;
        }
    }
}

using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios
{
	public class FacturaRepositorio : IFacturaRepositorio
	{
		// Variable para capturar la información al llamar "ILogInRepositorio" en el servicio de Blazor
		private string CadenaConexion;

		// Constructor para pasar valores a "CadenaConexion"
		public FacturaRepositorio(string _cadenaConexion)
		{
			CadenaConexion = _cadenaConexion;
		}

		// Método para establecer una conexión de MySQL
		private MySqlConnection EstablecerConexion()
		{
			return new MySqlConnection(CadenaConexion); // Retorna un nuevo objeto de conexión
			// Contiene la IP del servidor, el puerto, la BD, la contraseña, el usuario, etc.
		}

		public async Task<int> NuevaFactura(Factura factura)
		{
            int idFactura = 0;
            try
            {
                using MySqlConnection conexion = EstablecerConexion();
                await conexion.OpenAsync();
                string sql = @"INSERT INTO factura (IdentidadCliente, Fecha, CodigoUsuario, ISV, Descuento, Subtotal, Total) 
                               VALUES (@IdentidadCliente, @Fecha, @CodigoUsuario, @ISV, @Descuento, @Subtotal, @Total); SELECT LAST_INSERT_ID()";
                idFactura = Convert.ToInt32(await conexion.ExecuteScalarAsync(sql, factura));
            }
            catch (Exception ex)
            {
            }
            return idFactura;
        }
	}
}

using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios
{
	public class DetalleFacturaRepositorio : IDetalleFacturaRepositorio
	{
		// Variable para capturar la información al llamar "ILogInRepositorio" en el servicio de Blazor
		private string CadenaConexion;

		// Constructor para pasar valores a "CadenaConexion"
		public DetalleFacturaRepositorio(string _cadenaConexion)
		{
			CadenaConexion = _cadenaConexion;
		}

		// Método para establecer una conexión de MySQL
		private MySqlConnection EstablecerConexion()
		{
			return new MySqlConnection(CadenaConexion); // Retorna un nuevo objeto de conexión
			// Contiene la IP del servidor, el puerto, la BD, la contraseña, el usuario, etc.
		}

		public async Task<bool> NuevoDetalle(DetalleFactura detalleFactura)
		{
			bool inserto = false;
			try
			{
				// Abrir una nueva conexión 
				using MySqlConnection conexion = EstablecerConexion();
				await conexion.OpenAsync();
				string sql = @"INSERT INTO detallefactura (IdFactura, CodigoProducto, Precio, Cantidad, Total) 
                                VALUES (@IdFactura, @CodigoProducto, @Precio, @Cantidad, @Total);";
				// Guardar los datos en caso de que se haya hecho un nuevo registro.
				inserto = Convert.ToBoolean(await conexion.ExecuteAsync(sql, detalleFactura));
			}
			catch (Exception ex)
			{
			}
			return inserto;
		}
	}
}

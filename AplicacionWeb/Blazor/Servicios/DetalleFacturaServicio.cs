using Blazor.Interfaces;
using Datos.Interfaces;
using Datos.Repositorios;
using Modelos;

namespace Blazor.Servicios
{
	public class DetalleFacturaServicio : IDetalleFacturaServicio
	{
		// Solo permite leer el contenido de la propiedad "_configuracion"
		private readonly Config _configuracion;

		// Llamar la Interfaz del Repositorio
		private IDetalleFacturaRepositorio detalleFacturaRepositorio; // Nombre Objeto: logInRepositorio

		public DetalleFacturaServicio(Config config) // Constructor
		{
			_configuracion = config;
			// Configuración del servicio
			// Del objeto "Config" se toma la CadenaConexion
			detalleFacturaRepositorio = new DetalleFacturaRepositorio(config.CadenaConexion);
		}


		public async Task<bool> NuevoDetalle(DetalleFactura detalleFactura)
		{
			return await detalleFacturaRepositorio.NuevoDetalle(detalleFactura);
		}
	}
}

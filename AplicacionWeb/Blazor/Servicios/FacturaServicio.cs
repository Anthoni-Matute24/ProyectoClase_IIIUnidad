using Blazor.Interfaces;
using Datos.Interfaces;
using Datos.Repositorios;
using Modelos;

namespace Blazor.Servicios
{
	public class FacturaServicio : IFacturaServicio
	{
		// Solo permite leer el contenido de la propiedad "_configuracion"
		private readonly Config _configuracion;

		// Llamar la Interfaz del Repositorio
		private IFacturaRepositorio facturaRepositorio; // Nombre Objeto: logInRepositorio

		public FacturaServicio(Config config) // Constructor
		{
			_configuracion = config;
			// Configuración del servicio
			// Del objeto "Config" se toma la CadenaConexion
			facturaRepositorio = new FacturaRepositorio(config.CadenaConexion);
		}

		public async Task<int> NuevaFactura(Factura factura)
		{
			return await facturaRepositorio.NuevaFactura(factura);
		}
	}
}

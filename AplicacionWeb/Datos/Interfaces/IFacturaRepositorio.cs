using Modelos;

namespace Datos.Interfaces
{
	public interface IFacturaRepositorio
	{
		Task<int> NuevaFactura(Factura factura);
	}
}

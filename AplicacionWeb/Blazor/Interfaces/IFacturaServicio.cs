using Modelos;

namespace Blazor.Interfaces
{
	public interface IFacturaServicio
	{
		Task<int> NuevaFactura(Factura factura);
	}
}

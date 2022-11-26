using Modelos;

namespace Blazor.Interfaces
{
	public interface IDetalleFacturaServicio
	{
		Task<bool> NuevoDetalle(DetalleFactura detalleFactura);
	}
}

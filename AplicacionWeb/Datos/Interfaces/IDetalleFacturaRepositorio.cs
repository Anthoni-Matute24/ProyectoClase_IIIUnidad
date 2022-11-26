using Modelos;

namespace Datos.Interfaces
{
	public interface IDetalleFacturaRepositorio
	{
		Task<bool> NuevoDetalle(DetalleFactura detalleFactura);
	}
}

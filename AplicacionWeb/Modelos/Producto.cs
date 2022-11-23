using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class Producto
    {
        // Propiedades

        [Required(ErrorMessage = "El código es obligatorio")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La existencia es obligatoria")]
        public int Existencia { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria")]
        public DateTime FechaCreacion { get; set; }
        public byte[]? Imagen { get; set; }
    }
}

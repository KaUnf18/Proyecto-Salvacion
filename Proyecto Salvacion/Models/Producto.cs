using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Salvacion.Models
{
    public class Producto
    {
        public int Id { get; set; } // Clave primaria
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }
        public ICollection<ReservaProducto> ReservaProductos { get; set; }
    }

}


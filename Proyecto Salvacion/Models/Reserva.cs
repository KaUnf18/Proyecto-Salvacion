using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Salvacion.Models
{
    public class Reserva
    {
        public int Id { get; set; } // Clave primaria
        public string ClienteNombre { get; set; }
        public string ClienteCorreo { get; set; }
        public DateTime FechaReserva { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PagoAdelantado { get; set; } // 25% del total

        [Column(TypeName = "decimal(18,2)")]
        public decimal PagoTotal { get; set; }
        public bool Confirmado { get; set; } // Si se completó el pago total
        public ICollection<ReservaProducto> ReservaProductos { get; set; }
    }


}

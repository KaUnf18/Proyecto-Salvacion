namespace Proyecto_Salvacion.Models
{
    public class ReservaProducto
    {
        public int ReservaId { get; set; } // Parte de la clave primaria compuesta
        public Reserva Reserva { get; set; }

        public int ProductoId { get; set; } // Parte de la clave primaria compuesta
        public Producto Producto { get; set; }

        public int Cantidad { get; set; } // Cantidad de este producto en la reserva
    }
}

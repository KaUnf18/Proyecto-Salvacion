using System.ComponentModel.DataAnnotations;

namespace Proyecto_Salvacion.Models
{
    public class Rol
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre del rol no puede tener más de 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción del rol es obligatoria")]
        [StringLength(250, ErrorMessage = "La descripción no puede tener más de 250 caracteres")]
        public string Descripcion { get; set; }
    }

}


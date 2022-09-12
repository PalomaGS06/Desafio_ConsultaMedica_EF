using System.ComponentModel.DataAnnotations;

namespace ConsultaMedicaVet.Models
{
    public class TipoUsuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tipo { get; set; }
    }
}

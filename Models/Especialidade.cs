using System.ComponentModel.DataAnnotations;

namespace ConsultaMedicaVet.Models
{
    public class Especialidade
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Categoria { get; set; }


    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaMedicaVet.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        [ForeignKey("TipoUsuario")]
        public int IdTipoUsuario { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public virtual ICollection<Medico> Medico { get; set; }
        public virtual ICollection<Paciente> Paciente { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaMedicaVet.Models
{
    public class Consulta
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DataHora { get; set; }

        [ForeignKey("Medico")]
        public int IdMedico { get; set; }
        public Medico Medico { get; set; }

        [ForeignKey("Paciente")]
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsultaMedicaVet.Models
{
    public class Especialidade
    {
        // Na classe Model, haverá todos os atributos/colunas que compõe a classe Consulta

        [Key]  // primary key
        public int Id { get; set; }
        [Required]  // campo obrigatório
        public string Categoria { get; set; }

        public virtual ICollection<Medico> Medico { get; set; }  // lista de médicos 


    }
}

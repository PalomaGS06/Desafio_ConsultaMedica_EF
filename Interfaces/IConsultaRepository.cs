using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConsultaMedicaVet.Interfaces
{
    public interface IConsultaRepository
    {
        Consulta Inserir(Consulta consultas);
        ICollection<Consulta> ListarTodas();
        Consulta BuscarPorId(int id);
        void Alterar(Consulta consultas);
        void Excluir(Consulta consultas);
        void AlterarParcialmente(JsonPatchDocument patchConsulta, Consulta consultas);
    }
}

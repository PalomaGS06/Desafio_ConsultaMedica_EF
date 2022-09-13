using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConsultaMedicaVet.Interfaces
{
    public interface IEspecialidadeRepository
    {
        Especialidade Inserir(Especialidade especialidade);
        ICollection<Especialidade> ListarTodas();
        Especialidade BuscarPorId(int id);
        void Alterar(Especialidade especialidade);
        void Excluir(Especialidade especialidade);
        void AlterarParcialmente(JsonPatchDocument patchEspecialidade, Especialidade especialidade);
    }
}

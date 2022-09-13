using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConsultaMedicaVet.Interfaces
{
    public interface IPacienteRepository
    {
        Paciente Inserir(Paciente paciente);
        ICollection<Paciente> ListarTodos();
        Paciente BuscarPorId(int id);
        void Alterar(Paciente paciente);
        void Excluir(Paciente paciente);
        void AlterarParcialmente(JsonPatchDocument patchPaciente, Paciente paciente);
    }
}

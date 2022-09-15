using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConsultaMedicaVet.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario Inserir(Usuario usuario);
        ICollection<Usuario> ListarTodosUsers();
        ICollection<Usuario> ListarMedicosUsers();
        ICollection<Usuario> ListarPacientesUsers();
        Usuario BuscarPorId(int id);
        void Alterar(Usuario usuario);
        void Excluir(Usuario usuario);
        void AlterarParcialmente(JsonPatchDocument patchUsuario, Usuario usuario);
    }
}

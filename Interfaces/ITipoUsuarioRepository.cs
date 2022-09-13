using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConsultaMedicaVet.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        TipoUsuario Inserir(TipoUsuario tipoUsuario);
        ICollection<TipoUsuario> ListarTodos();
        TipoUsuario BuscarPorId(int id);
        void Alterar(TipoUsuario tipoUsuario);
        void Excluir(TipoUsuario tipoUsuario);
        void AlterarParcialmente(JsonPatchDocument patchTipoUsuario, TipoUsuario tipoUsuario);

    }
}

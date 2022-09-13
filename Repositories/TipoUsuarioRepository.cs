using ConsultaMedicaVet.Contexts;
using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaMedicaVet.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        // Injeção de Dependência
        ConsultaMedVetContext ctx; //criando a dependência de contexto
        public TipoUsuarioRepository(ConsultaMedVetContext _ctx) //método construtor
        {
            ctx = _ctx;
        }

        public void Alterar(TipoUsuario tipoUsuario)
        {
            ctx.Entry(tipoUsuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchTipoUsuario, TipoUsuario tipoUsuario)
        {
            patchTipoUsuario.ApplyTo(tipoUsuario);
            ctx.Entry(tipoUsuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public TipoUsuario BuscarPorId(int id)
        {
            return ctx.TipoUsuario.Find(id);
        }

        public void Excluir(TipoUsuario tipoUsuario)
        {
            ctx.TipoUsuario.Remove(tipoUsuario);
            ctx.SaveChanges();
        }

        public TipoUsuario Inserir(TipoUsuario tipoUsuario)
        {
            ctx.TipoUsuario.Add(tipoUsuario);
            ctx.SaveChanges();
            return tipoUsuario;
        }

        public ICollection<TipoUsuario> ListarTodos()
        {
            return ctx.TipoUsuario.ToList();
        }
    }
}

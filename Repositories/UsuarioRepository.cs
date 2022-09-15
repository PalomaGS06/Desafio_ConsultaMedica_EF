using ConsultaMedicaVet.Contexts;
using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaMedicaVet.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        // Injeção de Dependência
        ConsultaMedVetContext ctx; //criando a dependência de contexto
        public UsuarioRepository(ConsultaMedVetContext _ctx) //método construtor
        {
            ctx = _ctx;
        }

        public void Alterar(Usuario usuario)
        {
            ctx.Entry(usuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchUsuario, Usuario usuario)
        {
            patchUsuario.ApplyTo(usuario);
            ctx.Entry(usuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Usuario BuscarPorId(int id)
        {
            return ctx.Usuarios.Find(id);
        }

        public void Excluir(Usuario usuario)
        {
            ctx.Usuarios.Remove(usuario);
            ctx.SaveChanges();
        }

        public Usuario Inserir(Usuario usuario)
        {
            ctx.Usuarios.Add(usuario);
            ctx.SaveChanges();
            return usuario;
        }

        public ICollection<Usuario> ListarTodosUsers()
        {
            return ctx.Usuarios.ToList();
        }

        public ICollection<Usuario> ListarMedicosUsers()
        {
            var medicos = ctx.Usuarios
                   .Include(m => m.Medico)
                   .ThenInclude(es => es.Especialidade)
                   .Where(m => m.IdTipoUsuario == 1)
                   .ToList();

            return medicos;
        }

        public ICollection<Usuario> ListarPacientesUsers()
        {
            var pacientes = ctx.Usuarios
                   .Include(p => p.Medico)
                   .Where(p => p.IdTipoUsuario == 2)
                   .ToList();

            return pacientes;
        }
    }
}

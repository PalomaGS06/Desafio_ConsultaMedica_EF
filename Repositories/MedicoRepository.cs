using ConsultaMedicaVet.Contexts;
using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaMedicaVet.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {

        // Injeção de Dependência
        ConsultaMedVetContext ctx; //criando a dependência de contexto
        public MedicoRepository(ConsultaMedVetContext _ctx) //método construtor criado
        {
            ctx = _ctx;
        }


        public void Alterar(Medico medico)
        {
            ctx.Entry(medico).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchMedico, Medico medico)
        {
            patchMedico.ApplyTo(medico);
            ctx.Entry(medico).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Medico BuscarPorId(int id)
        {
            var medicoId = ctx.Medico
               .Include(e => e.IdEspecialidade)
               .Include(u => u.IdUsuario)
               .FirstOrDefault(m => m.Id == id);

            return medicoId;
        }

        public void Excluir(Medico medico)
        {
            ctx.Medico.Remove(medico);
            var usuarioM = ctx.Usuarios.Find(medico.IdUsuario);
            ctx.Usuarios.Remove(usuarioM);
            ctx.SaveChanges();
        }

        public Medico Inserir(Medico medico)
        {
            ctx.Medico.Add(medico);
            ctx.SaveChanges();
            return medico;
        }

        public ICollection<Medico> ListarTodos()
        {
            var consultas = ctx.Medico
                    .Include(c => c.Consulta)
                    .ToList();

            return consultas;
        }
    }
}

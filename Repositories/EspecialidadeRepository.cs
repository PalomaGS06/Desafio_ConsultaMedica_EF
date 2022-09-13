using ConsultaMedicaVet.Contexts;
using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaMedicaVet.Repositories
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        // Injeção de Dependência
        ConsultaMedVetContext ctx; //criando a dependência de contexto
        public EspecialidadeRepository(ConsultaMedVetContext _ctx) //cria-se um método construtor
        {
            ctx = _ctx;
        }
        public void Alterar(Especialidade especialidade)
        {
            ctx.Entry(especialidade).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchEspecialidade, Especialidade especialidade)
        {
            patchEspecialidade.ApplyTo(especialidade);
            ctx.Entry(especialidade).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Especialidade BuscarPorId(int id)
        {
            return ctx.Especialidade.Find(id);
        }

        public void Excluir(Especialidade especialidade)
        {
            ctx.Especialidade.Remove(especialidade);
            ctx.SaveChanges();
        }

        public Especialidade Inserir(Especialidade especialidade)
        {
            ctx.Especialidade.Add(especialidade);
            ctx.SaveChanges();
            return especialidade;
        }

        public ICollection<Especialidade> ListarTodas()
        {
            return ctx.Especialidade.ToList(); // para listar todas as especialidades, é utilizada a biblioteca Linq
        }
    }
}

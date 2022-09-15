using ConsultaMedicaVet.Contexts;
using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaMedicaVet.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        // Injeção de Dependência
        ConsultaMedVetContext ctx; //criando a dependência de contexto
        public ConsultaRepository(ConsultaMedVetContext _ctx) //método construtor gerado
        {
            ctx = _ctx;
        }

        public void Alterar(Consulta consultas)
        {
            ctx.Entry(consultas).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchConsulta, Consulta consultas)
        {

            patchConsulta.ApplyTo(consultas);
            ctx.Entry(consultas).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Consulta BuscarPorId(int id)
        {
            return ctx.Consultas.Find(id);
        }

        public void Excluir(Consulta consultas)
        {
            ctx.Consultas.Remove(consultas);
            ctx.SaveChanges(); 
        }

        public Consulta Inserir(Consulta consultas)
        {
            ctx.Consultas.Add(consultas);
            ctx.SaveChanges();
            return consultas;
        }

        public ICollection<Consulta> ListarTodas()
        {
            var consultar = ctx.Consultas
                .Include(p => p.IdPaciente)
                .Include(m => m.IdMedico)
                .ToList();

            return consultar;
        }
    }
}

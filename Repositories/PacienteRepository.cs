﻿using ConsultaMedicaVet.Contexts;
using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaMedicaVet.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        // Injeção de Dependência
        ConsultaMedVetContext ctx; //criando a dependência de contexto
        public PacienteRepository(ConsultaMedVetContext _ctx) //método construtor
        {
            ctx = _ctx;
        }
        public void Alterar(Paciente paciente)
        {
            ctx.Entry(paciente).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchPaciente, Paciente paciente)
        {
            patchPaciente.ApplyTo(paciente);
            ctx.Entry(paciente).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Paciente BuscarPorId(int id)
        {
            var pacienteId = ctx.Paciente
               .Include(u => u.IdUsuario)
               .FirstOrDefault(m => m.Id == id);

            return pacienteId;
        }

        public void Excluir(Paciente paciente)
        {
            ctx.Paciente.Remove(paciente);
            var usuarioP = ctx.Usuarios.Find(paciente.IdUsuario);
            ctx.Usuarios.Remove(usuarioP);
            ctx.SaveChanges();
        }

        public Paciente Inserir(Paciente paciente)
        {
            ctx.Paciente.Add(paciente);
            ctx.SaveChanges();
            return paciente;
        }

        public ICollection<Paciente> ListarTodos()
        {
            var consultas = ctx.Paciente
                   .Include(c => c.Consulta)
                   .Include(u => u.Usuario)
                   .ToList();

            return consultas;
        }
    }
}

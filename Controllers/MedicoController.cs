﻿using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaMedicaVet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly IMedicoRepository repositorio;

        public MedicoController(IMedicoRepository _repositorio) //criando um construtor
        {
            repositorio = _repositorio;
        }

        //verbo POST - Inserir/Cadastrar

        /// <summary>
        /// Cadastra/Inclui médicos e seus respectivos Ids
        /// </summary>
        /// <param name="medico"> Dados dos Médicos</param>
        /// <returns>Médico cadastrado!</returns>
        [HttpPost]
        public IActionResult Cadastrar(Medico medico)
        {

            try
            {
                medico.Usuario.IdTipoUsuario = 1;   // O médico sempre será com o Id 1, não importando qual valor o usuario digitar
                var retorno = repositorio.Inserir(medico);
                return Ok(retorno);

            }
            catch (System.Exception e)
            {        
                return StatusCode(500, new
                {
                    Error = "Falha na transação...",
                    Message = e.Message,
                });
            }

        }

        //verbo GET - Buscar/Listar

        /// <summary>
        /// Lista/Busca todos os médicos existentes no BD
        /// </summary>
        /// <returns>Lista de Médicos com consultas</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var retorno = repositorio.ListarTodos();
                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha de transação...",
                    Message = e.Message,
                });

            }

        }

        //verbo GET - Buscar/Listar por ID

        /// <summary>
        /// Lista o médico por meio de seu Id
        /// </summary>
        /// <param name="id">Dados do médico selecionado</param>
        /// <returns>Médico listado pelo ID</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarMedicoPorID(int id)
        {
            try
            {
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Médico não achado na lista..."
                    });
                }

                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação...",
                    Message = e.Message,
                });
            }
        }

        //verbo PUT - Alterar/Atualizar

        /// <summary>
        /// Altera os dados do médico
        /// </summary>
        /// <param name="id">Id do médico </param>
        /// <param name="medico">Dados do médico alterado</param>
        /// <returns>Médico alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Medico medico)
        {
            try
            {

                //Verificar se os ids batem!
                if (id != medico.Id)
                {
                    return BadRequest();
                }

                //Verificar se o id existe no banco!
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Médico não encontrado..."
                    });
                }

                //Altera efetivamente o médico!
                repositorio.Alterar(medico);

                return NoContent();

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação...",
                    Message = e.Message,
                });
            }

        }

        //verbo PATCH - Alterar parcialmente

        /// <summary>
        /// Altera alguns dos dados do médico
        /// </summary>
        /// <param name="id">Id selecionado para alteração</param>
        /// <param name="patchMedico">Dado alterado</param>
        /// <returns>Médico alterado</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchMedico)
        {
            if (patchMedico == null)
            {
                return BadRequest();
            }

            // Temos que buscar o objeto
            var medico = repositorio.BuscarPorId(id); //médico encontrado
            if (medico == null)
            {
                return NotFound(new
                {
                    Message = "Médico não encontrado..."
                });
            }

            repositorio.AlterarParcialmente(patchMedico, medico);
            return Ok(medico);
        }

        //verbo DELETE - Excluir

        /// <summary>
        /// Deletar médico através de seu Id
        /// </summary>
        /// <param name="id">Id selecionado para exclusão</param>
        /// <returns>Mensagem de exclusão</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                var busca = repositorio.BuscarPorId(id);
                if (busca == null)
                {
                    return NotFound(new
                    {
                        Message = "Médico não encontrado..."
                    });
                }

                repositorio.Excluir(busca);

                return NoContent(); // Status 204 de sucesso

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação...",
                    Message = e.Message,
                });
            }

        }
    }
}

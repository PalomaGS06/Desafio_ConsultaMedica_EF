using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaMedicaVet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadeController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly IEspecialidadeRepository repositorio;

        public EspecialidadeController(IEspecialidadeRepository _repositorio) //criando um construtor
        {
            repositorio = _repositorio;
        }

        [HttpPost]
        public IActionResult Cadastrar(Especialidade especialidade)
        {

            try
            {
                var retorno = repositorio.Inserir(especialidade);
                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                // return BadRequest(e.Message);            
                return StatusCode(500, new
                {
                    Error = "Falha na transação !!",
                    Message = e.Message,
                });
            }

        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var retorno = repositorio.ListarTodas();
                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha no sistema !!",
                    Message = e.Message,
                });

            }

        }
        [HttpGet("{id}")]
        public IActionResult BuscarEspecialidadePorID(int id)
        {
            try
            {
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Especialidade não encontrada na lista !!"
                    });
                }

                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação !!",
                    Message = e.Message,
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Especialidade especialidade)
        {
            try
            {

                //Verificar se os ids batem!
                if (id != especialidade.Id)
                {
                    return BadRequest();
                }

                //Verificar se o id existe no banco!
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Especialidade não encontrada !!"
                    });
                }

                //Altera efetivamente a especialidade!
                repositorio.Alterar(especialidade);

                return NoContent();

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação !!",
                    Message = e.Message,
                });
            }

        }


        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchEspecialidade)
        {
            if (patchEspecialidade == null)
            {
                return BadRequest();
            }

            // Temos que buscar o objeto
            var especialidade = repositorio.BuscarPorId(id); //especialidade encontrada
            if (especialidade == null)
            {
                return NotFound(new
                {
                    Message = "Especialidade não encontrada !!"
                });
            }

            repositorio.AlterarParcialmente(patchEspecialidade, especialidade);
            return Ok(especialidade);
        }

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
                        Message = "Especialidade não encontrada !!"
                    });
                }

                repositorio.Excluir(busca);

                return NoContent(); // Status 204 de sucesso

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação!!",
                    Message = e.Message,
                });
            }

        }

    }
}

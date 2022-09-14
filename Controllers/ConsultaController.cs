using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaMedicaVet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly IConsultaRepository repositorio;

        public ConsultaController(IConsultaRepository _repositorio) //criando um construtor
        {
            repositorio = _repositorio;
        }

        [HttpPost]
        public IActionResult Cadastrar(Consulta consulta)
        {

            try
            {
                var retorno = repositorio.Inserir(consulta);
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
                    Error = "Falha de transação !!",
                    Message = e.Message,
                });

            }

        }
        [HttpGet("{id}")]
        public IActionResult BuscarConsultaPorID(int id)
        {
            try
            {
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Consulta não achada na lista !!"
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
        public IActionResult Alterar(int id, Consulta consulta)
        {
            try
            {

                //Verificar se os ids batem!
                if (id != consulta.Id)
                {
                    return BadRequest();
                }

                //Verificar se o id existe no banco!
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Consulta não encontrada !!"
                    });
                }

                //Altera efetivamente a consulta!
                repositorio.Alterar(consulta);

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
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchConsulta)
        {
            if (patchConsulta == null)
            {
                return BadRequest();
            }

            // Temos que buscar o objeto
            var consulta = repositorio.BuscarPorId(id); //consulta encontrada
            if (consulta == null)
            {
                return NotFound(new
                {
                    Message = "Consulta não encontrada !!"
                });
            }

            repositorio.AlterarParcialmente(patchConsulta, consulta);
            return Ok(consulta);
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
                        Message = "Consulta não encontrada !!"
                    });
                }

                repositorio.Excluir(busca);

                return NoContent(); // Sucesso

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

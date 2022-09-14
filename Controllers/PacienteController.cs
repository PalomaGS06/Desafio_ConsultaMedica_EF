using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaMedicaVet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly IPacienteRepository repositorio;

        public PacienteController(IPacienteRepository _repositorio) //criando um construtor
        {
            repositorio = _repositorio;
        }

        [HttpPost]
        public IActionResult Cadastrar(Paciente paciente)
        {

            try
            {
                var retorno = repositorio.Inserir(paciente);
                return Ok(retorno);

            }
            catch (System.Exception e)
            {
       
                return StatusCode(500, new
                {
                    Error = "Falha na transação!",
                    Message = e.Message,
                });
            }

        }

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
                    Error = "Falha de transação!",
                    Message = e.Message,
                });

            }

        }
        [HttpGet("{id}")]
        public IActionResult BuscarPacientePorID(int id)
        {
            try
            {
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Paciente não achado na lista!"
                    });
                }

                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação!",
                    Message = e.Message,
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Paciente paciente)
        {
            try
            {

                //Verificar se os ids batem!
                if (id != paciente.Id)
                {
                    return BadRequest();
                }

                //Verificar se o id existe no banco!
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Paciente não encontrado!"
                    });
                }

                //Altera efetivamente o paciente!
                repositorio.Alterar(paciente);

                return NoContent();

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação!",
                    Message = e.Message,
                });
            }

        }


        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchPaciente)
        {
            if (patchPaciente == null)
            {
                return BadRequest();
            }

            // Temos que buscar o objeto
            var paciente = repositorio.BuscarPorId(id); //paciente encontrado
            if (paciente == null)
            {
                return NotFound(new
                {
                    Message = "Paciente não encontrado!"
                });
            }

            repositorio.AlterarParcialmente(patchPaciente, paciente);
            return Ok(paciente);
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
                        Message = "Paciente não encontrado!"
                    });
                }

                repositorio.Excluir(busca);

                return NoContent(); // Status 204 de sucesso

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação!",
                    Message = e.Message,
                });
            }

        }
    }
}

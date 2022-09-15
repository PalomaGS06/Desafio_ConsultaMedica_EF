using ConsultaMedicaVet.Interfaces;
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

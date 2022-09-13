using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaMedicaVet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly ITipoUsuarioRepository repositorio;

        public TipoUsuarioController(ITipoUsuarioRepository _repositorio) //criando um construtor
        {
            repositorio = _repositorio;
        }

        [HttpPost]
        public IActionResult Cadastrar(TipoUsuario tipoUsuario)
        {

            try
            {
                var retorno = repositorio.Inserir(tipoUsuario);
                return Ok(retorno);

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
                    Error = "Falha no servidor!!",
                    Message = e.Message,
                });

            }

        }
        [HttpGet("{id}")]
        public IActionResult BuscarTipoUsuarioPorID(int id)
        {
            try
            {
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Tipo de usuario não encontrado na lista!!"
                    });
                }

                return Ok(retorno);

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

        [HttpPut("{id}")]
        public IActionResult Alterar(int id, TipoUsuario tipoUsuario)
        {
            try
            {

                //Verificar se os ids batem!
                if (id != tipoUsuario.Id)
                {
                    return BadRequest();
                }

                //Verificar se o id existe no banco!
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Tipo de usuario não encontrado!!"
                    });
                }

                //Altera efetivamente o tipo!
                repositorio.Alterar(tipoUsuario);

                return NoContent();

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha no servidor!!",
                    Message = e.Message,
                });
            }

        }


        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchTipoUsuario)
        {
            if (patchTipoUsuario == null)
            {
                return BadRequest();
            }

            // Temos que buscar o objeto
            var tipoUsuario = repositorio.BuscarPorId(id); //tipoUsuario encontrado
            if (tipoUsuario == null)
            {
                return NotFound(new
                {
                    Message = "Tipo de usuário não encontrado!!"
                });
            }

            repositorio.AlterarParcialmente(patchTipoUsuario, tipoUsuario);
            return Ok(tipoUsuario);
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
                        Message = "Tipo de usuário não encontrado!!"
                    });
                }

                repositorio.Excluir(busca);

                return NoContent(); // Status 204 de sucesso

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha no servidor!!",
                    Message = e.Message,
                });
            }

        }
    }
}

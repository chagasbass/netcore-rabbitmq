using Microsoft.AspNetCore.Mvc;
using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Entidades;
using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Servicos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreMicroservices.Banco.Api.Contextos.ContasCorrentes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IContaCorrenteServico ContaCorrenteServico;

        public ContaCorrenteController(IContaCorrenteServico contaCorrenteServico)
        {
            ContaCorrenteServico = contaCorrenteServico;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContaCorrente>>> ListarContas()
        {
            var contas = await ContaCorrenteServico.ListarContasCorrenteAsync();

            return Ok(contas);
        }

        [HttpPost]
        public async Task<ActionResult<Transferencia>> EfetuarTransferencia([FromBody] Transferencia transferencia)
        {
            await ContaCorrenteServico.TransferirFundosAsync(transferencia);

            return Ok(transferencia);
        }

        //    [HttpPost]
        //    [Route("")]
        //    [Consumes(MediaTypeNames.Application.Json)]
        //    [ProducesResponseType(StatusCodes.Status201Created)]
        //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    public async Task<ActionResult<IComandoResultado>> SalvarCategoria([FromBody] CadastrarCategoriaComando criarCategoriaComando)
        //    {
        //        var usuario = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        //        criarCategoriaComando.Usuario = usuario;

        //        var retorno = (ComandoResultado)await _barramentoDeComandos.EnviarComando(criarCategoriaComando);

        //        if (!retorno.Sucesso)
        //            return BadRequest(retorno);

        //        return Created("v1/Categorias", retorno);
        //    }

        //    [HttpPut]
        //    [Route("")]
        //    [Consumes(MediaTypeNames.Application.Json)]
        //    [ProducesResponseType(StatusCodes.Status204NoContent)]
        //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    public async Task<ActionResult<IComandoResultado>> AtualizarCategoria([FromBody] AtualizarCategoriaComando atualizarCategoriaComando)
        //    {
        //        var usuario = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        //        atualizarCategoriaComando.Usuario = usuario;

        //        var retorno = (ComandoResultado)await _barramentoDeComandos.EnviarComando(atualizarCategoriaComando);

        //        if (!retorno.Sucesso)
        //            return BadRequest(retorno);

        //        return NoContent();
        //    }

        //    [HttpDelete]
        //    [Route("")]
        //    [Consumes(MediaTypeNames.Application.Json)]
        //    [ProducesResponseType(StatusCodes.Status200OK)]
        //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    public async Task<ActionResult<IComandoResultado>> ExcluirCategoria([FromBody] ExcluirCategoriaComando excluirCategoriaComando)
        //    {
        //        var usuario = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        //        excluirCategoriaComando.Usuario = usuario;

        //        var retorno = (ComandoResultado)await _barramentoDeComandos.EnviarComando(excluirCategoriaComando);

        //        if (!retorno.Sucesso)
        //            return BadRequest(retorno);

        //        return Ok(retorno);
        //    }
        //}
    }
}

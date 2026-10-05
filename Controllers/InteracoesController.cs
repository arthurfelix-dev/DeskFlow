using DeskFlow.API.Models.Entities;
using DeskFlow.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.API.Controllers;

[ApiController]
[Route("api/chamados/{chamadoId}/interacoes")]
public class InteracoesController : ControllerBase
{
    private readonly InteracaoService _service;

    public InteracoesController(InteracaoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<Interacao>> Criar(
        int chamadoId,
        [FromBody] Interacao interacao)
    {
        var novaInteracao = await _service.CriarAsync(
            chamadoId,
            interacao);

        if (novaInteracao == null)
        {
            return BadRequest(
                "Não foi possível criar a interação. Verifique se o chamado existe e se não está fechado.");
        }

        return Ok(novaInteracao);
    }
}
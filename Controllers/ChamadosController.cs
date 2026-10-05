using DeskFlow.API.Models.Entities;
using DeskFlow.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.API.Controllers;

[ApiController]
[Route("api/chamados")]
public class ChamadosController : ControllerBase
{
    private readonly ChamadoService _service;

    public ChamadosController(ChamadoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Chamado>>> Listar(
        [FromQuery] string? status,
        [FromQuery] string? prioridade,
        [FromQuery] int? categoriaId)
    {
        var chamados = await _service.ListarTodosAsync(
            status,
            prioridade,
            categoriaId);

        return Ok(chamados);
    }

    [HttpPost]
    public async Task<ActionResult<Chamado>> Criar([FromBody] Chamado chamado)
    {
        var novoChamado = await _service.CriarAsync(chamado);

        return CreatedAtAction(
            nameof(BuscarPorId),
            new { id = novoChamado.Id },
            novoChamado);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Chamado>> BuscarPorId(int id)
    {
        var chamado = await _service.BuscarPorIdAsync(id);

        if (chamado == null)
        {
            return NotFound();
        }

        return Ok(chamado);
    }

    [HttpPost("{id}/iniciar")]
public async Task<ActionResult<Chamado>> Iniciar(int id)
{
    var chamado = await _service.IniciarAsync(id);

    if (chamado == null)
    {
        return NotFound();
    }

    return Ok(chamado);
}

[HttpPost("{id}/encerrar")]
public async Task<ActionResult<Chamado>> Encerrar(
    int id,
    [FromBody] string solucao)
{
    var chamado = await _service.EncerrarAsync(id, solucao);

    if (chamado == null)
    {
        return BadRequest("Não foi possível encerrar o chamado. Verifique se a solução foi informada e se o chamado ainda não está fechado.");
    }

    return Ok(chamado);
}

}
using DeskFlow.API.Models.Entities;
using DeskFlow.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.API.Controllers;

[ApiController]
[Route("api/categorias")]
public class CategoriaController : ControllerBase
{
    private readonly CategoriaService _service;

    public CategoriaController(CategoriaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Categoria>>> Listar()
    {
        var categorias = await _service.ListarTodasAsync();

        return Ok(categorias);
    }

    [HttpPost]
    public async Task<ActionResult<Categoria>> Criar(Categoria categoria)
    {
        var novaCategoria = await _service.CriarAsync(categoria);

        return CreatedAtAction(
            nameof(BuscarPorId),
            new { id = novaCategoria.Id },
            novaCategoria);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> BuscarPorId(int id)
    {
        var categoria = await _service.BuscarPorIdAsync(id);

        if (categoria == null)
        {
            return NotFound();
        }

        return Ok(categoria);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Categoria>> Atualizar(int id, Categoria categoria)
    {
        var categoriaAtualizada = await _service.AtualizarAsync(id, categoria);

        if (categoriaAtualizada == null)
        {
            return NotFound();
        }

        return Ok(categoriaAtualizada);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(int id)
{
    var excluida = await _service.ExcluirAsync(id);

    if (!excluida)
    {
        return NotFound();
    }

    return NoContent();
}

}


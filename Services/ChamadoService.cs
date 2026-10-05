using DeskFlow.API.Data;
using DeskFlow.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.API.Services;

public class ChamadoService
{
    private readonly AppDbContext _context;

    public ChamadoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Chamado>> ListarTodosAsync(
        string? status = null,
        string? prioridade = null,
        int? categoriaId = null)
    {
        var query = _context.Chamados
            .Include(c => c.Categoria)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(c => c.Status == status);
        }

        if (!string.IsNullOrWhiteSpace(prioridade))
        {
            query = query.Where(c => c.Prioridade == prioridade);
        }

        if (categoriaId.HasValue)
        {
            query = query.Where(c => c.CategoriaId == categoriaId.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<Chamado> CriarAsync(Chamado chamado)
    {
        chamado.Status = "Aberto";
        chamado.DataAbertura = DateTime.Now;

        _context.Chamados.Add(chamado);

        await _context.SaveChangesAsync();

        return chamado;
    }

    public async Task<Chamado?> BuscarPorIdAsync(int id)
    {
        return await _context.Chamados
            .Include(c => c.Categoria)
            .Include(c => c.Interacoes)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Chamado?> IniciarAsync(int id)
    {
        var chamado = await _context.Chamados
            .FirstOrDefaultAsync(c => c.Id == id);

        if (chamado == null)
        {
            return null;
        }

        if (chamado.Status != "Aberto")
        {
            return null;
        }

        chamado.Status = "EmAndamento";

        await _context.SaveChangesAsync();

        return chamado;
    }

    public async Task<Chamado?> EncerrarAsync(int id, string solucao)
    {
        var chamado = await _context.Chamados
            .FirstOrDefaultAsync(c => c.Id == id);

        if (chamado == null)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(solucao))
        {
            return null;
        }

        if (chamado.Status == "Fechado")
        {
            return null;
        }

        chamado.Status = "Fechado";
        chamado.Solucao = solucao;
        chamado.DataFechamento = DateTime.Now;

        await _context.SaveChangesAsync();

        return chamado;
    }
}
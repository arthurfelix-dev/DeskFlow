using DeskFlow.API.Data;
using DeskFlow.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.API.Services;

public class InteracaoService
{
    private readonly AppDbContext _context;

    public InteracaoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Interacao?> CriarAsync(
        int chamadoId,
        Interacao interacao)
    {
        var chamado = await _context.Chamados
            .FirstOrDefaultAsync(c => c.Id == chamadoId);

        if (chamado == null)
        {
            return null;
        }

        if (chamado.Status == "Fechado")
        {
            return null;
        }

        interacao.ChamadoId = chamadoId;
        interacao.DataRegistro = DateTime.Now;

        _context.Interacoes.Add(interacao);

        await _context.SaveChangesAsync();

        return interacao;
    }
}
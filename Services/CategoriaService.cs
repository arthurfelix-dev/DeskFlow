using DeskFlow.API.Data;
using DeskFlow.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.API.Services;

public class CategoriaService
{
    private readonly AppDbContext _context;

    public CategoriaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Categoria>> ListarTodasAsync()
    {
        return await _context.Categorias
            .ToListAsync();
    }

    public async Task<Categoria> CriarAsync(Categoria categoria)
    {
        _context.Categorias.Add(categoria);

        await _context.SaveChangesAsync();

        return categoria;
    }

    public async Task<Categoria?> BuscarPorIdAsync(int id)
    {
        return await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Categoria?> AtualizarAsync(int id, Categoria categoria)
    {
        var categoriaExistente = await BuscarPorIdAsync(id);

        if (categoriaExistente == null)
        {
            return null;
        }

        categoriaExistente.Nome = categoria.Nome;

        await _context.SaveChangesAsync();

        return categoriaExistente;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var categoria = await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoria == null)
        {
            return false;
        }

        var possuiChamados = await _context.Chamados
            .AnyAsync(c => c.CategoriaId == id);

        if (possuiChamados)
        {
            return false;
        }

        _context.Categorias.Remove(categoria);

        await _context.SaveChangesAsync();

        return true;
    }
}
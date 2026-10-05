using DeskFlow.API.Data;
using DeskFlow.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.API.Repositories;

public class CategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Categoria>> ListarTodasAsync()
    {
        return await _context.Categorias
            .ToListAsync();
    }

    public async Task<Categoria?> BuscarPorIdAsync(int id)
    {
        return await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AdicionarAsync(Categoria categoria)
    {
        await _context.Categorias.AddAsync(categoria);
    }

    public async Task<bool> PossuiChamadosAsync(int categoriaId)
    {
        return await _context.Chamados
            .AnyAsync(c => c.CategoriaId == categoriaId);
    }

    public void Remover(Categoria categoria)
    {
        _context.Categorias.Remove(categoria);
    }

    public async Task SalvarAsync()
    {
        await _context.SaveChangesAsync();
    }
}
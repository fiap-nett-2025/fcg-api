using FCG.Domain.Entities;
using FCG.Domain.Exceptions;
using FCG.Domain.Interfaces;
using FCG.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FCG.Infra.Data.Repository;

public class GameRepository(FCGDbContext context) : IGameRepository
{
    private readonly FCGDbContext _context = context;
    public async Task AddAsync(Game game)
    {
        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var game = await GetBy(g => g.Id.Equals(id));
        if (game != null)
        {
            _context.Games.Attach(game);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await _context.Games
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Game?> GetBy(Expression<Func<Game, bool>> condition)
    {
        return await _context.Games
            .AsNoTracking()
            .FirstOrDefaultAsync(condition);
    }
    public async Task UpdateAsync(Game game)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
    }
}

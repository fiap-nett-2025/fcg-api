using FCG.Domain.Entities;
using System.Linq.Expressions;

namespace FCG.Domain.Interfaces;

public interface IGameRepository
{
    Task<Game?> GetBy(Expression<Func<Game, bool>> condition);
    Task<IEnumerable<Game>> GetAllAsync();
    Task AddAsync(Game game);
    Task UpdateAsync(Game game);
    Task DeleteAsync(int id);
}

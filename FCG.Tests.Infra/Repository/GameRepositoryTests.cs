using Microsoft.EntityFrameworkCore;
using FCG.Domain.Entities;
using FCG.Infra.Data.Context;
using FCG.Infra.Data.Repository;

namespace FCG.Infra.Tests.Repositories;

public class GameRepositoryTests
{
    private readonly FCGDbContext _context;
    private readonly GameRepository _repository;

    public GameRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<FCGDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new FCGDbContext(options);
        _repository = new GameRepository(_context);
    }

    [Fact]
    public async Task AddAsync_ValidGame_ShouldAddGameToDatabase()
    {
        // Arrange
        var game = new Game("Tetris", 9.99M, "Puzzle Clássico", "Puzzle");

        // Act
        await _repository.AddAsync(game);
        var savedGame = await _repository.GetBy(g => g.Title == game.Title);

        // Assert
        Assert.NotNull(savedGame);
        Assert.Equal("Tetris", savedGame.Title);
    }

    [Fact]
    public async Task DeleteAsync_DeleteExistingGame_ShouldRemoveGameFromDatabase()
    {
        // Arrange
        var game = new Game("The Witcher 3", 49.99M, "RPG de Ação", "RPG");
        await _repository.AddAsync(game);
        var gameToDelete = _repository.GetBy(g => g.Title == "The Witcher 3");
        _context.ChangeTracker.Clear();

        // Act
        await _repository.DeleteAsync(gameToDelete.Id);

        // Assert
        Assert.Null(await _repository.GetBy(g => g.Id.Equals(gameToDelete.Id)));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllGames()
    {
        // Arrange
        var games = new List<Game>
        {
            new Game("Sonic 2", 49.99M, "Plataforma 2d", "Paltaforma"),
            new Game("Deus Ex", 59.99M, "RPG de Ação", "RPG"),
            new Game("Resident Evil 3", 39.99M, "Survival Horror", "Horror")
        };
        _context.Games.AddRange(games);
        _context.SaveChanges();
        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.Equal(_context.Games.Count(), result.Count());
        Assert.Contains(result, g => g.Title == "Sonic 2");
        Assert.Contains(result, g => g.Title == "Deus Ex");
        Assert.Contains(result, g => g.Title == "Resident Evil 3");
    }

    [Fact]
    public async Task GetBy_ByTitle_ShouldReturnGame()
    {
        // Arrange
        var game = new Game("Cyberpunk 2077", 99.99M, "RPG de Ação", "RPG");
        await _repository.AddAsync(game);

        // Act
        var result = await _repository.GetBy(g => g.Title == "Cyberpunk 2077");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Cyberpunk 2077", result.Title);
    }

    [Fact]
    public async Task UpdateAsync_ModifyPrice_ShouldModify()
    {
        // Arrange
        var game = new Game("Resident Evil 4", 39.99M, "Survival Horror", "Horror");
        await _repository.AddAsync(game);
        var newPrice = 59.99M;

        // Act
        game.Price = newPrice;
        await _repository.UpdateAsync(game);

        // Assert
        var updatedGame = await _repository.GetBy(g => g.Title == "Resident Evil 4");
        Assert.NotNull(updatedGame);
        Assert.Equal(newPrice, updatedGame.Price);
    }
}

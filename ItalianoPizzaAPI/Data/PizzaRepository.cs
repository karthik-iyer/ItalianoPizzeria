using System.Linq;
using System.Threading.Tasks;
using ItalianoPizzaAPI.Data.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ItalianoPizzaAPI.Data
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly PizzaContext _context;
        private readonly ILogger<PizzaRepository> _logger;

        public PizzaRepository(PizzaContext context, ILogger<PizzaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
             _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Ingredient[]> GetAllIngredientsAsync()
        {
           _logger.LogInformation($"Getting all Ingredients");

           IQueryable<Ingredient> query = _context.Ingredients;

           query = query.OrderBy(c =>c.IngredientId);

           return await query.ToArrayAsync();
        }

        public async Task<Pizza[]> GetAllPizzasAsync()
        {
            _logger.LogInformation($"Getting all Pizzas");

           IQueryable<Pizza> query = _context.Pizzas.Include(c => c.PizzaIngredients).ThenInclude(i => i.Ingredient);

           query = query.OrderBy(c =>c.PizzaId);

           return await query.ToArrayAsync();
        }

        public async Task<Pizza> GetPizzaAsync(int pizzaId)
        {
             _logger.LogInformation($"Getting all Pizzas");

           IQueryable<Pizza> query = _context.Pizzas.Include(c => c.PizzaIngredients).ThenInclude(i => i.Ingredient);

           query = query.Where(p => p.PizzaId == pizzaId);

           return await query.FirstOrDefaultAsync();
        }

        public async Task<Pizza> GetPizzaAsync(string pizzaName)
        {
             _logger.LogInformation($"Getting all Pizzas");

           IQueryable<Pizza> query = _context.Pizzas.Include(c => c.PizzaIngredients).ThenInclude(i => i.Ingredient);

           query = query.Where(p => p.Name == pizzaName);

           return await query.FirstOrDefaultAsync();
        }
    }
}
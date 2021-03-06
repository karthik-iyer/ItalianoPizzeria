using System.Threading.Tasks;
using ItalianoPizzaAPI.Data.Entities;

namespace ItalianoPizzaAPI.Data
{
    public interface IPizzaRepository
    {
         // General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //Pizzas
        Task<Pizza[]> GetAllPizzasAsync();
        Task<Pizza> GetPizzaAsync(int pizzaId);

        Task<Pizza> GetPizzaAsync(string pizzaName);

        //Ingredients
         Task<Ingredient[]> GetAllIngredientsAsync();
    }
}
using System.Threading.Tasks;
using ItalianoPizzaAPI.Data.Entities;
using ItalianoPizzaAPI.Model;

namespace ItalianoPizzaAPI.Services
{
    public interface IPizzaService
    {
        Task<PizzaModel[]> GetAllPizzasAsync();

        Task<PizzaModel> GetPizzaAsync(int pizzaId);

        Task<IngredientModel[]> GetAllIngredientsAsync();

        Task<PizzaModel> CreateNewPizzaAsync(PizzaModel pizzaModel);

        Task<PizzaModel> UpdatePizzaAsync(int pizzaId,PizzaModel pizzaModel);

        Task<string> DeletePizzaAsync(int pizzaId);        
    }
}
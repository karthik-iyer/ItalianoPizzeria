using System;
using System.Threading.Tasks;
using AutoMapper;
using ItalianoPizzaAPI.Data;
using ItalianoPizzaAPI.Data.Entities;
using ItalianoPizzaAPI.Exceptions;
using ItalianoPizzaAPI.Model;

namespace ItalianoPizzaAPI.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly IPizzaRepository _repository;
        private readonly IMapper _mapper;

        public PizzaService(IPizzaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PizzaModel[]> GetAllPizzasAsync()
        {
           var results = await _repository.GetAllPizzasAsync();
           return _mapper.Map<PizzaModel[]>(results);          
        }

        public async Task<PizzaModel> GetPizzaAsync(int pizzaId)
        {
            var results = await _repository.GetPizzaAsync(pizzaId);
                
            if(results == null) throw new PizzaNotFoundException($"Pizza Not Found . Pizza Id {pizzaId}");

            return _mapper.Map<PizzaModel>(results);       
        }
        
        public async Task<IngredientModel[]> GetAllIngredientsAsync()
        {
            var results = await _repository.GetAllIngredientsAsync();
            return _mapper.Map<IngredientModel[]>(results);      
        }

        public async Task<PizzaModel> CreateNewPizzaAsync(PizzaModel pizzaModel)
        {
            var existingPizza = await _repository.GetPizzaAsync(pizzaModel.PizzaName);
               
            if(existingPizza != null)
            {
                throw new PizzaAlreadyExistsException( $"Pizza already exists. Please choose a different name : {existingPizza.Name}");
            }

            var pizza = _mapper.Map<Pizza>(pizzaModel);

            _repository.Add(pizza);
            
            if(await _repository.SaveChangesAsync())
            {
                var createdPizza = await _repository.GetPizzaAsync(pizza.PizzaId);
                return _mapper.Map<PizzaModel>(createdPizza);
            };

            throw new PizzaNotCreatedException($"Pizza cannot be created. Pizza name {pizzaModel.PizzaName}");
        }

        public async Task<PizzaModel> UpdatePizzaAsync(int pizzaId, PizzaModel pizzaModel)
        {
             var existingPizza = await _repository.GetPizzaAsync(pizzaId);

            if(existingPizza == null) throw new PizzaNotFoundException($"Could not find Pizza with Name {pizzaModel.PizzaName}");

            _mapper.Map(pizzaModel,existingPizza);

            if(await _repository.SaveChangesAsync())
            {
                var updatedPizza = await _repository.GetPizzaAsync(pizzaModel.PizzaId);
                return _mapper.Map<PizzaModel>(updatedPizza);
            }
            throw new PizzaNotUpdatedException($"Pizza cannot be updated . Pizza {pizzaModel.PizzaId}");
        }

        public async Task<string> DeletePizzaAsync(int pizzaId)
        {            
            var existingPizza = await _repository.GetPizzaAsync(pizzaId);

            if(existingPizza == null) throw new PizzaNotFoundException($"Could not find Pizza with Id {pizzaId}");

            _repository.Delete(existingPizza);

            if(await _repository.SaveChangesAsync())
            {
                return $"Pizza {existingPizza.Name} Deleted Successfully";
            }
            throw new PizzaNotDeletedException($"Pizza cannot be deleted. Pizza Name :{existingPizza.Name}");
        }
    }
}
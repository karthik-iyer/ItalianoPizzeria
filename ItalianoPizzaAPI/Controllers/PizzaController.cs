using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ItalianoPizzaAPI.Data;
using ItalianoPizzaAPI.Data.Entities;
using ItalianoPizzaAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ItalianoPizzaAPI.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaRepository _repository;
        private readonly IMapper _mapper;

        public PizzaController(IPizzaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }   

        [HttpGet]
        [Route("pizzas")]
        /// <summary>
        /// The method gets all the pizzas in the DB
        /// </summary>
        /// <returns>List of Pizzas</returns>
        public async Task<ActionResult<PizzaModel[]>> GetAllPizzas()
        {
            try
            {
                var results = await _repository.GetAllPizzasAsync();
                return _mapper.Map<PizzaModel[]>(results);                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet]
        [Route("pizzas/{PizzaName}")]
        /// <summary>
        /// Get Pizza by Pizza Name
        /// </summary>
        /// <param name="PizzaName"></param>
        /// <returns>Returns Pizza by Pizza Name</returns>
        public async Task<ActionResult<PizzaModel>> GetPizzaByName(string PizzaName)
        {
            try
            {
                var decodedName = System.Uri.UnescapeDataString(PizzaName);
                var results = await _repository.GetPizzaAsync(decodedName);
                
                if(results == null) return BadRequest($"Pizza Not Found . Pizza Name {PizzaName}");

                return _mapper.Map<PizzaModel>(results);                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet]
        [Route("ingredients")]
        /// <summary>
        /// Gets list of Predefined Ingredients in the system
        /// </summary>
        /// <returns>List of Ingredients</returns>
        public async Task<ActionResult<IngredientModel[]>> GetAllIngredients()
        {
            try
            {
                var results = await _repository.GetAllIngredientsAsync();
                return Ok(_mapper.Map<IngredientModel[]>(results));                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }


        [HttpPost]
        [Route("pizzas")]
        /// <summary>
        /// Creates a new Pizza
        /// </summary>
        /// <param name="pizzaModel"></param>
        /// <returns>Returns Success upon creation or Failure upon unSuccessful creation</returns>
        public async Task<ActionResult<PizzaModel>> Post(PizzaModel pizzaModel)
        {
            try
            {
                var existingPizza = await _repository.GetPizzaAsync(System.Uri.UnescapeDataString(pizzaModel.PizzaName));
               
                if(existingPizza != null)
                {
                    return BadRequest( $"Pizza already exists. Please choose a different name : {pizzaModel.PizzaName}");
                }

                //Create a new Pizza 

                var pizza = _mapper.Map<Pizza>(pizzaModel);

                _repository.Add(pizza);
                
                if(await _repository.SaveChangesAsync())
                {
                   var createdPizza = await _repository.GetPizzaAsync(System.Uri.UnescapeDataString(pizzaModel.PizzaName));
                   return Created("",_mapper.Map<PizzaModel>(createdPizza));
                };

                return Ok();                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPut]
        [Route("pizzas/{PizzaName}")]
        /// <summary>
        /// Updates an existing Pizza
        /// </summary>
        /// <param name="PizzaName"></param>
        /// <param name="pizzaModel"></param>
        /// <returns>Returns an updated Pizza</returns>
        public async Task<ActionResult<PizzaModel>> Put(string PizzaName, PizzaModel pizzaModel)
        {
            try
            {
                var decodedName = System.Uri.UnescapeDataString(PizzaName);
                var existingPizza = await _repository.GetPizzaAsync(PizzaName);

                if(existingPizza == null) return NotFound($"Could not find Pizza with Name {pizzaModel.PizzaName}");

                _mapper.Map(pizzaModel,existingPizza);

               if(await _repository.SaveChangesAsync())
               {
                   var updatedPizza = await _repository.GetPizzaAsync(pizzaModel.PizzaName);
                   return _mapper.Map<PizzaModel>(updatedPizza);
               }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {ex.ToString()}");
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("pizzas/{PizzaName}")]
        /// <summary>
        /// Deletes a Pizza
        /// </summary>
        /// <param name="PizzaName"></param>
        /// <returns>Returns success or failure</returns>
        public async Task<IActionResult> Delete(string PizzaName)
        {
            try
            {
                var decodedName = System.Uri.UnescapeDataString(PizzaName);
                var existingPizza = await _repository.GetPizzaAsync(decodedName);

                if(existingPizza == null) return NotFound($"Could not find Pizza with Name {decodedName}");

                _repository.Delete(existingPizza);

                if(await _repository.SaveChangesAsync())
                {
                   return Ok($"Pizza {decodedName} Deleted Successfully");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }

            return BadRequest("Failed to Delete Pizza");
        }
    }
}
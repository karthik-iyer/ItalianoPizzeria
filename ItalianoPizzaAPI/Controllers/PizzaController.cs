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
        public async Task<ActionResult<PizzaModel>> GetPizzaByName(string PizzaName)
        {
            try
            {
                var decodedName = System.Uri.UnescapeDataString(PizzaName);
                var results = await _repository.GetPizzaAsync(decodedName);
                return _mapper.Map<PizzaModel>(results);                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet]
        [Route("ingredients")]
        public async Task<ActionResult<IngredientModel[]>> GetAllIngredients()
        {
            try
            {
                var results = await _repository.GetAllIngredientsAsync();
                _mapper.Map<IngredientModel[]>(results);
                return Ok(results);                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }


        [HttpPost]
        [Route("pizzas")]
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
                    return Created("",_mapper.Map<PizzaModel>(pizza));
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
                   return _mapper.Map<PizzaModel>(existingPizza);
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
                   return Ok();
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
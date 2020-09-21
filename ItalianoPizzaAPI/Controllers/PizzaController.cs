using System;
using System.Linq;
using ItalianoPizzaAPI.Data;
using ItalianoPizzaAPI.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ItalianoPizzaAPI.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly PizzaContext _context;
       

        public PizzaController(PizzaContext context)
        {
            _context = context;
           
        }   

        [HttpGet]
        [Route("pizzas")]
        public ActionResult GetAllPizzas()
        {
            try
            {
                var results = _context.Pizzas.Join(
                    _context.PizzaIngredients,
                    pizza => pizza.PizzaId,
                    pizzaIngredient => pizzaIngredient.PizzaId,
                    (pizza,pizzaIngredient) => new {
                        PizzaID = pizza.PizzaId,
                        PizzaName = pizza.Name,
                        PizzaDoughType = pizza.DoughType,
                        isCalzone = pizza.isCalzone,
                        pizzaIngredients = pizzaIngredient.Ingredient.Name 
                    }
                );
                return Ok(results);                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet]
        [Route("ingredients")]
        public ActionResult GetAllIngredients()
        {
            try
            {
                var results = _context.Ingredients.ToList();
                return Ok(results);                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }


        [HttpPost]
        [Route("pizzas")]
        public ActionResult Post(Pizza pizza)
        {
            try
            {
                var existingPizza = _context.Pizzas.Where(x => x.Name == pizza.Name).FirstOrDefault();
               
                if(existingPizza != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Pizza already exists. Please choose a different name : {pizza.Name}");
                }

                _context.Pizzas.Add(pizza);
                _context.SaveChanges();

                return Ok();                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPut]
        [Route("pizzas/{PizzaId:int}")]
        public ActionResult Put(Pizza pizza, int PizzaId)
        {
            try
            {
                var existingPizza = _context.Pizzas.Where(x => x.PizzaId == PizzaId).FirstOrDefault();

                if(existingPizza == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Pizza does not exists. Please choose existing pizza to update");
                }

                existingPizza = pizza;
                existingPizza.PizzaIngredients = pizza.PizzaIngredients;
                _context.Pizzas.Update(existingPizza);
                _context.SaveChanges();

                return Ok();                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {ex.ToString()}");
            }
        }

        [HttpDelete]
        [Route("pizzas/{PizzaId:int}")]
        public ActionResult Delete(int PizzaId)
        {
            try
            {
                var existingPizza = _context.Pizzas.Where(x => x.PizzaId == PizzaId).FirstOrDefault();

                if(existingPizza == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Pizza does not exists. Please choose existing pizza to delete");
                }

                _context.Pizzas.Remove(existingPizza);
                _context.SaveChanges();

                return Ok();                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
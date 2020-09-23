using System;
using System.Threading.Tasks;
using ItalianoPizzaAPI.Model;
using ItalianoPizzaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace ItalianoPizzaAPI.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;

        public IngredientsController(IPizzaService service)
        {
            _pizzaService = service;
        }
        
        [HttpGet]
        /// <summary>
        /// Gets list of Predefined Ingredients in the system
        /// </summary>
        /// <returns>List of Ingredients</returns>
        public async Task<ActionResult<IngredientModel[]>> GetAllIngredientsAsync()
        {
            try
            {
                return Ok(await _pizzaService.GetAllIngredientsAsync());          
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
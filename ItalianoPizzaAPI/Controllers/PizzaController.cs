using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ItalianoPizzaAPI.Data;
using ItalianoPizzaAPI.Data.Entities;
using ItalianoPizzaAPI.Exceptions;
using ItalianoPizzaAPI.Model;
using ItalianoPizzaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ItalianoPizzaAPI.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// Pizza Controller
    /// </summary>
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;
        public PizzaController(IPizzaService service)
        {
            _pizzaService = service;
        }   

        [HttpGet]
        /// <summary>
        /// The method gets all the pizzas in the DB
        /// </summary>
        /// <returns>List of Pizzas</returns>
        public async Task<ActionResult<PizzaModel[]>> GetAllPizzasAsync()
        {
            try
            {
                return await _pizzaService.GetAllPizzasAsync();      
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet]
        [Route("{PizzaId:int}")]
        /// <summary>
        /// Get Pizza by Pizza Name
        /// </summary>
        /// <param name="PizzaId"></param>
        /// <returns>Returns Pizza by Pizza Name</returns>
        public async Task<ActionResult<PizzaModel>> GetPizzaByIdAsync(int PizzaId)
        {
            try
            {
                return await _pizzaService.GetPizzaAsync(PizzaId);         
            }
            catch(PizzaNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost]
        /// <summary>
        /// Creates a new Pizza
        /// </summary>
        /// <param name="pizzaModel"></param>
        /// <returns>Returns Success upon creation or Failure upon unSuccessful creation</returns>
        public async Task<ActionResult<PizzaModel>> PostAsync(PizzaModel pizzaModel)
        {
            try
            {
                return Created("", await _pizzaService.CreateNewPizzaAsync(pizzaModel));               
            }
            catch(PizzaNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch(PizzaNotCreatedException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPut]
        [Route("{PizzaId:int}")]
        /// <summary>
        /// Updates an existing Pizza
        /// </summary>
        /// <param name="PizzaId"></param>
        /// <param name="pizzaModel"></param>
        /// <returns>Returns an updated Pizza</returns>
        public async Task<ActionResult<PizzaModel>> PutAsync(int PizzaId, PizzaModel pizzaModel)
        {
            try
            {
                return await _pizzaService.UpdatePizzaAsync(PizzaId,pizzaModel);
            }
            catch(PizzaNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch(PizzaNotUpdatedException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {ex.ToString()}");
            }
        }

        [HttpDelete]
        [Route("{PizzaId:int}")]
        /// <summary>
        /// Deletes a Pizza
        /// </summary>
        /// <param name="PizzaId"></param>
        /// <returns>Returns success or failure</returns>
        public async Task<IActionResult> DeleteAsync(int PizzaId)
        {
            try
            {
                await _pizzaService.DeletePizzaAsync(PizzaId);
                return Ok();
            }
            catch(PizzaNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (PizzaNotDeletedException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
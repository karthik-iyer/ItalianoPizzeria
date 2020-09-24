using System;
using System.Threading.Tasks;
using ItalianoPizzaAPI.Controllers;
using ItalianoPizzaAPI.Services;
using ItalianoPizzaAPI.Model;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Http;

namespace ItalianoPizzaAPITests.ControllerTests
{
    public class IngredientsControllerTests
    {
        private IngredientsController _target; 
        private Mock<IPizzaService> _PizzaService = new Mock<IPizzaService>(MockBehavior.Strict);

        [SetUp]
        public void Setup()
        {
            _target = new IngredientsController(_PizzaService.Object);

        }

        [Test]
        public async Task GetAllIngredientsAsync_WhenSuccessful_ReturnsIngredients()
        {
            var ingredientModels = new[] {new IngredientModel()};
            //Arrange
            _PizzaService.Setup(x => x.GetAllIngredientsAsync()).ReturnsAsync(ingredientModels);
            var expected = new[] {new IngredientModel()};

            //Act
            var actual = await _target.GetAllIngredientsAsync();
            var actualresult = actual.Value;
            //Assert
            Assert.AreEqual(actualresult.Length, expected.Length);
        }
        
        [Test]
        public async Task GetAllIngredientsAsync_WhenNotSuccessful_ReturnsInternalServerError()
        {
            //Arrange
            _PizzaService.Setup(x => x.GetAllIngredientsAsync()).ThrowsAsync(new Exception());

            var actual = await _target.GetAllIngredientsAsync();

            Assert.AreEqual((actual.Result as ObjectResult).StatusCode, StatusCodes.Status500InternalServerError);
        }
    }
}
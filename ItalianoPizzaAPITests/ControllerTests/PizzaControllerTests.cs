using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using ItalianoPizzaAPI.Controllers;
using ItalianoPizzaAPI.Exceptions;
using ItalianoPizzaAPI.Model;
using ItalianoPizzaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ItalianoPizzaAPITests
{
    public class PizzaControllerTests
    {
        private PizzaController _target;
        private Mock<IPizzaService> _pizzaService = new Mock<IPizzaService>();
        
        [SetUp]
        public void Setup()
        {
            _target = new PizzaController(_pizzaService.Object);
        }

        [Test]
        public async Task GetAllPizzaAsync_WhenSuccessful_ReturnsAllPizzas()
        {
            //Arrange
            var pizzaModels = new[] {new PizzaModel()};
            _pizzaService.Setup(x => x.GetAllPizzasAsync()).ReturnsAsync(pizzaModels);

            var expected =  new[] {new PizzaModel()};
            //Act
            var actual = await _target.GetAllPizzasAsync();
            var actualValue = actual.Value;
            //Assert
            Assert.AreEqual(actualValue.Length,expected.Length);
        }
        
        [Test]
        public async Task GetAllPizzaAsync_WhenNotSuccessful_ReturnsInternalServerError()
        {
            //Arrange
            _pizzaService.Setup(x => x.GetAllPizzasAsync()).ThrowsAsync(new Exception());

            var expected = await _target.GetAllPizzasAsync();

            Assert.AreEqual((expected.Result as ObjectResult).StatusCode, StatusCodes.Status500InternalServerError);
        }

        [Test]
        public async Task GetPizzaByIdAsync_WhenSuccessful_ReturnsValidPizza()
        {
            //Arrange
            var pizzaModel = new PizzaModel()
            {
                PizzaId = 1,
                DoughType = "Sicilian",
                IsCalzone = true,
                PizzaName = "Sicilian Pizza",
                PizzaIngredientsModel = new List<PizzaIngredientsModel>()
            };
            _pizzaService.Setup(x => x.GetPizzaAsync(It.IsAny<int>())).ReturnsAsync(pizzaModel);

            var expected =  new PizzaModel()
            {
                PizzaId = 1,
                DoughType = "Sicilian",
                IsCalzone = true,
                PizzaName = "Sicilian Pizza",
                PizzaIngredientsModel = new List<PizzaIngredientsModel>()
            };
            
            //Act
            var actual = await _target.GetPizzaByIdAsync(It.IsAny<int>());
            
            //Assert
            actual.Value.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public async Task GetPizzaByIdAsync_throwsPizzaNotFoundException_ReturnsBadRequest()
        {
            //Arrange
            _pizzaService.Setup(x => x.GetPizzaAsync(It.IsAny<int>())).ThrowsAsync(new PizzaNotFoundException());

            //Act
            var actual = await _target.GetPizzaByIdAsync(It.IsAny<int>());
            
            //Assert
            Assert.AreEqual((actual.Result as ObjectResult).StatusCode, StatusCodes.Status400BadRequest);
        }
        
        [Test]
        public async Task GetPizzaByIdAsync_throwsException_ReturnsInternalServerError()
        {
            //Arrange
            _pizzaService.Setup(x => x.GetPizzaAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            //Act
            var actual = await _target.GetPizzaByIdAsync(It.IsAny<int>());
            
            //Assert
            Assert.AreEqual((actual.Result as ObjectResult).StatusCode, StatusCodes.Status500InternalServerError);
        }
        
        [Test]
        public async Task PostAsync_throwsPizzaNotFoundException_ReturnsBadRequestError()
        {
            //Arrange
            _pizzaService.Setup(x => x.CreateNewPizzaAsync(It.IsAny<PizzaModel>())).ThrowsAsync(new PizzaNotFoundException());

            //Act
            var actual = await _target.PostAsync(It.IsAny<PizzaModel>());
            
            //Assert
            Assert.AreEqual((actual.Result as ObjectResult).StatusCode, StatusCodes.Status400BadRequest);
        }
        
        [Test]
        public async Task PostAsync_throwsPizzaNoCreatedException_ReturnsBadRequest()
        {
            //Arrange
            _pizzaService.Setup(x => x.CreateNewPizzaAsync(It.IsAny<PizzaModel>())).ThrowsAsync(new PizzaNotCreatedException());

            //Act
            var actual = await _target.PostAsync(It.IsAny<PizzaModel>());
            
            //Assert
            Assert.AreEqual((actual.Result as ObjectResult).StatusCode, StatusCodes.Status400BadRequest);
        }
        
        [Test]
        public async Task PostAsync_throwsException_ReturnsInternalServerError()
        {
            //Arrange
            _pizzaService.Setup(x => x.CreateNewPizzaAsync(It.IsAny<PizzaModel>())).ThrowsAsync(new Exception());

            //Act
            var actual = await _target.PostAsync(It.IsAny<PizzaModel>());
            
            //Assert
            Assert.AreEqual((actual.Result as ObjectResult).StatusCode, StatusCodes.Status500InternalServerError);
        }
        
        [Test]
        public async Task PostAsync_WhenSuccessful_ReturnsCreatedPizza()
        {
            //Arrange
            var pizzaModel = new PizzaModel()
            {
                PizzaId = 1,
                DoughType = "Sicilian",
                IsCalzone = true,
                PizzaName = "Sicilian Pizza",
                PizzaIngredientsModel = new List<PizzaIngredientsModel>()
            };
            _pizzaService.Setup(x => x.CreateNewPizzaAsync(It.IsAny<PizzaModel>())).ReturnsAsync(pizzaModel);

            var expected =  new PizzaModel()
            {
                PizzaId = 1,
                DoughType = "Sicilian",
                IsCalzone = true,
                PizzaName = "Sicilian Pizza",
                PizzaIngredientsModel = new List<PizzaIngredientsModel>()
            };
            
            //Act
            var actual = await _target.PostAsync(It.IsAny<PizzaModel>());
            
            //Assert
            (actual.Result as CreatedResult).Value.Should().BeEquivalentTo(expected);
        }
        
         [Test]
        public async Task PutAsync_throwsPizzaNotFoundException_ReturnsBadRequestError()
        {
            //Arrange
            _pizzaService.Setup(x => x.UpdatePizzaAsync(It.IsAny<int>(),It.IsAny<PizzaModel>())).ThrowsAsync(new PizzaNotFoundException());

            //Act
            var actual = await _target.PutAsync(It.IsAny<int>(),It.IsAny<PizzaModel>());
            
            //Assert
            Assert.AreEqual((actual.Result as ObjectResult).StatusCode, StatusCodes.Status400BadRequest);
        }
        
        [Test]
        public async Task PutAsync_throwsPizzaNotUpdatedException_ReturnsBadRequestError()
        {
            //Arrange
            _pizzaService.Setup(x => x.UpdatePizzaAsync(It.IsAny<int>(),It.IsAny<PizzaModel>())).ThrowsAsync(new PizzaNotUpdatedException());

            //Act
            var actual = await _target.PutAsync(It.IsAny<int>(),It.IsAny<PizzaModel>());
            
            //Assert
            Assert.AreEqual((actual.Result as ObjectResult).StatusCode, StatusCodes.Status400BadRequest);
        }
        
        [Test]
        public async Task PutAsync_throwsException_ReturnsInternalServerError()
        {
            //Arrange
            _pizzaService.Setup(x => x.UpdatePizzaAsync(It.IsAny<int>(),It.IsAny<PizzaModel>())).ThrowsAsync(new Exception());

            //Act
            var actual = await _target.PutAsync(It.IsAny<int>(),It.IsAny<PizzaModel>());
            
            //Assert
            Assert.AreEqual((actual.Result as ObjectResult).StatusCode, StatusCodes.Status500InternalServerError);
        }
        
        [Test]
        public async Task PutAsync_WhenSuccessful_ReturnsUpdatedPizza()
        {
            //Arrange
            var pizzaModel = new PizzaModel()
            {
                PizzaId = 1,
                DoughType = "Sicilian",
                IsCalzone = true,
                PizzaName = "Sicilian Pizza",
                PizzaIngredientsModel = new List<PizzaIngredientsModel>()
            };
            _pizzaService.Setup(x => x.UpdatePizzaAsync(It.IsAny<int>(),It.IsAny<PizzaModel>())).ReturnsAsync(pizzaModel);

            var expected =  new PizzaModel()
            {
                PizzaId = 1,
                DoughType = "Sicilian",
                IsCalzone = true,
                PizzaName = "Sicilian Pizza",
                PizzaIngredientsModel = new List<PizzaIngredientsModel>()
            };
            
            //Act
            var actual = await _target.PutAsync(It.IsAny<int>(),It.IsAny<PizzaModel>());
            
            //Assert
            actual.Value.Should().BeEquivalentTo(expected);
        }
        
           [Test]
        public async Task DeleteAsync_throwsPizzaNotFoundException_ReturnsBadRequestError()
        {
            //Arrange
            _pizzaService.Setup(x => x.DeletePizzaAsync(It.IsAny<int>())).ThrowsAsync(new PizzaNotFoundException());

            //Act
            var actual = await _target.DeleteAsync(It.IsAny<int>());
            
            //Assert
            Assert.AreEqual((actual as ObjectResult).StatusCode, StatusCodes.Status400BadRequest);
        }
        
        [Test]
        public async Task DeleteAsync_throwsPizzaNotDeletedException_ReturnsBadRequestError()
        {
            //Arrange
            _pizzaService.Setup(x => x.DeletePizzaAsync(It.IsAny<int>())).ThrowsAsync(new PizzaNotDeletedException());

            //Act
            var actual = await _target.DeleteAsync(It.IsAny<int>());
            
            //Assert
            Assert.AreEqual((actual as ObjectResult).StatusCode, StatusCodes.Status400BadRequest);
        }
        
        [Test]
        public async Task DeleteAsync_throwsException_ReturnsInternalServerError()
        {
            //Arrange
            _pizzaService.Setup(x => x.DeletePizzaAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            //Act
            var actual = await _target.DeleteAsync(It.IsAny<int>());
            
            //Assert
            Assert.AreEqual((actual as ObjectResult).StatusCode, StatusCodes.Status500InternalServerError);
        }
        
        [Test]
        public async Task DeleteAsync_WhenSuccessful_ReturnsDeletedPizzaMessage()
        {
            //Arrange
            _pizzaService.Setup(x => x.DeletePizzaAsync(It.IsAny<int>())).ReturnsAsync(string.Empty);

            //Act
            var actual = await _target.DeleteAsync(It.IsAny<int>());
            
            //Assert
            Assert.AreEqual((actual as OkResult).StatusCode, StatusCodes.Status200OK);
        }
    }
}
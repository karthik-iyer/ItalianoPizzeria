using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using ItalianoPizzaAPI.Data;
using ItalianoPizzaAPI.Data.Entities;
using ItalianoPizzaAPI.Exceptions;
using ItalianoPizzaAPI.Model;
using ItalianoPizzaAPI.Services;
using NUnit.Framework;
using Moq;
namespace ItalianoPizzaAPITests.ServiceTests
{
    public class PizzaServiceTests
    {
        private PizzaService _target;
        private Mock<IMapper> _mapper = new Mock<IMapper>();
        private Mock<IPizzaRepository> _pizzaRepository = new Mock<IPizzaRepository>();
        [SetUp]
        public void Setup()
        {
            _target = new PizzaService(_pizzaRepository.Object,_mapper.Object);
        }

        [Test] 
        public async Task GetAllPizzasAsync_WhenSuccessful_returns_PizzaModelArray()
        {
            //Arrange
            var pizzas = new [] { new Pizza()
            {
                PizzaId = 1,
                Name = "Test Pizza",
                DoughType = "Sicilian",
                isCalzone = true,
                PizzaIngredients = new List<PizzaIngredient>()
            }};
            var pizzaModels = new[]
            {
                new PizzaModel()
                {
                    PizzaId = 1,
                    PizzaName = "Test Pizza",
                    DoughType = "Sicilian",
                    IsCalzone = true,
                    PizzaIngredientsModel = new List<PizzaIngredientsModel>()
                }
            };

            var expected = new[] {
                new PizzaModel()
                {
                    PizzaId = 1,
                    PizzaName = "Test Pizza",
                    DoughType = "Sicilian",
                    IsCalzone = true,
                    PizzaIngredientsModel = new List<PizzaIngredientsModel>()
                }
            };
                
            _pizzaRepository.Setup(x => x.GetAllPizzasAsync()).ReturnsAsync(pizzas);
            _mapper.Setup(x => x.Map<PizzaModel[]>(pizzas)).Returns(pizzaModels);
            
            //Act
            var actual = await _target.GetAllPizzasAsync();


            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
        
        [Test] 
        public async Task GetPizzaAsync_WhenSuccessful_returns_Pizza()
        {
            //Arrange
            var pizza =  new Pizza()
            {
                PizzaId = 1,
                Name = "Test Pizza",
                DoughType = "Sicilian",
                isCalzone = true,
                PizzaIngredients = new List<PizzaIngredient>()
            };
            var pizzaModel = new PizzaModel()
            {
                PizzaId = 1,
                PizzaName = "Test Pizza",
                DoughType = "Sicilian",
                IsCalzone = true,
                PizzaIngredientsModel = new List<PizzaIngredientsModel>()
            };

            var expectedPizzaModel = new PizzaModel()
                                    {
                                        PizzaId = 1,
                                        PizzaName = "Test Pizza",
                                        DoughType = "Sicilian",
                                        IsCalzone = true,
                                        PizzaIngredientsModel = new List<PizzaIngredientsModel>()
                                    };

            _pizzaRepository.Setup(x => x.GetPizzaAsync(It.IsAny<int>())).ReturnsAsync(pizza);
            _mapper.Setup(x => x.Map<PizzaModel>(pizza)).Returns(pizzaModel);
            
            //Act
            var actual = await _target.GetPizzaAsync(It.IsAny<int>());


            //Assert
            actual.Should().BeEquivalentTo(expectedPizzaModel);
        }
        
        [Test]
        public void GetPizzaAsync_WhenUnSuccessful_returns_PizzaNotFoundException()
        {
            //Arrange
            _pizzaRepository.Setup(x => x.GetPizzaAsync(It.IsAny<int>())).ReturnsAsync(() => null);
            //Assert
            Assert.That(() => _target.GetPizzaAsync(1), Throws.TypeOf<PizzaNotFoundException>());
        }
        
        [Test] 
        public async Task GetAllIngredientsAsync_WhenSuccessful_returns_IngredientModelArray()
        {
            //Arrange
            var ingredients = new [] { new Ingredient()
            {
                IngredientId = 1,
                Name = "Onion",
                Type = "Veggie",
                PizzaIngredients = new List<PizzaIngredient>()
            }};
            
            var ingredientModels = new[]
            {
                new IngredientModel()
                {
                    IngredientId = 1,
                    Name = "Onion"
                }
            };

            var expected = new[] {
                new IngredientModel()
                {
                    IngredientId = 1,
                    Name = "Onion"
                }
            };
                
            _pizzaRepository.Setup(x => x.GetAllIngredientsAsync()).ReturnsAsync(ingredients);
            _mapper.Setup(x => x.Map<IngredientModel[]>(ingredients)).Returns(ingredientModels);
            
            //Act
            var actual = await _target.GetAllIngredientsAsync();
            
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void CreateNewPizzaAsync_WhenUnSuccessful_returns_PizzaAlreadyExistsException()
        {
            var pizza = new Pizza()
            {
                PizzaId = 1,
                Name = "Test Pizza",
                DoughType = "Sicilian",
                isCalzone = true,
                PizzaIngredients = new List<PizzaIngredient>()
            };
            
            var pizzaModel = new PizzaModel()
            {
                PizzaId = 1,
                PizzaName = "Test Pizza",
                DoughType = "Sicilian",
                IsCalzone = true,
                PizzaIngredientsModel = new List<PizzaIngredientsModel>()
            };
            //Arrange
            _pizzaRepository.Setup(x => x.GetPizzaAsync(It.IsAny<string>())).ReturnsAsync(pizza);
            //Assert
            Assert.That(() => _target.CreateNewPizzaAsync(pizzaModel), Throws.TypeOf<PizzaAlreadyExistsException>());
        }
        
        [Test]
        public void CreateNewPizzaAsync_WhenUnSuccessfulSave_returns_PizzaNotCreatedException()
        {
            var pizza = new Pizza()
            {
                PizzaId = 1,
                Name = "Test Pizza",
                DoughType = "Sicilian",
                isCalzone = true,
                PizzaIngredients = new List<PizzaIngredient>()
            };
            
            var pizzaModel = new PizzaModel()
            {
                PizzaId = 1,
                PizzaName = "Test Pizza",
                DoughType = "Sicilian",
                IsCalzone = true,
                PizzaIngredientsModel = new List<PizzaIngredientsModel>()
            };
            //Arrange
            _pizzaRepository.Setup(x => x.GetPizzaAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            _mapper.Setup(x => x.Map<Pizza>(pizzaModel)).Returns(pizza);
            _pizzaRepository.Setup(x => x.Add(pizza)).Verifiable();
            _pizzaRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(false);
            //Assert
            Assert.That(() => _target.CreateNewPizzaAsync(pizzaModel), Throws.TypeOf<PizzaNotCreatedException>());
        }


            [Test]
            public async Task CreateNewPizzaAsync_WhenSuccessfulSave_returns_CreatedPizza()
            {
                //Arrange
                var pizza = new Pizza()
                {
                    PizzaId = 1,
                    Name = "Test Pizza",
                    DoughType = "Sicilian",
                    isCalzone = true,
                    PizzaIngredients = new List<PizzaIngredient>()
                };
        
                var pizzaModel = new PizzaModel()
                {
                    PizzaId = 1,
                    PizzaName = "Test Pizza",
                    DoughType = "Sicilian",
                    IsCalzone = true,
                    PizzaIngredientsModel = new List<PizzaIngredientsModel>()
                };
        
                var expectedPizzaModel = new PizzaModel()
                {
                    PizzaId = 1,
                    PizzaName = "Test Pizza",
                    DoughType = "Sicilian",
                    IsCalzone = true,
                    PizzaIngredientsModel = new List<PizzaIngredientsModel>()
                };
                _pizzaRepository.Setup(x => x.GetPizzaAsync(pizzaModel.PizzaName)).ReturnsAsync(() => null);
                _mapper.Setup(x => x.Map<Pizza>(pizzaModel)).Returns(pizza);
                _pizzaRepository.Setup(x => x.Add(pizza)).Verifiable();
                _pizzaRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(true);
                _pizzaRepository.Setup(x => x.GetPizzaAsync(It.IsAny<int>())).ReturnsAsync(pizza);
                _mapper.Setup(x => x.Map<PizzaModel>(pizza)).Returns(pizzaModel);
                //Assert
                var actualPizzaModel = await _target.CreateNewPizzaAsync(pizzaModel);
                actualPizzaModel.Should().BeEquivalentTo(expectedPizzaModel);
            }
        [Test]
        public void UpdatePizzaAsync_WhenUnSuccessful_returns_PizzaNotFoundException()
        {
            //Arrange
            var pizzaModel = new PizzaModel()
            {
                PizzaId = 1,
                PizzaName = "Test Pizza",
                DoughType = "Sicilian",
                IsCalzone = true,
                PizzaIngredientsModel = new List<PizzaIngredientsModel>()
            };
          
            _pizzaRepository.Setup(x => x.GetPizzaAsync(pizzaModel.PizzaId)).ReturnsAsync(() => null);
            //Act, Assert
            Assert.That(() => _target.UpdatePizzaAsync(pizzaModel.PizzaId,pizzaModel), Throws.TypeOf<PizzaNotFoundException>());
        }
        
        [Test]
        public void UpdatePizzaAsync_WhenUnSuccessfulSave_returns_PizzaNotUpdatedException()
        {
            //Arrange
            var pizza = new Pizza()
            {
                PizzaId = 1,
                Name = "Test Pizza",
                DoughType = "Sicilian",
                isCalzone = true,
                PizzaIngredients = new List<PizzaIngredient>()
            };
            
            var pizzaModel = new PizzaModel()
            {
                PizzaId = 1,
                PizzaName = "Test Pizza",
                DoughType = "Sicilian",
                IsCalzone = true,
                PizzaIngredientsModel = new List<PizzaIngredientsModel>()
            };
            _pizzaRepository.Setup(x => x.GetPizzaAsync(pizzaModel.PizzaId)).ReturnsAsync(pizza);
            _mapper.Setup(x => x.Map<Pizza>(pizzaModel)).Returns(pizza);
            _pizzaRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(false);
            //Act, Assert
            Assert.That(() => _target.UpdatePizzaAsync(pizzaModel.PizzaId,pizzaModel), Throws.TypeOf<PizzaNotUpdatedException>());
        }
        
        [Test]
        public async Task UpdatePizzaAsync_WhenSuccessfulUpdate_returns_UpdatedPizza()
        {
            //Arrange
            var pizza = new Pizza()
            {
                PizzaId = 1,
                Name = "Test Pizza",
                DoughType = "Sicilian",
                isCalzone = true,
                PizzaIngredients = new List<PizzaIngredient>()
            };
            
            var pizzaModel = new PizzaModel()
            {
                PizzaId = 1,
                PizzaName = "Test Pizza",
                DoughType = "Sicilian",
                IsCalzone = true,
                PizzaIngredientsModel = new List<PizzaIngredientsModel>()
            };
            
            var expectedPizzaModel = new PizzaModel()
            {
                PizzaId = 1,
                PizzaName = "Test Pizza",
                DoughType = "Sicilian",
                IsCalzone = true,
                PizzaIngredientsModel = new List<PizzaIngredientsModel>()
            };
            
            _pizzaRepository.Setup(x => x.GetPizzaAsync(pizzaModel.PizzaId)).ReturnsAsync(pizza);
            _mapper.Setup(x => x.Map<Pizza>(pizzaModel)).Returns(pizza);
            _pizzaRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(true);
            _pizzaRepository.Setup(x => x.GetPizzaAsync(It.IsAny<int>())).ReturnsAsync(pizza);
            _mapper.Setup(x => x.Map<PizzaModel>(pizza)).Returns(pizzaModel);
            //Act
            var actualModel = await _target.UpdatePizzaAsync(pizzaModel.PizzaId, pizzaModel);
            //Assert
            actualModel.Should().BeEquivalentTo(expectedPizzaModel);
        }
        
        [Test]
        public void DeletePizzaAsync_WhenUnSuccessful_returns_PizzaNotFoundException()
        {
            //Arrange
            _pizzaRepository.Setup(x => x.GetPizzaAsync(It.IsAny<int>())).ReturnsAsync(() => null);
            // Act , Assert
            Assert.That(() => _target.DeletePizzaAsync(It.IsAny<int>()), Throws.TypeOf<PizzaNotFoundException>());
        }
        
        [Test]
        public void DeletePizzaAsync_WhenUnSuccessfulDelete_returns_PizzaNotDeletedException()
        {
            //Arrange
            var pizza = new Pizza()
            {
                PizzaId = 1,
                Name = "Test Pizza",
                DoughType = "Sicilian",
                isCalzone = true,
                PizzaIngredients = new List<PizzaIngredient>()
            };
            
            var pizzaModel = new PizzaModel()
            {
                PizzaId = 1,
                PizzaName = "Test Pizza",
                DoughType = "Sicilian",
                IsCalzone = true,
                PizzaIngredientsModel = new List<PizzaIngredientsModel>()
            };
            
            _pizzaRepository.Setup(x => x.GetPizzaAsync(pizzaModel.PizzaId)).ReturnsAsync(pizza);
            _pizzaRepository.Setup(x => x.Delete(pizza)).Verifiable();
            _pizzaRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(false);
            //Act
            //Assert
            Assert.That(() => _target.DeletePizzaAsync(pizzaModel.PizzaId), Throws.TypeOf<PizzaNotDeletedException>());
        }
        
        [Test]
        public async Task DeletePizzaAsync_WhenSuccessfulDelete_returns_SuccessfulDeletedMessage()
        {
            //Arrange
            var pizza = new Pizza()
            {
                PizzaId = 1,
                Name = "Test Pizza",
                DoughType = "Sicilian",
                isCalzone = true,
                PizzaIngredients = new List<PizzaIngredient>()
            };

            _pizzaRepository.Setup(x => x.GetPizzaAsync(It.IsAny<int>())).ReturnsAsync(pizza);
            _pizzaRepository.Setup(x => x.Delete(pizza)).Verifiable();
            _pizzaRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(true);
            //Act
            var actualModel = await _target.DeletePizzaAsync(It.IsAny<int>());
            
            //Assert
            Assert.IsNotEmpty(actualModel);
        }
    }
}
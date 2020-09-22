using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ItalianoPizzaAPI.Data.Entities;
using ItalianoPizzaAPI.Model;

namespace ItalianoPizzaAPI.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PizzaModel,Pizza>()
            .ForMember(m => m.Name, o => o.MapFrom(src => src.PizzaName))
            .ForMember(m => m.PizzaIngredients, o => o.MapFrom(src => MapPizzaIngredientsModelToPizzaIngredients(src.PizzaId,src.PizzaIngredientsModel)));


            CreateMap<Pizza,PizzaModel>()
            .ForMember(m => m.PizzaName, o => o.MapFrom(src => src.Name))
            .ForMember(m => m.PizzaIngredientsModel, o => o.MapFrom(src => MapPizzaIngredientsToPizzaIngredientsModel(src.PizzaId,src.PizzaIngredients)));

            CreateMap<PizzaIngredientsModel,PizzaIngredient>()
            .ForMember(m => m.Ingredient, opt => opt.Ignore())
            .ForMember(m => m.Pizza, opt => opt.Ignore());

            CreateMap<PizzaIngredient,PizzaIngredientsModel>()
            .ForMember(m => m.IngredientName, o => o.MapFrom(src => src.Ingredient.Name));               
                   

            CreateMap<IngredientModel,Ingredient>()
            .ForMember(m => m.IngredientId, o => o.MapFrom(src => src.IngredientId))
            .ForMember(m => m.Name, o => o.MapFrom(src => src.Name))
            .ForMember(m => m.Type, o => o.MapFrom(src => src.Type))
            .ForMember(m => m.PizzaIngredients, opt => opt.Ignore());
        }

        private List<PizzaIngredientsModel> MapPizzaIngredientsToPizzaIngredientsModel(int pizzaId, ICollection<PizzaIngredient> pizzaIngredients)
        {
            var pizzaIngredientsModel = new List<PizzaIngredientsModel>();

            foreach(var pizzaIngredient in pizzaIngredients)
            {
                var pizzaIngredientModel = new PizzaIngredientsModel();
                pizzaIngredientModel.IngredientId = pizzaIngredient.IngredientId;
                pizzaIngredientModel.IngredientName = pizzaIngredient.Ingredient.Name;
                pizzaIngredientsModel.Add(pizzaIngredientModel);
            }
            return pizzaIngredientsModel.OrderBy(o => o.IngredientId).ToList();
        }

        private List<PizzaIngredient> MapPizzaIngredientsModelToPizzaIngredients(int pizzaId, List<PizzaIngredientsModel> pizzaIngredientsModel)
        {
            var pizzaIngredients = new List<PizzaIngredient>();

            foreach(var pizzaIngredientModel in pizzaIngredientsModel)
            {
                var pizzaIngredient = new PizzaIngredient();
                pizzaIngredient.IngredientId = pizzaIngredientModel.IngredientId;
                pizzaIngredient.PizzaId = pizzaId;
                pizzaIngredients.Add(pizzaIngredient);
            }

            return pizzaIngredients.OrderBy(x => x.IngredientId).ToList();
        }
    }
}
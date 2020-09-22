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
            CreateMap<Pizza,PizzaModel>()
            .ForMember(m => m.PizzaName, o => o.MapFrom(src => src.Name))
            .ForMember(m => m.PizzaId, o => o.MapFrom(src => src.PizzaId))
            .ForMember(m => m.DoughType, o => o.MapFrom(src => src.DoughType))
            .ForMember(m => m.IsCalzone, o => o.MapFrom(src => src.isCalzone))
            .ForMember(m => m.PizzaIngredientsModel, o => o.MapFrom(src => MapPizzaIngredientsToPizzaIngredientsModel(src.PizzaIngredients)));

            CreateMap<PizzaModel,Pizza>()
            .ForMember(m => m.Name, o => o.MapFrom(src => src.PizzaName))
            .ForMember(m => m.PizzaId, o => o.MapFrom(src => src.PizzaId))
            .ForMember(m => m.DoughType, o => o.MapFrom(src => src.DoughType))
            .ForMember(m => m.isCalzone, o => o.MapFrom(src => src.IsCalzone))
            .ForMember(m => m.PizzaIngredients, o =>  o.MapFrom(src => MapPizzaIngredientsModelToPizzaIngredients(src.PizzaId, src.PizzaIngredientsModel)));

            CreateMap<IngredientModel,Ingredient>()
            .ForMember(m => m.IngredientId, o => o.MapFrom(src => src.IngredientId))
            .ForMember(m => m.Name, o => o.MapFrom(src => src.Name))
            .ForMember(m => m.Type, o => o.MapFrom(src => src.Type))
            .ForMember(m => m.PizzaIngredients, opt => opt.Ignore());
        }

        private List<PizzaIngredientsModel> MapPizzaIngredientsToPizzaIngredientsModel(ICollection<PizzaIngredient> pizzaIngredients)
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
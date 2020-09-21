using System;
using System.Collections.Generic;
using System.Linq;
using ItalianoPizzaAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ItalianoPizzaAPI.Data.Migrations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new PizzaContext(serviceProvider.GetRequiredService<DbContextOptions<PizzaContext>>()))
            {
                if(context.Pizzas.Any())
                {
                    return;
                }

                context.Ingredients.AddRange(
                    new Ingredient
                    {
                        IngredientId = 1,
                        Name = "Onion",
                        Type = "Veggies"
                    },
                    new Ingredient
                    {
                        IngredientId = 2,
                        Name = "Tomato",
                        Type = "Veggies"
                    },
                    new Ingredient
                    {
                        IngredientId = 3,
                        Name = "Peppers",
                        Type = "Veggies"
                    },
                    new Ingredient
                    {
                        IngredientId = 4,
                        Name = "Jalapeno",
                        Type = "Veggies"
                    },
                     new Ingredient
                    {
                        IngredientId = 5,
                        Name = "Provolone",
                        Type = "Cheese"
                    },
                    new Ingredient
                    {
                        IngredientId = 6,
                        Name = "Pepper Jack",
                        Type = "Cheese"
                    },
                    new Ingredient
                    {
                        IngredientId = 7,
                        Name = "Cheddar",
                        Type = "Cheese"
                    },
                    new Ingredient
                    {
                        IngredientId = 8,
                        Name = "Ham",
                        Type = "Meat"
                    },
                    new Ingredient
                    {
                        IngredientId = 9,
                        Name = "Chicken",
                        Type = "Meat"
                    }
                );

                context.Pizzas.AddRange(
                    new Pizza
                    {
                       PizzaId =1,
                       Name = "New York Style Pizza",
                       DoughType = "New York Style",
                       isCalzone = false,
                       PizzaIngredients = new List<PizzaIngredient>(){
                        new PizzaIngredient{
                            IngredientId = 1,
                            PizzaId = 1
                        },
                        new PizzaIngredient{
                            IngredientId = 2,
                            PizzaId = 1
                        },
                        new PizzaIngredient{
                            IngredientId = 5,
                            PizzaId = 1
                        },
                        new PizzaIngredient{
                            IngredientId = 8,
                            PizzaId = 1
                        },                    
                      }
                    },
                    new Pizza
                    {
                       PizzaId = 2,
                       Name = "Neapolitan Pizza",
                       DoughType = "Neapolitan",
                       isCalzone = false,
                       PizzaIngredients = new List<PizzaIngredient>(){
                        new PizzaIngredient{
                            IngredientId = 2,
                            PizzaId = 2
                        },
                        new PizzaIngredient{
                            IngredientId = 5,
                            PizzaId = 2
                        },
                        new PizzaIngredient{
                            IngredientId = 6,
                            PizzaId = 2
                        },
                        new PizzaIngredient{
                            IngredientId = 9,
                            PizzaId = 2
                        },                    
                      }                       
                    },
                    new Pizza
                    {
                       PizzaId = 3,
                       Name = "Sicilian Pizza",
                       DoughType = "Sicilian",
                       isCalzone = false, 
                       PizzaIngredients = new List<PizzaIngredient>(){
                        new PizzaIngredient{
                            IngredientId = 3,
                            PizzaId = 3
                        },
                        new PizzaIngredient{
                            IngredientId = 4,
                            PizzaId = 3
                        },
                        new PizzaIngredient{
                            IngredientId = 6,
                            PizzaId = 3
                        },
                        new PizzaIngredient{
                            IngredientId = 9,
                            PizzaId = 3
                        },                    
                      }                      
                    }               
                );

                context.SaveChanges();
            }
        }
    }
}
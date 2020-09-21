using System.ComponentModel.DataAnnotations;

namespace ItalianoPizzaAPI.Data.Entities
{
    public class PizzaIngredient
    {
        [Key]
        public int PizzaId { get; set; }
        [Key]
        public int IngredientId { get; set; }

        public Pizza Pizza { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}
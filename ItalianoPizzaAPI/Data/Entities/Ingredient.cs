using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItalianoPizzaAPI.Data.Entities
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public ICollection<PizzaIngredient> PizzaIngredients { get; set; }
    }
}
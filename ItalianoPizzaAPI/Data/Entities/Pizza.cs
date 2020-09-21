using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItalianoPizzaAPI.Data.Entities
{
    public class Pizza
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PizzaId { get; set; }

        public string Name { get; set; }

        public string DoughType { get; set; }

        public bool isCalzone { get; set; }

        public ICollection<PizzaIngredient> PizzaIngredients { get; set; }

    }
}
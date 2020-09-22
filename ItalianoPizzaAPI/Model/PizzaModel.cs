using System.Collections.Generic;

namespace ItalianoPizzaAPI.Model
{
    public class PizzaModel
    {
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public string DoughType { get; set; }
        public bool IsCalzone { get; set; }
        public List<PizzaIngredientsModel> PizzaIngredientsModel { get; set; }
    }
}
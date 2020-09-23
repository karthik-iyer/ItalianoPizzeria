using System;

namespace ItalianoPizzaAPI.Exceptions
{
    public class PizzaNotFoundException : Exception
    {
        public PizzaNotFoundException()
        {
            
        }


        public PizzaNotFoundException(string pizzaNotFoundError) : base(pizzaNotFoundError)
        {   
        }
    }
}
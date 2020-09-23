using System;

namespace ItalianoPizzaAPI.Exceptions
{
    public class PizzaAlreadyExistsException : Exception
    {
         public PizzaAlreadyExistsException()
        {
            
        }

        public PizzaAlreadyExistsException(string pizzaAlreadyExistsError) : base(pizzaAlreadyExistsError)
        {}
    }
}
using System;

namespace ItalianoPizzaAPI.Exceptions
{
    public class PizzaNotUpdatedException : Exception
    {
        public PizzaNotUpdatedException()
        {
            
        }

        public PizzaNotUpdatedException(string PizzaNotUpdatedError) : base(PizzaNotUpdatedError)
        {
            
        }
    }
}
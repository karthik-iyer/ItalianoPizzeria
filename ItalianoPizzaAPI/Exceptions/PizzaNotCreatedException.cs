using System;

namespace ItalianoPizzaAPI.Exceptions
{
    public class PizzaNotCreatedException: Exception
    {
        public PizzaNotCreatedException()
        {
            
        }

        public PizzaNotCreatedException(string PizzaNotCreatedError): base(PizzaNotCreatedError)
        {
            
        }
    }
}
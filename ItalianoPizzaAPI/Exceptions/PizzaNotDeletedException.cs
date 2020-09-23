using System;

namespace ItalianoPizzaAPI.Exceptions
{
    public class PizzaNotDeletedException : Exception
    {
        public PizzaNotDeletedException()
        {
        }

        public PizzaNotDeletedException(string PizzaNotDeletedError): base(PizzaNotDeletedError)
        {   
        }
    }
}
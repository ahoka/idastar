using System;

namespace IdaStar
{
    public class SolutionNotFoundException : Exception
    {
        public SolutionNotFoundException(string message) : base(message)
        {
        }

        public SolutionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
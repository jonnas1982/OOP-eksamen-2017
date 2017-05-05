using System;

namespace Eksamensopgave2017
{
    class ProductDoesNotExsistException : Exception
    {
        public ProductDoesNotExsistException() { }

        public ProductDoesNotExsistException(string message) : base(message) { }

        public ProductDoesNotExsistException(string message, Exception inner) : base(message, inner) { }

    }
}

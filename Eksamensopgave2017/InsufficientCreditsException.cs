using System;

namespace Eksamensopgave2017
{
    class InsufficientCreditsException : Exception
    {
        //https://msdn.microsoft.com/en-us/library/87cdya3t(v=vs.110).aspx

        public InsufficientCreditsException() { }

        public InsufficientCreditsException(string message) : base(message) { }

        public InsufficientCreditsException(string message, Exception inner) : base(message, inner) { }
    }
}

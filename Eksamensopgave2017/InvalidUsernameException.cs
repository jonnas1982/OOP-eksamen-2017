using System;

namespace Eksamensopgave2017
{
    class InvalidUsernameException : Exception
    {
        //https://msdn.microsoft.com/en-us/library/87cdya3t(v=vs.110).aspx

        public InvalidUsernameException() { }

        public InvalidUsernameException(string message) : base(message) { }

        public InvalidUsernameException(string message, Exception inner) : base(message, inner) { }
    }
}

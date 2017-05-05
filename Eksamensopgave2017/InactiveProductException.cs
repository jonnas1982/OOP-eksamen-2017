using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    class InactiveProductException : Exception
    {
        //https://msdn.microsoft.com/en-us/library/87cdya3t(v=vs.110).aspx

        public InactiveProductException() { }

        public InactiveProductException(string message) : base(message) { }

        public InactiveProductException(string message, Exception inner) : base(message, inner) { }
    }
}

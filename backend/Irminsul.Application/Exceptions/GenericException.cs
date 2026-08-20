using System;
using System.Collections.Generic;
using System.Text;

namespace Irminsul.Application.Exceptions
{
    public class GenericException : Exception
    {
        public GenericException() : base("Ocorreu um erro genérico.")
        {
        }
    }
}

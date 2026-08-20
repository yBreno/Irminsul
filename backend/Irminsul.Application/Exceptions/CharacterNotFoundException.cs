using System;
using System.Collections.Generic;
using System.Text;

namespace Irminsul.Application.Exceptions
{
    public class CharacterNotFoundException : Exception
    {
        public CharacterNotFoundException() : base("Personagem não encontrado.")
        {
        }
    }
}

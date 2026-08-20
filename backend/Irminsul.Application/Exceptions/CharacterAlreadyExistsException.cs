using System;
using System.Collections.Generic;
using System.Text;

public class CharacterAlreadyExistsException : Exception
{
    public CharacterAlreadyExistsException()
        : base("Este personagem já existe.")
    {
    }
}

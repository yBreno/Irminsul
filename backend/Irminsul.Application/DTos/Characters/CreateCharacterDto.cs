using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Irminsul.Domain.Enums;

namespace Irminsul.Application.DTos.Characters
{
    public record CreateCharacterDto (string name, string title, CharacterRarity rarity, Vision vision, WeaponType weaponType, Nation nation, string imageUrl, string description, string lore)
    {
        
    }
}

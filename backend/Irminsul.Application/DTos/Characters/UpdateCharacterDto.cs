using Irminsul.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Irminsul.Application.DTos.Characters
{
    public record UpdateCharacterDto (string name, string title, CharacterRarity rarity, Vision vision, WeaponType weaponType, Nation nation, string imageUrl, string description, string lore)
    {

    }
}

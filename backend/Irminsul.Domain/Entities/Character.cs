using Irminsul.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Irminsul.Domain.Entities
{
    public class Character
    {
        public Character(string name, string title, CharacterRarity rarity, Vision vision, WeaponType weaponType, Nation nation, string imageUrl, string description, string lore)
        {
            Id = Guid.NewGuid();
            Name = name;
            Title = title;
            Rarity = rarity;
            Vision = vision;
            WeaponType = weaponType;
            Nation = nation;
            ImageUrl = imageUrl;
            Description = description;
            Lore = lore;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Title { get; private set; }
        public CharacterRarity Rarity { get; private set; }
        public Vision Vision { get; private set; }
        public WeaponType WeaponType { get; private set; }
        public Nation Nation { get; private set; }
        public string ImageUrl { get; private set; }
        public string Description { get; private set; }
        public string Lore { get; private set; }

        private Character() { }

        public void Update(string name, string title, CharacterRarity rarity, Vision vision, WeaponType weaponType, Nation nation, string imageUrl, string description, string lore)
        {
            Name = name;
            Title = title;
            Rarity = rarity;
            Vision = vision;
            WeaponType = weaponType;
            Nation = nation;
            ImageUrl = imageUrl;
            Description = description;
            Lore = lore;
        }

    }
    
}

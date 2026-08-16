using Irminsul.Application.Interfaces;
using Irminsul.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Irminsul.Domain.Enums;

namespace Irminsul.Application.Services
{
    public class CharacterService
    {
        private readonly ICharacterRepository _characterRepository;

        public CharacterService(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _characterRepository.GetAllAsync();
        }

        public async Task<Character?> GetCharacterByIdAsync(Guid id)
        {
            return await _characterRepository.GetByIdAsync(id);
        }

        public async Task<Character> CreateCharacterAsync(string name, string title, CharacterRarity rarity, Vision vision, WeaponType weaponType, Nation nation, string imageUrl, string description, string lore)
        {
            var character = new Character(name, title, rarity, vision, weaponType, nation, imageUrl, description, lore);
            
            return await _characterRepository.AddAsync(character);
        }

    }
}

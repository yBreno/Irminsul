using Irminsul.Application.Interfaces;
using Irminsul.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Irminsul.Domain.Enums;
using Irminsul.Application.DTos.Characters;

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

        public async Task<Character> CreateCharacterAsync(CreateCharacterDto dto)
        {
            var character = new Character(dto.name, dto.title, dto.rarity, dto.vision, dto.weaponType, dto.nation, dto.imageUrl, dto.description, dto.lore);
            
            return await _characterRepository.AddAsync(character);
        }

        public async Task<Character> UpdateCharacterAsync(Guid id, UpdateCharacterDto dto)
        {
            var existingCharacter = await _characterRepository.GetByIdAsync(id);
            if (existingCharacter == null)
            {
                throw new Exception("Personagem não encontrado.");
            }
            existingCharacter.Update(dto.name, dto.title, dto.rarity, dto.vision, dto.weaponType, dto.nation, dto.imageUrl, dto.description, dto.lore);
            return await _characterRepository.UpdateAsync(existingCharacter);
        }

    }
}

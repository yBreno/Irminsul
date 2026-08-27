using Irminsul.Application.Interfaces;
using Irminsul.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Irminsul.Domain.Enums;
using Irminsul.Application.DTos.Characters;
using Irminsul.Application.Exceptions;

namespace Irminsul.Application.Services
{
    public class CharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IGenshinApiClient _genshinApiClient;

        public CharacterService(ICharacterRepository characterRepository, IGenshinApiClient genshinApiClient)
        {
            _characterRepository = characterRepository;
            _genshinApiClient = genshinApiClient;
        }

        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _characterRepository.GetAllAsync();
        }

        public async Task<Character> GetCharacterByIdAsync(Guid id)
        {
            var character = await _characterRepository.GetByIdAsync(id);

            if (character == null)
            {
                throw new CharacterNotFoundException();
            }

            return character;
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
                throw new CharacterNotFoundException();
            }
            existingCharacter.Update(dto.name, dto.title, dto.rarity, dto.vision, dto.weaponType, dto.nation, dto.imageUrl, dto.description, dto.lore);
            return await _characterRepository.UpdateAsync(existingCharacter);
        }

        public async Task<Character> DeleteCharacterAsync(Guid id)
        {
            var existingCharacter = await _characterRepository.GetByIdAsync(id);
            if (existingCharacter == null)
            {
                throw new CharacterNotFoundException();
            }
            return await _characterRepository.DeleteAsync(id);
        }

        public async Task<Character> GetCharacterFromExternalApiAsync(string name)
        {
            var characterDto = await _genshinApiClient.GetCharacterAsync(name);
            var characterImagesDto = await _genshinApiClient.GetCharacterImagesAsync(name);


            if (characterDto == null)
            {
                throw new CharacterNotFoundException();
            }

            var character = new Character(
                characterDto.name,
                characterDto.title,
                (CharacterRarity)characterDto.rarity,
                Enum.Parse<Vision>(characterDto.elementText, true),
                Enum.Parse<WeaponType>(characterDto.weaponText, true),
                Enum.Parse<Nation>(characterDto.region, true),
                characterImagesDto?.hoyowiki_icon ?? string.Empty,
                characterDto.description,
                string.Empty
            );

            return character;
        }

        public async Task<Character> ImportCharacterFromExternalApiAsync(string name)
        {
            var character = await GetCharacterFromExternalApiAsync(name);

            return await _characterRepository.AddAsync(character);
        }
    }
}

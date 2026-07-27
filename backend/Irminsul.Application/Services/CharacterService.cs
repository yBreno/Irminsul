using Irminsul.Application.Interfaces;
using Irminsul.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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


    }
}

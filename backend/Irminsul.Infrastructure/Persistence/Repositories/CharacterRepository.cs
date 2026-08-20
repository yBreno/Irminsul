using Irminsul.Domain.Entities;
using Irminsul.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Irminsul.Application.Interfaces;
using System.Runtime.CompilerServices;

namespace Irminsul.Infrastructure.Persistence.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {

        private readonly IrminsulContext _context;

        public CharacterRepository(IrminsulContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Character>> GetAllAsync()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<Character?> GetByIdAsync(Guid id)
        {
            return await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Character> AddAsync(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        public async Task<Character> UpdateAsync(Character character)
        {
            var existingCharacter = await _context.Characters.FindAsync(character.Id);
            if (existingCharacter == null)
            {
                throw new Exception("Personagem não encontrado.");
            }
            existingCharacter.Update(character.Name, character.Title, character.Rarity, character.Vision, character.WeaponType, character.Nation, character.ImageUrl, character.Description, character.Lore);
            await _context.SaveChangesAsync();
            return existingCharacter;
        }

        public async Task<Character> DeleteAsync(Guid id)
        {
            var existingCharacter = await _context.Characters.FindAsync(id);

            _context.Characters.Remove(existingCharacter!);
            await _context.SaveChangesAsync();

            return existingCharacter!;
        }
    }
}

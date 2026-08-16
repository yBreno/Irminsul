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

        
    }
}

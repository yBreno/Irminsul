using Irminsul.Application.Interfaces;
using Irminsul.Domain.Entities;
using Irminsul.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore

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

        
    }
}

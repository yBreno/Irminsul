using Irminsul.Domain.Entities;

namespace Irminsul.Application.Interfaces;

public interface ICharacterRepository
{
    Task<IEnumerable<Character>> GetAllAsync();

    Task<Character?> GetByIdAsync(Guid id);
    Task<Character> AddAsync(Character character);
    Task<Character> UpdateAsync(Character character);

}
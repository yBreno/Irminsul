using Irminsul.Application.Services;
using Irminsul.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Irminsul.Domain.Enums;

namespace Irminsul.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class CharactersController : ControllerBase
    {

        private readonly CharacterService _characterService;

        public CharactersController(CharacterService characterService)
        {
            _characterService = characterService;
        }

        //Lista de todos os personagens
        [HttpGet("characters")]
        public async Task<IActionResult> FullList()
        {
            var characters = await _characterService.GetAllCharactersAsync();
            if (characters == null)
            {
                return NotFound();
            }

            return Ok(characters);
        }

        //Lista personagem por ID
        [HttpGet("characters/{id}")]
        public async Task<IActionResult> GetCharacterById (Guid id)
        {
            var characters = await _characterService.GetCharacterByIdAsync(id);
            
            if (characters == null)
            {
                return NotFound();
            }

            return Ok(characters);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharacter([FromBody] Character character)
        {
            var createdCharacter = await _characterService.CreateCharacterAsync(
                character.Name,
                character.Title,
                character.Rarity,
                character.Vision,
                character.WeaponType,
                character.Nation,
                character.ImageUrl,
                character.Description,
                character.Lore
            );

            return CreatedAtAction(nameof(GetCharacterById), new { id = createdCharacter.Id }, createdCharacter);
        }

    }
}

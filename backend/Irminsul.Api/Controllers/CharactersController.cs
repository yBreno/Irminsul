using FluentValidation;
using Irminsul.Application.DTos.Characters;
using Irminsul.Application.Services;
using Irminsul.Domain.Entities;
using Irminsul.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Irminsul.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class CharactersController : ControllerBase
    {

        private readonly CharacterService _characterService;
        private readonly IValidator<CreateCharacterDto> _validator;

        public CharactersController(CharacterService characterService, IValidator<CreateCharacterDto> validator)
        {
            _characterService = characterService;
            _validator = validator;

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
        public async Task<IActionResult> CreateCharacter(CreateCharacterDto character)
        {
            var validationResult = await _validator.ValidateAsync(character);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var createdCharacter = await _characterService.CreateCharacterAsync(character);

            return CreatedAtAction(nameof(GetCharacterById), new { id = createdCharacter.Id }, createdCharacter);
        }

    }
               
}

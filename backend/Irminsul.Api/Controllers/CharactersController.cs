using FluentValidation;
using Irminsul.Application.DTos.Characters;
using Irminsul.Application.Exceptions;
using Irminsul.Application.Services;
using Irminsul.Domain.Entities;
using Irminsul.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Irminsul.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class CharactersController : ControllerBase
    {

        private readonly CharacterService _characterService;
        private readonly IValidator<CreateCharacterDto> _validator;
        private readonly IValidator<UpdateCharacterDto> _updateValidator;

        public CharactersController(CharacterService characterService, IValidator<CreateCharacterDto> validator, IValidator<UpdateCharacterDto> updateValidator)
        {
            _characterService = characterService;
            _validator = validator;
            _updateValidator = updateValidator;

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

        [HttpPut("characters/{id}")]
        public async Task<IActionResult> UpdateCharacter(Guid id, UpdateCharacterDto character)
        {
            var validationResult = await _updateValidator.ValidateAsync(character);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var updatedCharacter = await _characterService.UpdateCharacterAsync(id, character);
            
            return Ok(updatedCharacter);
        }

        [HttpDelete("characters/{id}")]
        public async Task<IActionResult> DeleteCharacter(Guid id)
        {
            var deletedCharacter = await _characterService.DeleteCharacterAsync(id);
            
            return Ok(deletedCharacter);
        }
    }
               
}

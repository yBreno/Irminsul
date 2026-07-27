using Irminsul.Application.Services;
using Irminsul.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        //Futuro: Adicionar rotas exclusivas para Artefatos, Armas e Elementos
    }
}

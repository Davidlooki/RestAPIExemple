using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestAPIExemple.Models;

namespace RestAPIExemple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonsController(IPokemonRepository pokemonRepository) => _pokemonRepository = pokemonRepository;

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Pokemon>>> GetPokemonsByType(PokemonType pokemonType)
        {
            try
            {
                var result = await _pokemonRepository.GetPokemonsByType(pokemonType);

                return result.Any() ? Ok(result) : NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        
        [HttpGet("{GetPokemonByName}")]
        public async Task<ActionResult<Pokemon>> GetPokemonByName(string name)
        {
            try
            {
                var result = await _pokemonRepository.GetPokemonByName(name);

                return result != null ? result : NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        
        [HttpGet]
        public async Task<ActionResult> GetPokemons()
        {
            try
            {
                return Ok(await _pokemonRepository.GetPokemons());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pokemon>> GetPokemon(int id)
        {
            try
            {
                var result = await _pokemonRepository.GetPokemon(id);

                return result != null ? result : NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Pokemon>> CreatePokemon(Pokemon pokemon)
        {
            try
            {
                if (pokemon == null) return BadRequest();

                var poke = await _pokemonRepository.GetPokemon(pokemon.Id);

                if (poke != null)
                {
                    ModelState.AddModelError("Id", "Pokemon Id already in use");

                    return BadRequest(ModelState);
                }

                var createdPokemon = await _pokemonRepository.AddPokemon(pokemon);

                return CreatedAtAction(nameof(GetPokemon), new
                { id = createdPokemon.Id }, createdPokemon);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new pokemon record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Pokemon>> UpdatePokemon(int id, Pokemon pokemon)
        {
            try
            {
                if (id != pokemon.Id)
                    return BadRequest("Pokemon Id mismatch");

                var pokemontoUpdate = await _pokemonRepository.GetPokemon(id);

                if (pokemontoUpdate == null)
                    return NotFound($"Pokemon with Id = {id} not found");

                return await _pokemonRepository.UpdatePokemon(pokemon);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating pokemon record");
            }
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Pokemon>> DeletePokemon(int id)
        {
            try
            {
                var pokemontoUpdate = await _pokemonRepository.GetPokemon(id);

                if (pokemontoUpdate == null)
                    return NotFound($"Pokemon with Id = {id} not found");

                await _pokemonRepository.DeletePokemon(id);

                return Ok($"Pokemon with Id = {id} deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting pokemon record");
            }
        }
    }
}
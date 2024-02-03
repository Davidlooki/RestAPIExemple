using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPIExemple.Models
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<Pokemon>> GetPokemonsByType(PokemonType pokemonType);
        Task<Pokemon> GetPokemon(int id);
        Task<IEnumerable<Pokemon>> GetPokemons();
        Task<Pokemon> GetPokemonByName(string name);
        Task<Pokemon> AddPokemon(Pokemon pokemon);
        Task<Pokemon> UpdatePokemon(Pokemon pokemon);
        Task DeletePokemon(int id);
    }
}
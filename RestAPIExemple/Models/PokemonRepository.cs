using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestAPIExemple.Models
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly AppDbContext _appDbContext;

        public PokemonRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<IEnumerable<Pokemon>> GetPokemonsByType(PokemonType pokemonType)
        {
            IQueryable<Pokemon> query = _appDbContext.Pokemons;
            query = query.Where(p => p.Type.HasFlag(pokemonType));
            return await query.ToListAsync();
        }

        public async Task<Pokemon> GetPokemon(int id) =>
            await _appDbContext.Pokemons.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Pokemon>> GetPokemons() =>
            await _appDbContext.Pokemons.ToListAsync();

        public async Task<Pokemon> GetPokemonByName(string name) =>
            await _appDbContext.Pokemons.FirstOrDefaultAsync(p => p.Name == name);

        public async Task<Pokemon> AddPokemon(Pokemon pokemon)
        {
            var result = await _appDbContext.Pokemons.AddAsync(pokemon);
            await _appDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Pokemon> UpdatePokemon(Pokemon pokemon)
        {
            var result = await _appDbContext.Pokemons.FirstOrDefaultAsync(p => p.Id == pokemon.Id);

            if (result != null)
            {
                result.Name = pokemon.Name;
                result.Description = pokemon.Description;
                //result.ImagePath = pokemon.ImagePath;

                await _appDbContext.SaveChangesAsync();
            }

            return result;
        }

        public async Task DeletePokemon(int id)
        {
            var result = await _appDbContext.Pokemons.FirstOrDefaultAsync(p => p.Id == id);

            if (result != null)
            {
                _appDbContext.Pokemons.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
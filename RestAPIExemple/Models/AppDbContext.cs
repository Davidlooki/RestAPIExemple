using Microsoft.EntityFrameworkCore;

namespace RestAPIExemple.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pokemon> Pokemons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Send pokemon table
            modelBuilder.Entity<Pokemon>().HasData(
                new Pokemon
                { 
                  Id = 1,
                  Name = "Bulbasaur",
                  Description = "...",
                  //ImagePath = "Images/Bulbasaur.png"
                });
            modelBuilder.Entity<Pokemon>().HasData(
                new Pokemon
                { 
                Id = 2,
                Name = "Ivysaur",
                Description = "...",
                //ImagePath = "Images/Ivysaur.png" 
                });
            modelBuilder.Entity<Pokemon>().HasData(
                new Pokemon
                { 
                Id = 3,
                Name = "Venosaur",
                Description = "...",
                //ImagePath = "Images/Venosaur.png" 
                });
            modelBuilder.Entity<Pokemon>().HasData(
                new Pokemon
                { 
                Id = 4,
                Name = "Charmander",
                Description = "...",
                //ImagePath = "Images/Charmander.png" 
                });
            modelBuilder.Entity<Pokemon>().HasData(
                new Pokemon
                { 
                Id = 5,
                Name = "Charmeleon",
                Description = "...",
                //ImagePath = "Images/Charmeleon.png" 
                });
            modelBuilder.Entity<Pokemon>().HasData(
                new Pokemon
                { 
                Id = 6,
                Name = "Charizard",
                Description = "...",
                //ImagePath = "Images/Charizard.png" 
                });
            modelBuilder.Entity<Pokemon>().HasData(
                new Pokemon
                { 
                Id = 7,
                Name = "Squirtle",
                Description = "...",
                //ImagePath = "Images/Squirtle.png" 
                });
            modelBuilder.Entity<Pokemon>().HasData(
                new Pokemon
                { 
                Id = 8,
                Name = "Wartortle",
                Description = "...",
                //ImagePath = "Images/Wartortle.png" 
                });
            modelBuilder.Entity<Pokemon>().HasData(
                new Pokemon
                { 
                Id = 9,
                Name = "Blastoise",
                Description = "...",
                //ImagePath = "Images/Blastoise.png" 
                });
        }
    }
}
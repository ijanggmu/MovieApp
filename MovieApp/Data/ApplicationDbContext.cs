using Microsoft.EntityFrameworkCore;
using MovieApp.Models;
using System.Reflection.Metadata;

namespace MovieApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Seed data
        modelBuilder.Entity<Movie>().HasData(
            new Movie
            {
                Id = 1000,
                Title = "The Little Mermaid",
                Description = "The youngest of King Triton’s daughters, and the most defiant, Ariel longs to find out more about the world beyond the sea, and while visiting the surface, falls for the dashing Prince Eric. With mermaids forbidden to interact with humans, Ariel makes a deal with the evil sea witch, Ursula, which gives her a chance to experience life on land, but ultimately places her life – and her father’s crown – in jeopardy.",
                BannerUrl = "littlemermaid.jpg",
                MovieYear = "2023"
            },
            new Movie
            {
                Id = 1001,
                Title = "Barbie",
                Description = "A doll living in 'Barbie land' is expelled for not being perfect enough and sets off on an adventure in the real world. A Live-action feature film based on the popular line of Barbie toys.",
                BannerUrl = "barbie.jpg",
                MovieYear = "2023"
            },
            new Movie
            {
                Id = 1002,
                Title = "Oppenheimer",
                Description = "The story of J. Robert Oppenheimer’s role in the development of the atomic bomb during World War II.",
                BannerUrl = "Oppenheimer.jpg",
                MovieYear = "2023"
            },
            new Movie
            {
                Id = 1003,
                Title = "The Flash",
                Description = "When his attempt to save his family inadvertently alters the future, Barry Allen becomes trapped in a reality in which General Zod has returned and there are no Super Heroes to turn to. In order to save the world that he is in and return to the future that he knows, Barry's only hope is to race for his life. But will making the ultimate sacrifice be enough to reset the universe?",
                BannerUrl = "Flash.jpg",
                MovieYear = "2023"
            },
            new Movie
            {
                Id = 1004,
                Title = "John Wick: Chapter 4",
                Description = "With the price on his head ever increasing, John Wick uncovers a path to defeating The High Table. But before he can earn his freedom, Wick must face off against a new enemy with powerful alliances across the globe and forces that turn old friends into foes",
                BannerUrl = "JohnWick4.jpg",
                MovieYear = "2023"
            },
            new Movie
            {
                Id = 1005,
                Title = "Spider-Man: Across the Spider-Verse",
                Description = "The continuing story of Miles Morales and the many other Spider-People from different realities.",
                BannerUrl = "Spider-man-across.jpg",
                MovieYear = "2023"
            }
        );
    }
    public DbSet<Movie> Movies { get; set; }
}

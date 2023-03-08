using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Assignment3_V2.Models;

namespace Assignment3_V2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Assignment3_V2.Models.Movies> Movies { get; set; }
        public DbSet<Assignment3_V2.Models.Actors> Actors { get; set; }
        
        public DbSet<Assignment3_V2.Models.MoviesActors>? MoviesActors { get; set; }

    }
}
using Microsoft.EntityFrameworkCore;
using Movie_Application.Models;

namespace Movie_Application.Data
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(e => new {e.MovieId,e.ActoId});
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie)
                                              .WithMany(e => e.actor_Movies)
                                              .HasForeignKey(f=>f.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor)
                                      .WithMany(e => e.actor_Movies)
                                      .HasForeignKey(f => f.ActoId);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<Actor_Movie> Actor_Movies { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}

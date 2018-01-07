namespace CinemaShow.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public class CinemaShowContext : IdentityDbContext<User>
    {
        private static readonly string DefaultContextName = nameof(CinemaShowContext).Replace("Context", string.Empty);

        public CinemaShowContext() : base(DefaultContextName, throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<ImageUrl> ImageUrls { get; set; }

        public static CinemaShowContext Create()
        {
            return new CinemaShowContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Categories)
                .WithMany(c => c.Movies)
                .Map(mtmnpc =>
                {
                    mtmnpc.MapLeftKey("MovieId");
                    mtmnpc.MapRightKey("CategoryId");
                    mtmnpc.ToTable("MovieCategories");
                });

            modelBuilder.Entity<Movie>()
                .HasRequired(m => m.Image)
                .WithRequiredPrincipal(i => i.Movie)
                .WillCascadeOnDelete(false);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
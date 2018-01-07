namespace CinemaShow.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<CinemaShowContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CinemaShowContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole { Name = "Admin" });
                context.Roles.Add(new IdentityRole { Name = "Regular" });
                context.Categories.Add(new Category { Name = "Sci-Fi" });
            }
        }
    }
}
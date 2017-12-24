namespace CinemaShow.Application.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Data;
    using Models;
    using Models.ViewModels.Movie;
    using Services;

    public class MovieController : BaseController
    {
        public MovieController()
        {
        }

        public MovieController(
            SignInManager signInManager,
            UserManager userManager,
            CinemaShowContext context) : base(
                signInManager,
                userManager,
                context)
        {
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Add(NewMovieViewModel model)
        {
            var dbContext = this.CinemaShowDbContext;
            var contentManager = new ServerContentManager();
            var categories = dbContext.Categories;
            var path = contentManager.UploadFile(model.Image, DateTime.Now);

            var imageUrl = new ImageUrl
            {
                Id = Guid.NewGuid(),
                Url = path
            };
            dbContext.ImageUrls.Add(imageUrl);

            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Description = model.Description,
                Image = imageUrl,
                YoutubeUrl = model.YoutubeUrl,
                Price = model.Price
            };

            foreach (var category in model.Categories)
            {
                var currentCategory = categories.FirstOrDefault(c => c.Name.ToLower() == category.ToLower());
                movie.Categories.Add(currentCategory);
            }

            dbContext.Movies.Add(movie);
            dbContext.SaveChanges();
            return this.RedirectToAction("Index", "Home");
        }
    }
}
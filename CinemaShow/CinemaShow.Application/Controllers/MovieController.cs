namespace CinemaShow.Application.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
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

        public ActionResult MovieInfo(string id)
        {
            return this.View(this.CinemaShowDbContext
                .Movies
                .Include(m => m.Categories)
                .Include(m => m.Image)
                .FirstOrDefault(m => m.Id.ToString() == id));
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
            var data = this.CinemaShowDbContext;
            var contentManager = new ServerContentManager();
            var categories = data.Categories;
            var path = contentManager.UploadFile(model.Image, DateTime.Now);

            var imageUrl = new ImageUrl
            {
                Id = Guid.NewGuid(),
                Url = path
            };
            data.ImageUrls.Add(imageUrl);

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

            data.Movies.Add(movie);
            data.SaveChanges();
            return this.RedirectToAction("Index", "Home");
        }
    }
}
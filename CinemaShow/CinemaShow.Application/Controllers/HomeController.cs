namespace CinemaShow.Application.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Data;
    using Services;

    public class HomeController : BaseController
    {
        public HomeController()
        {
        }

        public HomeController(
            SignInManager signInManager,
            UserManager userManager,
            CinemaShowContext context) : base(
                signInManager,
                userManager,
                context)
        {
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            var movies = this.CinemaShowDbContext.Movies
                            .Include(m => m.Image)
                            .Include(m => m.Categories)
                            .OrderBy(m => m.Title)
                            .Skip((page - 1) * 8)
                            .Take(8)
                            .ToArray();
            return await Task.FromResult<ActionResult>(this.View(movies));
        }

        public ActionResult MovieInfo(string id)
        {
            return this.View(this.CinemaShowDbContext
                .Movies
                .Include(m => m.Categories)
                .Include(m => m.Image)
                .FirstOrDefault(m => m.Id.ToString() == id));
        }
    }
}
namespace CinemaShow.Application.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Data;
    using Models;

    public class ServiceController : ApiController
    {
        public IEnumerable<string> GetAllCategories()
        {
            return CinemaShowContext.Create().Categories.Select(c => c.Name).ToArray();
        }

        public void AddCategory(string category)
        {
            var ctx = CinemaShowContext.Create();
            ctx.Categories.Add(new Category { Name = category });
            ctx.SaveChanges();
        }

        public int GetMoviesCount()
        {
            return CinemaShowContext.Create().Movies.ToArray().Length;
        }
    }
}
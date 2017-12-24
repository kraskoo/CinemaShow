namespace CinemaShow.Models.ViewModels.Movie
{
    using System.Collections.Generic;
    using System.Web;

    public class NewMovieViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<string> Categories { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public string YoutubeUrl { get; set; }

        public decimal Price { get; set; }
    }
}
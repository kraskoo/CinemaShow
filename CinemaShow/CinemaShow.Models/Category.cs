namespace CinemaShow.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        private ICollection<Movie> movies;

        public Category()
        {
            this.movies = new HashSet<Movie>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies
        {
            get => this.movies;
            set => this.movies = value;
        }
    }
}

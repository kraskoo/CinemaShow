namespace CinemaShow.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Movie
    {
        private ICollection<Category> categories;

        public Movie()
        {
            this.categories = new HashSet<Category>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<Category> Categories
        {
            get => this.categories;
            set => this.categories = value;
        }

        public virtual ImageUrl Image { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string YoutubeUrl { get; set; }
    }
}
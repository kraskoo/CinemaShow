namespace CinemaShow.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ImageUrl
    {
        public Guid Id { get; set; }

        [Required]
        public string Url { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
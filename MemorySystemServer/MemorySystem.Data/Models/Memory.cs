namespace MemorySystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Memory
    {
        public Memory()
        {
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<Like>();
            this.Favorites = new HashSet<Favorite>();
        }

        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        public string Description { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public ICollection<Like> Likes { get; set; }

        public IEnumerable<Favorite> Favorites { get; set; }
    }
}

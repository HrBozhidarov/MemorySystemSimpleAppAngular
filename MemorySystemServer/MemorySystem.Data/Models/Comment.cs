namespace MemorySystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int PictureId { get; set; }

        public Picture Picture { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }
    }
}

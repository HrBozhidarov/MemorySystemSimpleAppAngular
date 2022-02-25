namespace MemorySystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int MemoryId { get; set; }

        public Memory Memory { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }
    }
}

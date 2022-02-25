namespace MemorySystem.Data.Models
{
    public class Favorite
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int MemoryId { get; set; }

        public Memory Memory { get; set; }
    }
}

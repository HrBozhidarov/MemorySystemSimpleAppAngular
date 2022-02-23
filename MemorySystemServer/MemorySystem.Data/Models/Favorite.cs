namespace MemorySystem.Data.Models
{
    public class Favorite
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int PictureId { get; set; }

        public Picture Picture { get; set; }
    }
}

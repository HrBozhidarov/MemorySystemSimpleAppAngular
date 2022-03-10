namespace MemorySystem.Controllers.Models.Output
{
    using MemorySystem.Infrastructure.AutomapperSettings;
    using MemorySystem.Services.Models;

    public class MemoryResponseModel : IMapFrom<MemoryModel>
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; set; }

        public string Owner { get; set; }

        public string OwnerProfilePicture { get; set; }

        public bool IsLikedFromCurrentUser { get; set; }

        public int Likes { get; set; }
    }
}

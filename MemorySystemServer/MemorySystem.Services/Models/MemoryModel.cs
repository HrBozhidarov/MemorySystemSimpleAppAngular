namespace MemorySystem.Services.Models
{
    using System.Linq;

    using AutoMapper;
    using MemorySystem.Data.Models;
    using MemorySystem.Infrastructure.AutomapperSettings;

    // Think about for this model?
    public class MemoryModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; set; }

        public string Owner { get; set; }

        public string OwnerProfilePicture { get; set; }

        public bool IsLikedFromCurrentUser { get; set; }

        public int Likes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Memory, MemoryModel>()
                .ForMember(pd => pd.Likes, opt => opt.MapFrom(p => p.Likes.Count()))
                .ForMember(pd => pd.Owner, opt => opt.MapFrom(p => p.Owner.UserName))
                .ForMember(pd => pd.IsLikedFromCurrentUser, opt => opt.MapFrom(p => p.Likes.Any(u => u.UserId == p.OwnerId)))
                .ForMember(pd => pd.OwnerProfilePicture, opt => opt.MapFrom(p => p.Owner.ProfileUrl));
        }
    }
}

namespace MemorySystem.Services.Models
{
    using MemorySystem.Data.Models;
    using MemorySystem.Infrastructure.AutomapperSettings;

    public class UserModel : BaseUserModel, IMapTo<User>, IMapFrom<User>
    {
        public string Email { get; set; }

        public string ProfileUrl { get; set; }
    }
}

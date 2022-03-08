using MemorySystem.Data.Models;
using MemorySystem.Infrastructure.AutomapperSettings;

namespace MemorySystem.Services.Models
{
    public class UserModel : BaseUserModel, IMapTo<User>, IMapFrom<User>
    {
        public string Email { get; set; }

        public string ProfileUrl { get; set; }
    }
}

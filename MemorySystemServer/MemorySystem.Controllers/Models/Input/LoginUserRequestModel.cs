using MemorySystem.Infrastructure.AutomapperSettings;
using MemorySystem.Services.Models;

namespace MemorySystemApp.Models.Identity
{
    public class LoginUserRequestModel : IMapTo<BaseUserModel>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}

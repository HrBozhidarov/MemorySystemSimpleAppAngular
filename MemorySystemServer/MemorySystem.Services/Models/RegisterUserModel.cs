using MemorySystem.Data.Models;
using MemorySystem.Infrastructure.AutomapperSettings;

namespace MemorySystem.Services.Models
{
    public class RegisterUserModel : IMapTo<User>
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string ProfileUrl { get; set; }

        public string Password { get; set; }
    }
}

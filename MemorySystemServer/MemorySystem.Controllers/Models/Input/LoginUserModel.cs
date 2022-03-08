namespace MemorySystemApp.Models.Identity
{
    using MemorySystem.Infrastructure.AutomapperSettings;
    using MemorySystem.Services.Models;

    public class LoginUserModel : IMapTo<BaseUserModel>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}

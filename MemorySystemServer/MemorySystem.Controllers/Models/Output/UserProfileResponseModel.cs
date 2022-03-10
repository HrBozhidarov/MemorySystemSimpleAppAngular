namespace MemorySystem.Controllers.Models.Output
{
    using MemorySystem.Infrastructure.AutomapperSettings;
    using MemorySystem.Services.Models;

    public class UserProfileResponseModel : IMapFrom<UserModel>
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string ProfileUrl { get; set; }

        public string Password { get; set; }
    }
}

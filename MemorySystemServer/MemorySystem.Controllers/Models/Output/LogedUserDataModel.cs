namespace MemorySystem.Controllers.Models.Output
{
    using MemorySystem.Infrastructure.AutomapperSettings;
    using MemorySystem.Services.Models;

    public class LogedUserDataModel : IMapFrom<LoginModel>
    {
        public string Token { get; set; }

        public string ProfileUrl { get; set; }

        public string Role { get; set; }
    }
}

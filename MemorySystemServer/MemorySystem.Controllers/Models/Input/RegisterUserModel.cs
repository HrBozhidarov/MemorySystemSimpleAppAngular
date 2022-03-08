namespace MemorySystemApp.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    using MemorySystem.Infrastructure.AutomapperSettings;
    using MemorySystem.Services.Models;

    public class RegisterUserModel : IMapTo<UserModel>
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        public string ProfileUrl { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

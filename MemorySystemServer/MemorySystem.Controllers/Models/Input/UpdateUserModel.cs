namespace MemorySystem.Controllers.Models.Input
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MemorySystem.Infrastructure.AutomapperSettings;
    using MemorySystem.Services.Models;

    public class UpdateUserModel : IMapTo<UserModel>
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public string ProfileUrl { get; set; }
    }
}

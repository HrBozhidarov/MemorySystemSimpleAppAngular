namespace MemorySystem.Controllers.Models.Input
{
    using System.ComponentModel.DataAnnotations;

    using MemorySystem.Data.Models;
    using MemorySystem.Infrastructure.AutomapperSettings;

    public class CreateMemoryModel : IMapTo<MemorySystem.Services.Models.CreateMemoryModel>
    {
        public CategoryType Type { get; set; }

        [Required]
        public string Url { get; set; }

        public string Description { get; set; }
    }
}

namespace MemorySystem.Services.Models
{
    using System.ComponentModel.DataAnnotations;
    using MemorySystem.Data.Models;
    using MemorySystem.Infrastructure.AutomapperSettings;

    public class MemoryRequestModel : IMapTo<Memory>
    {
        public CategoryType Type { get; set; }

        [Required]
        public string Url { get; set; }

        public string Description { get; set; }
    }
}

namespace MemorySystem.Services.Models
{
    using MemorySystem.Data.Models;
    using MemorySystem.Infrastructure.AutomapperSettings;

    public class CreateMemoryModel : IMapTo<Memory>
    {
        public CategoryType Type { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }
    }
}

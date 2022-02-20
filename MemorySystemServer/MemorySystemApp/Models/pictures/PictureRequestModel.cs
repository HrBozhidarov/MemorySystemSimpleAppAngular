namespace MemorySystemApp.Models.Pictures
{
    using System.ComponentModel.DataAnnotations;

    using MemorySystem.Infrastructure.AutomapperSettings;

    using MemorySystemApp.Data.Models;

    public class PictureRequestModel : IMapTo<Picture>
    {
        public CategoryType Type { get; set; }

        [Required]
        public string Url { get; set; }

        public string Description { get; set; }
    }
}

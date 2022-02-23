namespace MemorySystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.Pictures = new HashSet<Picture>();
        }

        public int Id { get; set; }

        [Required]
        public CategoryType Type { get; set; }

        public IEnumerable<Picture> Pictures { get; set; }
    }
}

namespace MemorySystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.Memories = new HashSet<Memory>();
        }

        public int Id { get; set; }

        [Required]
        public CategoryType Type { get; set; }

        public IEnumerable<Memory> Memories { get; set; }
    }
}

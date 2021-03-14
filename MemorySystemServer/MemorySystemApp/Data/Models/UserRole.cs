namespace MemorySystemApp.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class UserRole : IdentityUserRole<string>
    {
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}

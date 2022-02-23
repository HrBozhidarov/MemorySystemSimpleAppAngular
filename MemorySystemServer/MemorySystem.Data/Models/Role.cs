namespace MemorySystem.Data.Models
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    public class Role : IdentityRole
    {
        public Role()
        {
            this.UserRoles = new HashSet<UserRole>();
        }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

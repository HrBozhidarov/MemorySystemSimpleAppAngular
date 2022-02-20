namespace MemorySystemApp.Data.Models
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.Comments = new HashSet<Comment>();
            this.Pictures = new HashSet<Picture>();
            this.Likes = new HashSet<Like>();
            this.Favorites = new HashSet<Favorite>();
            this.UserRoles = new HashSet<UserRole>();
        }

        public string ProfileUrl { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<Picture> Pictures { get; set; }

        public IEnumerable<Like> Likes { get; set; }

        public IEnumerable<Favorite> Favorites { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}

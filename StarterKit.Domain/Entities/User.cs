using System;
using StarterKit.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace StarterKit.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;
            DeletedAt = null;
            IsDeleted = false;
        }
    }
}
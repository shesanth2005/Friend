using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Domain.Entities
{
    public class Customer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();           // Primary key
        public string UserId { get; set; }                                      // Foreign key to AppUser
        public string CustomerName { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Optional navigation property if you have AppUser entity
        public AppUser User { get; set; } = null!;
    }
}

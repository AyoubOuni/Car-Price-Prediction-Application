using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Projet.Models
{
    public class ResetPassword
    {
        [Key]
        public int Id { get; set; }

        // Foreign key to relate the reset request with a user
        public int UserId { get; set; }
        public User User { get; set; } // Navigation property to User model

        [MaxLength(255)] // Example max length, adjust as needed
        public string Url { get; set; }

        public DateTime RequestedAt { get; set; }

        public DateTime Expiry { get; set; } // Expiration time
    }
}

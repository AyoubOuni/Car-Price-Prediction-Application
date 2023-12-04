using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore; // Ajoutez cette ligne pour inclure l'espace de noms

namespace Projet.Models




{

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        [MaxLength(50, ErrorMessage = "Firstname cannot exceed 50 characters")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        [MaxLength(50, ErrorMessage = "Lastname cannot exceed 50 characters")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(20, ErrorMessage = "Password cannot exceed 20 characters")]
        public string Password { get; set; }
    }
}

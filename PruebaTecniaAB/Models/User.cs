using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecniaAB.Models
{
    public partial class User
    {
        public int IdUser { get; set; }

        [Required]
        public string Role { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Mail { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}

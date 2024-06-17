using System.ComponentModel.DataAnnotations;

namespace PruebaTecniaAB.Models.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? Clave { get; set; }

    }
}

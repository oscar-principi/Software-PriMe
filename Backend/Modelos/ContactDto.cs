using System.ComponentModel.DataAnnotations;

namespace Backend.Modelos
{
    public class ContactDto
    {
        [Required]
        public string name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string email { get; set; } = string.Empty;

        [Required]
        public string motivo { get; set; } = string.Empty;

        [Required]
        public string mensaje { get; set; } = string.Empty;
    }
}

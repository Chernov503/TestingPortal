using System.ComponentModel.DataAnnotations;

namespace Front.Models
{
    public class RegisterModel
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Company { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Пароль должен быть не менее 8 символов.")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string Password2 { get; set; }
    }
}

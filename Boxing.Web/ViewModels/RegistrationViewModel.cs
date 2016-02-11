using System.ComponentModel.DataAnnotations;

namespace Boxing.Web.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string RepeatedPassword { get; set; }
    }
}
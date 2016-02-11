using System.ComponentModel.DataAnnotations;

namespace Boxing.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
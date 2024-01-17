using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog_Project.Models
{
    public class VMUser
    {

        [Required(ErrorMessage = "Please prodive a Username")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Username needs to be 2-10 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please prodive a Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Password needs to be 10-100 characters.")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }
    }
}

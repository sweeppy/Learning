using System.ComponentModel.DataAnnotations;

namespace Notebook.Authorization
{
    public class UserRegistration
    {
        [Required(ErrorMessage = "Write your user name"), MaxLength(20)]
        public string LoginProp { get; set; }
        [Required(ErrorMessage = "Write your passwordd"), DataType(DataType.Password), MaxLength(20)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm the password"), DataType(DataType.Password), MaxLength (20),
            Compare(nameof(Password), ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }
    }
}

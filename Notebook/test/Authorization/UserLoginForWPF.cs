using System.ComponentModel.DataAnnotations;

namespace Notebook.Authorization
{
    public class UserLoginForWPF
    {
        [Required(ErrorMessage = "Write your user name"), MaxLength(20)]
        public string LoginProp { get; set; }
        [Required(ErrorMessage = "Write your password"), DataType(DataType.Password), MaxLength(20)]
        public string Password { get; set; }
    }
}

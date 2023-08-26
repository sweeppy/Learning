using System.ComponentModel.DataAnnotations;

namespace NotebookWPFApi.Authorization
{
    public class UserLogin
    {
        [Required(ErrorMessage ="Write your user name"), MaxLength(20)]
        public string LoginProp { get; set;}
        [Required(ErrorMessage ="Write your password"), DataType(DataType.Password), MaxLength(20)]
        public string Password { get; set;}
    }
}

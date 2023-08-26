using System.ComponentModel.DataAnnotations;

namespace Notebook.Authorization
{
    public class UserLogin
    {
        [Required(ErrorMessage ="Write your user name"), MaxLength(20)]
        public string LoginProp { get; set;}
        [Required(ErrorMessage ="Write your password"), DataType(DataType.Password), MaxLength(20)]
        public string Password { get; set;}
        /// <summary>
        /// Возвращение этой ссылки, если пользователь не авторизован
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}

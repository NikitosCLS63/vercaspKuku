using System.ComponentModel.DataAnnotations;

namespace WebApplication1TEST.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Логин")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Не указан Пароль")]

        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}

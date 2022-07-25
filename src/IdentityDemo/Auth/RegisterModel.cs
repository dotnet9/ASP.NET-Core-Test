using System.ComponentModel.DataAnnotations;

namespace IdentityDemo.Auth
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "用户名不能为空")] public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "邮件不能为空")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "密码不能为空")] public string? Password { get; set; }
    }
}
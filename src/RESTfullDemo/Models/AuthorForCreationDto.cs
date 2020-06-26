using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfullDemo.Models
{
    public class AuthorForCreationDto
    {
        [Required(ErrorMessage = "必须提供姓名")]
        [MaxLength(20, ErrorMessage = "姓名的最大长度为20个字符")]
        public string Name { get; set; }
        [Range(20, 100, ErrorMessage = "年龄必须在20到100岁之间")]
        public int Age { get; set; }
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }
        [Required(ErrorMessage = "必须提供住址")]
        [MaxLength(20, ErrorMessage = "住址的最大长度为20个字符")]
        public string BirthPlace { get; set; }
    }
}

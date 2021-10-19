using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Model
{
    public class UserInfo:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Memo { get; set; }
        [Required]
        public string Account { get; set; }
        [Required]
        public string Password { get; set; }

        public UserInfo(string name, string memo, string account, string password)
        {
            Name = name;
            Memo = memo;
            Account = account;
            Password = password;
        }
    }
}


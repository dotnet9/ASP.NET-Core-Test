using System.ComponentModel.DataAnnotations;

namespace RESTfullDemo.Models
{
    public class BookForCreationDto
    {
        [Required(ErrorMessage = "必须提供书籍标题")]
        [MaxLength(100, ErrorMessage = "书籍的最大长度为100个字符")]
        public string Title { get; set; }
        [MaxLength(500, ErrorMessage = "书籍描述的最大长度为500个字符")]
        public string Description { get; set; }
        public int Pages { get; set; }
    }
}

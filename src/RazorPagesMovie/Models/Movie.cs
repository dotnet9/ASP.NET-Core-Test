namespace RazorPagesMovie.Models;

public class Movie
{
    [DisplayName("ID")] public int ID { get; set; }

    [DisplayName("电影名")]
    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string Title { get; set; } = string.Empty;

    [DisplayName("发行时间")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [DisplayName("类型")] 
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    [Required]
    [StringLength(30)]
    public string Genre { get; set; } = string.Empty;

    [DisplayName("票价")]
    [Range(1, 100)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [DisplayName("评级")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
    [StringLength(5)]
    [Required]
    public string Rating { get; set; } = string.Empty;
}
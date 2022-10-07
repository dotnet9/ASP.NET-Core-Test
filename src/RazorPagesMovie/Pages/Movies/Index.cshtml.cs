namespace RazorPagesMovie.Pages.Movies;

public class IndexModel : PageModel
{
    private readonly RazorPagesMovieContext _context;

    public IndexModel(RazorPagesMovieContext context)
    {
        _context = context;
    }

    public IList<Movie> Movie { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public string? Keywords { get; set; }

    public SelectList? Genres { get; set; }
    [BindProperty(SupportsGet = true)] public string? MovieGenre { get; set; }


    public async Task OnGetAsync()
    {
        var genreQuery = from m in _context.Movie
            orderby m.Genre
            select m.Genre;
        var movies = from m in _context.Movie
            select m;

        if (!string.IsNullOrWhiteSpace(Keywords))
        {
            movies = movies.Where(s => s.Title.Contains(Keywords));
        }

        if (!string.IsNullOrWhiteSpace(MovieGenre))
        {
            movies = movies.Where(x => x.Genre == MovieGenre);
        }

        Genres = new SelectList(await genreQuery.Distinct().ToListAsync());

        Movie = await movies.ToListAsync();
    }
}
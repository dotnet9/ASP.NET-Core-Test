namespace RazorPagesMovie.Pages.Movies;

public class DetailsModel : PageModel
{
    private readonly RazorPagesMovieContext _context;

    public DetailsModel(RazorPagesMovieContext context)
    {
        _context = context;
    }

    public Movie? Movie { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Movie == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FirstOrDefaultAsync(m => m.ID == id);
        if (movie == null)
        {
            return NotFound();
        }
        else
        {
            Movie = movie;
        }

        return Page();
    }
}
namespace RazorPagesMovie.Pages.Movies;

public class DeleteModel : PageModel
{
    private readonly RazorPagesMovieContext _context;

    public DeleteModel(RazorPagesMovieContext context)
    {
        _context = context;
    }

    [BindProperty] public Movie? Movie { get; set; }

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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null || _context.Movie == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);

        if (movie == null) return RedirectToPage("./Index");
        Movie = movie;
        _context.Movie.Remove(Movie);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
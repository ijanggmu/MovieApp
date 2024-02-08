using System.ComponentModel.DataAnnotations;

namespace MovieApp.Dtos.Movie;

public class MovieListRequestModel
{
    [Required]

    public string Title { get; set; }
    public string? Description { get; set; }
    [Required]
    public string MovieYear { get; set; }
    [Required]
    public IFormFile Banner { get; set; }
}
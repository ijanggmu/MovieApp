using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Dtos.Movie;
using MovieApp.Models;
using System.Diagnostics;

namespace MovieApp.Controllers;

public class MovieController : Controller
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public MovieController(ApplicationDbContext dbContext,
                            IWebHostEnvironment webHostEnvironment)
    {
        _dbContext = dbContext;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var movieList = await _dbContext.Movies.Select(x => new MovieListResponseModel()
            {
                Id = x.Id,
                Title = x.Title,
                BannerUrl = x.BannerUrl,
                Description = x.Description,
                MovieYear = x.MovieYear
            }).ToListAsync();

            return View(movieList);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Error", "Movie");
        }
    }
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var movieDetails = await _dbContext.Movies
                .Where(m => m.Id == id)
                .Select(x => new MovieDetailResponseModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    BannerUrl = x.BannerUrl,
                    Description = x.Description,
                    MovieYear = x.MovieYear
                })
                .FirstOrDefaultAsync();

            if (movieDetails == null)
            {
                TempData["ErrorMessage"] = "Movie not found.";
                return RedirectToAction("Error", "Movie");
            }

            return View(movieDetails);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Error", "Movie");
        }
    }
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MovieListRequestModel requestModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var movieToAdd = new Movie();
                if (requestModel.Banner != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "moviePhoto");

                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = $"{requestModel.Title}-{Guid.NewGuid()}-{requestModel.Banner.FileName}";
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await requestModel.Banner.CopyToAsync(fileStream);
                    }

                    movieToAdd.BannerUrl = uniqueFileName;
                }
                movieToAdd.Title = requestModel.Title;
                movieToAdd.Description = requestModel.Description;
                movieToAdd.MovieYear = requestModel.MovieYear;

                await _dbContext.Movies.AddAsync(movieToAdd);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(requestModel);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Error", "Movie");
        }
    }
    
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        try
        {
            var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == Id);
            if(movie == null)
            {
                TempData["ErrorMessage"] = "Movie not found.";
                return RedirectToAction("Error", "Movie");
            }
             _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Something went wrong while deleting.";
            return RedirectToAction("Error", "Movie");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var errorMessage = TempData["ErrorMessage"] as string;
        ViewData["ErrorMessage"] = errorMessage;
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}
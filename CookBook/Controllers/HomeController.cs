using CookBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics;

namespace CookBook.Controllers
{
    [RequireLoggedIn]
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        private readonly CookBookDbContext _context;

        private readonly ILogger<HomeController> _logger;

        private IRecipeRepository _recipeRepository;

        public HomeController(ILogger<HomeController> logger, IRecipeRepository repo, CookBookDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _recipeRepository = repo;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var recipe = await _recipeRepository.GetAllRecipesAsync();
            return View(recipe);
        }

        public IActionResult PostRecipe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostRecipe(Recipe recipe, List<IFormFile> mediaFiles)
        {
           
            if (ModelState.IsValid)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                int userId = int.Parse(HttpContext.Session.GetString("UserId"));
#pragma warning restore CS8604 // Possible null reference argument.
                recipe.UserId = userId;
                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();

                // Save the uploaded files to the server and database
                foreach (var file in mediaFiles)
                {
                    // Determine if the file is an image or video
                    string mediaType = GetMediaType(file);

                    if(mediaType == "image" || mediaType == "video")
                    {
                        // Save the file to the server
                        string filePath = SaveFileToServer(file, mediaType);

                        // Save file details to the database
                        Medium media = new Medium
                        {
                            RecipeId = recipe.RecipeId,
                            MediaType = mediaType,
                            MediaUrl = filePath
                        };

                        _context.Media.Add(media);

                    }
                    
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(recipe);
        }

        private string GetMediaType(IFormFile file)
        {
            // Determine if the file is an image or video based on its content type
            if (file.ContentType.Contains("image"))
            {
                return "image";
            }
            else if (file.ContentType.Contains("video"))
            {
                return "video";
            }
            else
            {
                return "unknown";
            }
        }

        private string SaveFileToServer(IFormFile file, string mediaType)
        {
            // Save the file to the server under the wwwroot/img folder
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return "~/img/" + uniqueFileName;
        }

        public async Task<IActionResult> ViewRecipe(int recipeId)
        {
            // Retrieve the post data based on the postId
            var recipe = await _recipeRepository.GetRecipeWithDetailsAsync(recipeId);

            return View(recipe);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Login", "Account"); // Redirect to the login page
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

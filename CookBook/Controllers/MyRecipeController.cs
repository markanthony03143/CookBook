using CookBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics;

namespace CookBook.Controllers
{
    [RequireLoggedIn]
    public class MyRecipeController : Controller
    {
        private readonly ILogger<MyRecipeController> _logger;

        private IRecipeRepository _recipeRepository;

        public MyRecipeController(ILogger<MyRecipeController> logger, IRecipeRepository repo)
        {
            _logger = logger;
            _recipeRepository = repo;
        }

        public async Task<IActionResult> MyRecipe()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
#pragma warning restore CS8604 // Possible null reference argument.

            var userRecipes = await _recipeRepository.GetAllUserRecipesAsync(userId);

            return View(userRecipes);
        }

        public async Task<IActionResult> ViewRecipe(int recipeId)
        {
            // Retrieve the post data based on the postId
            var recipe = await _recipeRepository.GetRecipeWithDetailsAsync(recipeId);

            return View(recipe);
        }
    }
}

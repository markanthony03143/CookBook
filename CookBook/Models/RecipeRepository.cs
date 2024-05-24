using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CookBook.Models;

namespace CookBook.Models
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly CookBookDbContext _context;

        public RecipeRepository(CookBookDbContext context)
        {
            _context = context;
        }

        public async Task<Recipe> GetRecipeWithDetailsAsync(int recipeId)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Media)
                .Include(r => r.Likes)
                .FirstOrDefaultAsync(r => r.RecipeId == recipeId);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            return await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Media)
                .Include(r => r.Likes)
                .OrderByDescending(r => r.RecipeId)
                .ToListAsync();
        }

        public async Task<List<Recipe>> GetAllUserRecipesAsync(int userId)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Media)
                .Include(r => r.Likes)
                .Where(r => r.User.UserId == userId)
                .OrderByDescending(r => r.RecipeId)
                .ToListAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}

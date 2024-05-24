using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook.Models;

namespace CookBook.Models
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetRecipeWithDetailsAsync(int recipeId);
        Task<List<Recipe>> GetAllRecipesAsync();
        Task<List<Recipe>> GetAllUserRecipesAsync(int userId);
    }
}

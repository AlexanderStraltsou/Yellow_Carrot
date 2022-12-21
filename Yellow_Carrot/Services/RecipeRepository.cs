using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yellow_Carrot.Data;
using Yellow_Carrot.Models;

namespace Yellow_Carrot.Services
{
    internal class RecipeRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all recipes from the list
        public List<Recipe> GetRecipes()
        {
            return _context.Recipes.ToList();
        }

        // Get a recipe by id
        public Recipe? GetRecipes(int id)
        {
            return _context.Recipes.FirstOrDefault(c => c.RecipeId == id);
        }

        // Create a new recipe
        public void AddRecipe(Recipe recipeToAdd)
        {
            _context.Recipes.Add(recipeToAdd);
        }

        //Update existing recipe

        public void UpdateRecipe(Recipe recipeToUpdate)
        {
            _context.Recipes.Update(recipeToUpdate);
        }


        // Remove recipe
        public void RemoveRecipe(Recipe recipeToRemove)
        {
            _context.Recipes.Remove(recipeToRemove);
        }
    }
}

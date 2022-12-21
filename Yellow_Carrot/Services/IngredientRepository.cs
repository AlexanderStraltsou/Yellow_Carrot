using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yellow_Carrot.Data;
using Yellow_Carrot.Models;

namespace Yellow_Carrot.Services
{
    internal class IngredientRepository
    {
        private readonly AppDbContext _context;

        public IngredientRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all ingredients from the list
        public List<Ingredient> GetIngredients()
        {
            return _context.Ingredients.ToList();
        }

        public List<Ingredient> GetIngredientByName(string newIngredientName)
        {
            return _context.Ingredients.Where(x => x.Name == newIngredientName).ToList();
        }

        // Create new ingredient
        public void AddIngredient(Ingredient ingredientToAdd)
        {
            _context.Ingredients.Add(ingredientToAdd);
        }

        // Remove Ingredient
        public void RemoveIngredient(Ingredient ingredientToRemove)
        {
            _context.Ingredients.Remove(ingredientToRemove);
        }

    }
}

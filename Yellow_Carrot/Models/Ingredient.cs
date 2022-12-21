using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yellow_Carrot.Models
{
    internal class Ingredient
    {
        [Key]
        public string Name { get; set; } = null!;
        public int IngredientId { get; set; }
        public string Quantity { get; set; } = null!;
        
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yellow_Carrot.Models
{
    internal class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        public string Name { get; set; } = null!;
        public List<Ingredient> Ingredients { get; set; } = new();
        public int? TagId { get; set; }
        public List<Tags> Tags { get; set; } = new();



    }
}

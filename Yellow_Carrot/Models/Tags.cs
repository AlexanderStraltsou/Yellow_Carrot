using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yellow_Carrot.Models
{
    internal class Tags
    {
        [Key]
        public int? TagId { get; set; }
        public string TagName { get; set; } = null!;

        //public Recipe Recipe { get; set; } = null!;
        public List<Recipe> Recipes { get; set; } = new();


    }
}

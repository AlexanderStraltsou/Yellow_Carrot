using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yellow_Carrot.Models;

namespace Yellow_Carrot.Data
{
    internal class AppDbContext : DbContext 
    {

        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YellowCarrotDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasMany(u => u.Ingredients).WithOne(r => r.Recipe).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 1,
                Name = "Pasta Rigatoni",
                Quantity = "200 gr",
                RecipeId = 1
            }, new Ingredient()
            {
                IngredientId = 2,
                Name = "Pesto",
                Quantity = "70 gr",
                RecipeId = 1
            }, new Ingredient()
            {
                IngredientId = 3,
                Name = "Parmegano Cheese",
                Quantity = "100 gr",
                RecipeId = 1
            }, new Ingredient()
            {
                IngredientId = 4,
                Name = "Pizza Dough",
                Quantity = "1 kg",
                RecipeId = 2
            }, new Ingredient()
            {
                IngredientId = 5,
                Name = "Mozarella Cheese",
                Quantity = "100",
                RecipeId = 2
            }, new Ingredient()
            {
                IngredientId = 6,
                Name = "Red Paprika",
                Quantity = "50 gr",
                RecipeId = 2

            }, new Ingredient()
            {
                IngredientId = 7,
                Name = "Salami",
                Quantity = "100 gr",
                RecipeId = 2
            },

            new Ingredient()
            {
                IngredientId = 8,
                Name = "Tomato Sauce",
                Quantity = "200 gr",
                RecipeId = 2

            },

            new Ingredient()
            {
                IngredientId = 9,
                Name = "Curry",
                Quantity = "10 gr",
                RecipeId = 3

            },

            new Ingredient()
            {
                IngredientId = 10,
                Name = "Parma",
                Quantity = "50 gr",
                RecipeId = 3

            },

            new Ingredient()
            {
                IngredientId = 11,
                Name = "Pizza Bottom",
                Quantity = "2 pcs",
                RecipeId = 3

            },

            new Ingredient()
            {
                IngredientId = 12,
                Name = "Ananas",
                Quantity = "10 slices",
                RecipeId = 3

            }




            );

            modelBuilder.Entity<Tags>().HasData(new Tags()
            {
                TagId = 1,
                TagName = "Vegetarian"
            }, new Tags()
            {
                TagId = 2,
                TagName = "Vegan"
            }, new Tags()
            {
                TagId = 3,
                TagName = "Dairy"
            }, new Tags()
            {
                TagId = 4,
                TagName = "Sugar Free"
            }, new Tags()
            {
                TagId = 5,
                TagName = "Gluten Free"

            }, new Tags()
            {
                TagId = 6,
                TagName = "Lactose Free"
            });


            modelBuilder.Entity<Recipe>().HasData(new Recipe()
            {
                RecipeId = 1,
                Name = "Pasta Pesto",

            }, 

            new Recipe()
            {
                RecipeId = 2,
                Name = "Pizza Salami",
                TagId = 5
            },

            new Recipe()
            {
                RecipeId = 3,
                Name = "Pizza Hawaii",
                TagId = 6
            }


            );
        }
    }
}


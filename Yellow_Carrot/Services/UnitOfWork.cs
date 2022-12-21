using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yellow_Carrot.Data;

namespace Yellow_Carrot.Services
{
    internal class UnitOfWork
    {
        private readonly AppDbContext _context;
        private IngredientRepository? _ingredientRepo;
        private RecipeRepository? _recipeRepo;
        private TagsRepository? _tagsRepo;


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IngredientRepository IngredientRepo
        {
            get
            {
                if (_ingredientRepo == null)
                {
                    _ingredientRepo = new IngredientRepository(_context);
                }

                return _ingredientRepo;
            }
        }

        public RecipeRepository RecipeRepo
        {
            get
            {
                if (_recipeRepo == null)
                {
                    _recipeRepo = new RecipeRepository(_context);
                }

                return _recipeRepo;
            }
        }

        public TagsRepository TagsRepo
        {
            get
            {
                if (_tagsRepo == null)
                {
                    _tagsRepo = new TagsRepository(_context);
                }

                return _tagsRepo;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }

}

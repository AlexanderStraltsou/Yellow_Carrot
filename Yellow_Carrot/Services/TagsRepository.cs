using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yellow_Carrot.Data;
using Yellow_Carrot.Models;

namespace Yellow_Carrot.Services
{
    internal class TagsRepository
    {
        private readonly AppDbContext _context;

        public TagsRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all tags
        public List<Tags> GetTags()
        {
            return _context.Tags.ToList();
        }


        // Create a new tag
        public void AddTags(Tags tagsToAdd)
        {
            _context.Tags.Add(tagsToAdd);
        }

      
        // Update tags
        public void UpdateTags(Tags updatedTags)
        {
            _context.Tags.Update(updatedTags);
        }

        // Remove a tag
        public void RemoveTags(Tags tagsToRemove)
        {
            _context.Tags.Remove(tagsToRemove);
        }
    }
}

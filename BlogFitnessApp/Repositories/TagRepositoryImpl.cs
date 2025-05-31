using BlogFitnessApp.Data;
using BlogFitnessApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogFitnessApp.Repositories
{
    public class TagRepositoryImpl : ITagRepository
    {


        private readonly BLogFitnessDbContext _bLogFitnessDbContext;
        public TagRepositoryImpl(BLogFitnessDbContext bLogFitnessDbContext)
        {
            _bLogFitnessDbContext = bLogFitnessDbContext;
        }


        public async Task<Tag?> AddAsync(Tag tag)
        {
            await _bLogFitnessDbContext.AddAsync(tag);
            await _bLogFitnessDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            
            var existingTag = await _bLogFitnessDbContext.Tags.FindAsync(id);
            if (existingTag != null)
            {
                _bLogFitnessDbContext.Tags.Remove(existingTag);
                await _bLogFitnessDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
           return  await _bLogFitnessDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetByIdAsync(Guid id)   
        {
            return _bLogFitnessDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await _bLogFitnessDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await _bLogFitnessDbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }
    }
}

﻿using BlogFitnessApp.Models.Domain;

namespace BlogFitnessApp.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<BlogPost?> AddAsync(BlogPost blogPost);
        Task<BlogPost?> UpdateAsync(BlogPost blogPost);   
        Task<BlogPost?> DeleteAsync(Guid id);   

    }
}

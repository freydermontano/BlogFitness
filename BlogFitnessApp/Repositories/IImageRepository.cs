﻿namespace BlogFitnessApp.Repositories
{
    public interface IImageRepository
    {
        Task<string> UploadImageAsync(IFormFile file);
         
    }
}

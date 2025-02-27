using App.Domain.Core.Home.DTO;
using App.Domain.Core.Home.Entities.Categories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.AppServices.Categories
{
    public interface ICategoryAppService
    {
        Task<bool> AddCategoryAsync(Category category, CancellationToken cancellationToken);
        Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken);
        Task<Category> GetCategoryByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateCategoryAsync(Category category, CancellationToken cancellationToken);
        Task<bool> DeleteCategoryAsync(int id, CancellationToken cancellationToken);
        Task<string> UploadImage(IFormFile FormFile, string folderName, CancellationToken cancellation);
        Task EditCategoryAsync(Category category, CategoryDTO model, IFormFile imageFile, CancellationToken cancellationToken);
    }
}

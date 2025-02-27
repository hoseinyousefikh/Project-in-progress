using App.Domain.Core.Home.Contract.AppServices.Categories;
using App.Domain.Core.Home.Contract.Services.Categories;
using App.Domain.Core.Home.DTO;
using App.Domain.Core.Home.Entities.Categories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Home.AppServices.Categories
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryService _categoryService;

        public CategoryAppService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public Task<bool> AddCategoryAsync(Category category, CancellationToken cancellationToken)
        {
            return _categoryService.AddCategoryAsync(category, cancellationToken);
        }

        public Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            return _categoryService.GetAllCategoriesAsync(cancellationToken);
        }

        public Task<Category> GetCategoryByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _categoryService.GetCategoryByIdAsync(id, cancellationToken);
        }

        public Task<bool> UpdateCategoryAsync(Category category, CancellationToken cancellationToken)
        {
            return _categoryService.UpdateCategoryAsync(category, cancellationToken);
        }

        public Task<bool> DeleteCategoryAsync(int id, CancellationToken cancellationToken)
        {
            return _categoryService.DeleteCategoryAsync(id, cancellationToken);
        }
        public async Task<string> UploadImage(IFormFile formFile, string folderName, CancellationToken cancellation)
        {
            if (formFile == null || formFile.Length == 0)
                return string.Empty;

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, fileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream, cancellation);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Upload files operation failed: " + ex.Message);
            }

            return $"/images/{folderName}/{fileName}";
        }
        public async Task EditCategoryAsync(Category category, CategoryDTO model, IFormFile imageFile, CancellationToken cancellationToken)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                string uniqueFileName = await UploadImage(imageFile, "categories", cancellationToken);

                if (!string.IsNullOrEmpty(category.ImageUrl))
                {
                    string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", category.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                category.ImageUrl = uniqueFileName;
            }

            category.Name = model.Name;

            await _categoryService.UpdateCategoryAsync(category, cancellationToken);
        }
    }
}

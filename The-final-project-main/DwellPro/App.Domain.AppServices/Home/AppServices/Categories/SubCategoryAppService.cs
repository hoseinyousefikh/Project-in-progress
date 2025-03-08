using App.Domain.Core.Home.Contract.AppServices.Categories;
using App.Domain.Core.Home.Contract.Services.Categories;
using App.Domain.Core.Home.DTO;
using App.Domain.Core.Home.Entities.Categories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Home.AppServices.Categories
{
    public class SubCategoryAppService : ISubCategoryAppService
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryAppService(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        public Task<bool> AddSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken)
        {
            return _subCategoryService.AddSubCategoryAsync(subCategory, cancellationToken);
        }

        public Task<List<SubCategory>> GetAllSubCategoriesAsync(CancellationToken cancellationToken)
        {
            return _subCategoryService.GetAllSubCategoriesAsync(cancellationToken);
        }

        public Task<SubCategory> GetSubCategoryByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _subCategoryService.GetSubCategoryByIdAsync(id, cancellationToken);
        }

        public Task<bool> UpdateSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken)
        {
            return _subCategoryService.UpdateSubCategoryAsync(subCategory, cancellationToken);
        }

        public Task<bool> DeleteSubCategoryAsync(int id, CancellationToken cancellationToken)
        {
            return _subCategoryService.DeleteSubCategoryAsync(id, cancellationToken);
        }
        public async Task<string> AddImageSubCategory(IFormFile imageFile, string folderName, CancellationToken cancellationToken)
        {
            if (imageFile == null || imageFile.Length == 0)
                return string.Empty;

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, fileName);

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Upload files operation failed: " + ex.Message);
            }

            return $"/images/{folderName}/{fileName}";
        }
        public async Task<string> EditSubCategoryImage(IFormFile imageFile, string existingImageUrl, CancellationToken cancellationToken)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                string uniqueFileName = await AddImageSubCategory(imageFile, "subcategories", cancellationToken);

                if (!string.IsNullOrEmpty(existingImageUrl))
                {
                    string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                return uniqueFileName;
            }

            return existingImageUrl;
        }
        public async Task<List<SubCategoryDto>> GetFilteredSubCategoriesAsync(int categoryId, CancellationToken cancellationToken)
        {
            var subCategories = await GetAllSubCategoriesAsync(cancellationToken);

            return subCategories
                .Where(sc => sc.CategoryId == categoryId && !sc.IsDeleted)
                .Select(sc => new SubCategoryDto
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    ImageUrl = sc.ImageUrl,
                    CategoryId = sc.CategoryId
                })
                .ToList();
        }


    }
}

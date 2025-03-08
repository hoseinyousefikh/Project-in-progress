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
    public interface ISubCategoryAppService
    {
        Task<bool> AddSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken);
        Task<List<SubCategory>> GetAllSubCategoriesAsync(CancellationToken cancellationToken);
        Task<SubCategory> GetSubCategoryByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken);
        Task<bool> DeleteSubCategoryAsync(int id, CancellationToken cancellationToken);
        Task<string> AddImageSubCategory(IFormFile imageFile, string folderName, CancellationToken cancellationToken);
        Task<string> EditSubCategoryImage(IFormFile imageFile, string existingImageUrl, CancellationToken cancellationToken);
        Task<List<SubCategoryDto>> GetFilteredSubCategoriesAsync(int categoryId, CancellationToken cancellationToken);
    }
}

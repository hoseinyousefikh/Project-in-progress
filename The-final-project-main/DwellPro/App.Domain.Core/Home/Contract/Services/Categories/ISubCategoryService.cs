using App.Domain.Core.Home.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Services.Categories
{
    public interface ISubCategoryService
    {
        Task<bool> AddSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken);
        Task<List<SubCategory>> GetAllSubCategoriesAsync(CancellationToken cancellationToken);
        Task<SubCategory> GetSubCategoryByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken);
        Task<bool> DeleteSubCategoryAsync(int id, CancellationToken cancellationToken);
    }
}

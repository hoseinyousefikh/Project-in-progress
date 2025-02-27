using App.Domain.Core.Home.Contract.Repositories.Categories;
using App.Domain.Core.Home.Contract.Services.Categories;
using App.Domain.Core.Home.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Home.Services.Categories
{
    public class SubCategoryService: ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;

        public SubCategoryService(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task<bool> AddSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken)
        {
            if (subCategory == null)
                throw new ArgumentNullException(nameof(subCategory));

            return await _subCategoryRepository.AddAsync(subCategory, cancellationToken);
        }

        public async Task<List<SubCategory>> GetAllSubCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _subCategoryRepository.GetAllAsync(cancellationToken);
        }

        public async Task<SubCategory> GetSubCategoryByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _subCategoryRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken)
        {
            if (subCategory == null)
                throw new ArgumentNullException(nameof(subCategory));

            return await _subCategoryRepository.UpdateAsync(subCategory, cancellationToken);
        }


        public async Task<bool> DeleteSubCategoryAsync(int id, CancellationToken cancellationToken)
        {
            return await _subCategoryRepository.DeleteAsync(id, cancellationToken);
        }
    }
}

using App.Domain.Core.Home.Contract.Repositories.Categories;
using App.Domain.Core.Home.Contract.Services.Categories;
using App.Domain.Core.Home.Entities.Categories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Home.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> AddCategoryAsync(Category category, CancellationToken cancellationToken)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            return await _categoryRepository.AddAsync(category, cancellationToken);
        }

        public async Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Category> GetCategoryByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateCategoryAsync(Category category, CancellationToken cancellationToken)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            return await _categoryRepository.UpdateAsync(category, cancellationToken);
        }

        public async Task<bool> DeleteCategoryAsync(int id, CancellationToken cancellationToken)
        {
            return await _categoryRepository.DeleteAsync(id, cancellationToken);
        }
      
    }
}

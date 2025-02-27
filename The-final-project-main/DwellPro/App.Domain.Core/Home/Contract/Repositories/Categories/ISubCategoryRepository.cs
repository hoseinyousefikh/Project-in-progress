using App.Domain.Core.Home.Entities.Categories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Repositories.Categories
{
    public interface ISubCategoryRepository
    {
        Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken);
        Task<SubCategory> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddAsync(SubCategory subCategory, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(SubCategory subCategory, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}

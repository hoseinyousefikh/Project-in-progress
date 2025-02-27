using App.Domain.Core.Home.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Repositories.Categories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
        Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken); //*
        Task<bool> AddAsync(Category category, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}

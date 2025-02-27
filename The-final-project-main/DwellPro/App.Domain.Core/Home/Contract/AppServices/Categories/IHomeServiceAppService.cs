using App.Domain.Core.Home.Entities.Categories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.AppServices.Categories
{
    public interface IHomeServiceAppService
    {
        Task<bool> AddHomeServiceAsync(HomeService homeService, CancellationToken cancellationToken);
        Task<List<HomeService>> GetAllHomeServicesAsync(CancellationToken cancellationToken);
        Task<HomeService> GetHomeServiceByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateHomeServiceAsync(HomeService homeService, CancellationToken cancellationToken);
        Task<bool> DeleteHomeServiceAsync(int id, CancellationToken cancellationToken);
        Task<string> AddImageToFileSystem(IFormFile imageFile, string folderName, CancellationToken cancellationToken);
        Task<string> EditHomeServiceImage(IFormFile imageFile, string existingImageUrl, CancellationToken cancellationToken);
    }
}

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
    public class HomeServiceAppService : IHomeServiceAppService
    {
        private readonly IHomeServiceService _homeServiceService;

        public HomeServiceAppService(IHomeServiceService homeServiceService)
        {
            _homeServiceService = homeServiceService;
        }

        public Task<bool> AddHomeServiceAsync(HomeService homeService, CancellationToken cancellationToken)
        {
            return _homeServiceService.AddHomeServiceAsync(homeService, cancellationToken);
        }

        public Task<List<HomeService>> GetAllHomeServicesAsync(CancellationToken cancellationToken)
        {
            return _homeServiceService.GetAllHomeServicesAsync(cancellationToken);
        }

        public Task<HomeService> GetHomeServiceByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _homeServiceService.GetHomeServiceByIdAsync(id, cancellationToken);
        }

        public Task<bool> UpdateHomeServiceAsync(HomeService homeService, CancellationToken cancellationToken)
        {
            return _homeServiceService.UpdateHomeServiceAsync(homeService, cancellationToken);
        }

        public Task<bool> DeleteHomeServiceAsync(int id, CancellationToken cancellationToken)
        {
            return _homeServiceService.DeleteHomeServiceAsync(id, cancellationToken);
        }
        public async Task<string> AddImageToFileSystem(IFormFile imageFile, string folderName, CancellationToken cancellationToken)
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
        public async Task<string> EditHomeServiceImage(IFormFile imageFile, string existingImageUrl, CancellationToken cancellationToken)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                string uniqueFileName = await AddImageToFileSystem(imageFile, "homeservices", cancellationToken);

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
        public async Task<List<HomeServiceDto>> GetFilteredHomeServicesAsync(int subCategoryId, CancellationToken cancellationToken)
        {
            var allHomeServices = await GetAllHomeServicesAsync(cancellationToken);

            return allHomeServices
                .Where(hs => hs.SubCategoryId == subCategoryId && !hs.IsDeleted)
                .Select(hs => new HomeServiceDto
                {
                    Id = hs.Id,
                    Name = hs.Name,
                    ImageUrl = hs.ImageUrl,
                    Description = hs.Description,
                    BasePrice = hs.BasePrice,
                    ViewCount = hs.ViewCount,
                    SubCategoryId = hs.SubCategoryId
                })
                .ToList();
        }


    }
}

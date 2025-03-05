using App.Domain.Core.Home.Contract.AppServices.Other;
using App.Domain.Core.Home.Contract.Services.Other;
using App.Domain.Core.Home.Entities.Other;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Home.AppServices.Other
{
    public class PictureAppService : IPictureAppService
    {
        private readonly IPictureService _pictureService;
        public PictureAppService(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }
        public Task<bool> AddPictureAsync(Pictures picture, CancellationToken cancellationToken)
        {
            return _pictureService.AddPictureAsync(picture , cancellationToken);
        }
        public async Task<string> SaveImageAsync(IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return null;

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); 
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName); 

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            return "/uploads/" + fileName; 
        }

    }
}

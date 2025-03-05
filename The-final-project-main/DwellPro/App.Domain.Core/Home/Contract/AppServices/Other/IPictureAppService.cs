using App.Domain.Core.Home.Entities.Other;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.AppServices.Other
{
    public interface IPictureAppService
    {
        Task<bool> AddPictureAsync(Pictures picture, CancellationToken cancellationToken);
        Task<string> SaveImageAsync(IFormFile file, CancellationToken cancellationToken);
    }
}

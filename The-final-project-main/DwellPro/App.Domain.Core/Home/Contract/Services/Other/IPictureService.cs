using App.Domain.Core.Home.Entities.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Services.Other
{
    public interface IPictureService
    {
        Task<bool> AddPictureAsync(Pictures picture, CancellationToken cancellationToken);
    }

}

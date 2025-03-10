using App.Domain.Core.Home.Entities.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.AppServices.Other
{
    public interface IExpertHomeServiceAppService
    {
        Task<bool> AddExpertHomeServiceAsync(int userId, int homeServiceId, CancellationToken cancellationToken);
        Task<bool> RemoveExpertHomeServiceAsync(int id, CancellationToken cancellationToken);
    }
}

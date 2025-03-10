using App.Domain.Core.Home.Contract.AppServices.Other;
using App.Domain.Core.Home.Contract.Services.Other;
using App.Domain.Core.Home.Entities.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Home.AppServices.Other
{
    public class ExpertHomeServiceAppService : IExpertHomeServiceAppService
    {
        private readonly IExpertHomeServiceService _expertHomeServiceService;
        public ExpertHomeServiceAppService(IExpertHomeServiceService expertHomeServiceService)
        {
            _expertHomeServiceService = expertHomeServiceService;
        }
        public Task<bool> AddExpertHomeServiceAsync(int userId, int homeServiceId, CancellationToken cancellationToken)

        {
            return _expertHomeServiceService.AddExpertHomeServiceAsync(userId ,homeServiceId, cancellationToken);
        }

        public Task<bool> RemoveExpertHomeServiceAsync(int id, CancellationToken cancellationToken)
        {
            return _expertHomeServiceService.RemoveExpertHomeServiceAsync(id, cancellationToken);
        }
    }
}

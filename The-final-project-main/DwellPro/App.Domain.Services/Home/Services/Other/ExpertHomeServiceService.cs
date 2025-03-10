using App.Domain.Core.Home.Contract.Repositories.Other;
using App.Domain.Core.Home.Contract.Services.Other;
using App.Domain.Core.Home.Entities.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Home.Services.Other
{
    public class ExpertHomeServiceService : IExpertHomeServiceService
    {
        private readonly IExpertHomeServiceRepository _expertHomeServiceRepository;

        public ExpertHomeServiceService(IExpertHomeServiceRepository expertHomeServiceRepository)
        {
            _expertHomeServiceRepository = expertHomeServiceRepository;
        }

        public async Task<bool> AddExpertHomeServiceAsync(int userId, int homeServiceId, CancellationToken cancellationToken)
        {
            var expertHomeService = new ExpertHomeService
            {
                ExpertId = userId,
                HomeServiceId = homeServiceId,
                
            };
            return await _expertHomeServiceRepository.AddAsync(expertHomeService, cancellationToken);
        }

        public async Task<bool> RemoveExpertHomeServiceAsync(int id, CancellationToken cancellationToken)
        {
            return await _expertHomeServiceRepository.DeleteAsync(id, cancellationToken);
        }
    }
}

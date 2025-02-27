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
    public class AdminCommentAppService : IAdminCommentAppService
    {
        private readonly IAdminCommentService _adminCommentService;

        public AdminCommentAppService(IAdminCommentService adminCommentService)
        {
            _adminCommentService = adminCommentService;
        }

        public Task<List<Comments>> GetAllCommentsAsync(CancellationToken cancellationToken)
        {
            return _adminCommentService.GetAllCommentsAsync(cancellationToken);
        }

        public Task<bool> ApproveCommentAsync(int id, CancellationToken cancellationToken)
        {
            return _adminCommentService.ApproveCommentAsync(id, cancellationToken);
        }

        public Task<bool> RejectCommentAsync(int id, CancellationToken cancellationToken)
        {
            return _adminCommentService.RejectCommentAsync(id, cancellationToken);
        }

        public Task<bool> DeleteCommentAsync(int id, CancellationToken cancellationToken)
        {
            return _adminCommentService.DeleteCommentAsync(id, cancellationToken);
        }

        public Task<Comments> GetCommentDetailsAsync(int id, CancellationToken cancellationToken)
        {
            return _adminCommentService.GetCommentDetailsAsync(id, cancellationToken);
        }
    }
}

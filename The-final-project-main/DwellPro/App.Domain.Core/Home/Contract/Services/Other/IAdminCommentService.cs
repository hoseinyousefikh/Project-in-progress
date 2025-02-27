using App.Domain.Core.Home.Entities.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Home.Contract.Services.Other
{
    public interface IAdminCommentService
    {
        Task<List<Comments>> GetAllCommentsAsync(CancellationToken cancellationToken);
        Task<bool> ApproveCommentAsync(int id, CancellationToken cancellationToken);
        Task<bool> RejectCommentAsync(int id, CancellationToken cancellationToken);
        Task<bool> DeleteCommentAsync(int id, CancellationToken cancellationToken);
        Task<Comments> GetCommentDetailsAsync(int id, CancellationToken cancellationToken);
    }
}

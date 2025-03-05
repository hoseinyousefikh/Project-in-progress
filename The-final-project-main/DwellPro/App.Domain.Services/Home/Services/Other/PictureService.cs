using App.Domain.Core.Home.Contract.Repositories.Other;
using App.Domain.Core.Home.Contract.Services.Other;
using App.Domain.Core.Home.Entities.Other;
using App.Infra.Data.Repos.Ef.Home.Repository.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Home.Services.Other
{
  public class PictureService : IPictureService
{
    private readonly IPicturesRepository _pictureRepository;

    public PictureService(IPicturesRepository pictureRepository)
    {
        _pictureRepository = pictureRepository;
    }

    public async Task<bool> AddPictureAsync(Pictures picture, CancellationToken cancellationToken)
    {

        return await _pictureRepository.AddAsync(picture, cancellationToken);
    }
}

}

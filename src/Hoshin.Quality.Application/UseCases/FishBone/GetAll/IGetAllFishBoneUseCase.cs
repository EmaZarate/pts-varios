using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FishBone.GetAll
{
    public interface IGetAllFishBoneUseCase
    {
        List<FishBoneOutput> Execute();
    }
}

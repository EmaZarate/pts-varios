using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FishBone.GetAllActive
{
    public interface IGetAllActiveFishBoneUseCase
    {
        List<FishBoneOutput> Execute();
    }
}

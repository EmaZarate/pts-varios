using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FishBone.CreateFishBone
{
    public interface ICreateFishBoneUseCase
    {
        FishBoneOutput Execute(bool active,string color, string name);
    }
}

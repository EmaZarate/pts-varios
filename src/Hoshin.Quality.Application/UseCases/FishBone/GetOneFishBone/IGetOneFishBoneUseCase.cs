using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FishBone.GetOneFishBone
{
   public interface IGetOneFishBoneUseCase
    {
        FishBoneOutput Execute(int id);
    }
}

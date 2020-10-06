using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FishBone.UpdateFishBone
{
    public interface IUpdateFishBoneUseCase
    {
        FishBoneOutput Execute(int id, string name, string colour, bool active);
    }
}

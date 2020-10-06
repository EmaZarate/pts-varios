using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Quality.Domain.FishBone;
namespace Hoshin.Quality.Application.Repositories
{
   public interface  IFishBoneRepository
    {
        List<FishBone> GetAll();
        List<FishBone> GetAllActive();
        FishBone Get(int id);
        FishBone Get(string name, string color);
        FishBone Add(FishBone newFishBone);
        FishBone Update(FishBone updateFish);
        FishBone CheckExistsFishBone(string color, string name, int id);
        int GetCountActive();
    }
}

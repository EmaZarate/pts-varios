using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Microsoft.Extensions.DependencyInjection;
using Hoshin.Quality.Domain.FishBone;
using System.Linq;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
  public  class FishBoneRepository : Hoshin.Quality.Application.Repositories.IFishBoneRepository 
    {
        private readonly IServiceProvider _serviceProvider;
        public FishBoneRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public FishBone Add(FishBone newFishBone)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var fishBone = new Fishbone();
                fishBone.Active = newFishBone.Active;
                fishBone.Color = newFishBone.Color;
                fishBone.Name = newFishBone.Name;
                _ctx.Fishbone.Add(fishBone);
                _ctx.SaveChanges();
                newFishBone.FishboneID = newFishBone.FishboneID;
                return newFishBone;
            }

        }

        public FishBone CheckExistsFishBone(string color, string name, int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var param = _ctx.Fishbone.Where(x => (x.Name == name || x.Color == color) && x.FishboneID != id).FirstOrDefault();
                if (param != null)
                {
                    return new FishBone(param.Name, param.Color, param.Active,param.FishboneID);
                }
                return null;
            }
        }

        public FishBone Get(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {

                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var fishBone = _ctx.Fishbone.Where(x => x.FishboneID == id).FirstOrDefault();
                if (fishBone != null)
                {
                    return new FishBone(fishBone.Name, fishBone.Color, fishBone.Active, fishBone.FishboneID);
                }

            }
            return null;
        }

        public FishBone Get(string name, string color)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var param = _ctx.Fishbone.Where(x => x.Color == color || x.Name == name).FirstOrDefault();
                if (param != null)
                {
                    return new FishBone(param.Name, param.Color, param.Active, param.FishboneID);
                }
                return null;
            }
        }

        public List<FishBone> GetAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {

                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var res = _ctx.Fishbone.ToList();
                var list = new List<FishBone>();
               
                foreach (Fishbone fb in res)
                {
                    list.Add(new FishBone(fb.Name,fb.Color,fb.Active,fb.FishboneID));
                }
                return list;
            }

        }

        public List<FishBone> GetAllActive()
        {
            using (var scope = _serviceProvider.CreateScope())
            {

                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var res = _ctx.Fishbone.Where(x => x.Active == true ).ToList();
                var list = new List<FishBone>();

                foreach (Fishbone fb in res)
                {
                    list.Add(new FishBone(fb.Name, fb.Color, fb.Active, fb.FishboneID));
                }
                return list;
            }

        }
        public int GetCountActive()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var res = _ctx.Fishbone.Where(x => x.Active == true).Count();
                return res;
            }
        }

        public FishBone Update(FishBone updateFish)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var fishBone = _ctx.Fishbone.Where(x => x.FishboneID == updateFish.FishboneID).FirstOrDefault();
                fishBone.Name = updateFish.Name;
                fishBone.Color = updateFish.Color;
                fishBone.Active = updateFish.Active;
                _ctx.Update(fishBone);
                _ctx.SaveChanges();
                return updateFish;
            }
        }
    }
}

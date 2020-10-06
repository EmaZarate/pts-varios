using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.CorrectiveActionFishbone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class CorrectiveActionFishboneRepository : ICorrectiveActionFishboneRepository
    {
        private readonly SQLHoshinCoreContext _ctx;
        private readonly IMapper _mapper;

        public CorrectiveActionFishboneRepository(SQLHoshinCoreContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public void UpdateRange(List<CorrectiveActionFishbone> correctiveActionFishbones)
        {
            try
            {
                var mappedEntities = _mapper.Map<List<Entities.Quality.CorrectiveActionFishbone>>(correctiveActionFishbones);
                _ctx.CorrectiveActionFishbone.UpdateRange(mappedEntities);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task AddRange(List<CorrectiveActionFishbone> correctiveActionFishbones)
        {
            try
            {
                var mappedEntities = _mapper.Map<List<Entities.Quality.CorrectiveActionFishbone>>(correctiveActionFishbones);
                await _ctx.CorrectiveActionFishbone.AddRangeAsync(mappedEntities);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int DeleteAllByCorrectionAction(int correctiveActionId)
        {
            try
            {
                var fishboneCausesToRemove = _ctx.CorrectiveActionFishbone.Where(x => x.CorrectiveActionID == correctiveActionId).ToList();
            _ctx.CorrectiveActionFishbone.RemoveRange(fishboneCausesToRemove);
            _ctx.SaveChanges();
            return fishboneCausesToRemove.Count;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task SaveChanges()
        {
            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void EditRootReason(int correctiveActionId, string rootReason)
        {
            var acToUpdate = _ctx.CorrectiveActions.First(x => x.CorrectiveActionID == correctiveActionId);
            acToUpdate.RootReason = rootReason;

            _ctx.CorrectiveActions.Update(acToUpdate);
        }
    }
}

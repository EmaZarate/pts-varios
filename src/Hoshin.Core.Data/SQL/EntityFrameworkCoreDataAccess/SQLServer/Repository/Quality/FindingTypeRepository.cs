using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.FindingType;
using Hoshin.Quality.Domain.ParametrizationCriteria;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class FindingTypeRepository : IFindingTypeRepository
    {
        private readonly SQLHoshinCoreContext _ctx;
        private readonly IMapper _mapper;
        public FindingTypeRepository(SQLHoshinCoreContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public FindingType Add(FindingType findingType)
        {
            var findingTypeDb = _mapper.Map<FindingType, FindingTypes>(findingType);
            findingTypeDb.ParametrizationsFindingTypes = _mapper.Map<List<FindingTypeParametrization>, ICollection<ParametrizationsFindingTypes>>(findingType.Parametrizations);
            _ctx.Add(findingTypeDb);
            _ctx.SaveChanges();

            findingType.Id = findingTypeDb.FindingTypeID;

            return findingType;
        }

        public bool Delete(int id)
        {
            var res = _ctx.FindingTypes.Where(x => x.FindingTypeID == id).FirstOrDefault();
            res.Active = false;

            _ctx.FindingTypes.Update(res);
            _ctx.SaveChanges();

            return true;
        }

        public FindingType Get(string name)
        {
            var res = _ctx.FindingTypes.Where(x => x.Name == name).FirstOrDefault();
            if (res == null)
            {
                return null;
            }
            return _mapper.Map<FindingTypes, FindingType>(res);
        }

        public FindingType Get(int id)
        {
            var res = _ctx.FindingTypes
                .Include(x => x.ParametrizationsFindingTypes)
                .Where(x => x.FindingTypeID == id).FirstOrDefault();
            if (res == null)
            {
                return null;
            }
            return _mapper.Map<FindingTypes, FindingType>(res);
        }

        public List<FindingType> GetAll()
        {
            var res = _ctx.FindingTypes.Include(x => x.ParametrizationsFindingTypes).ToList();
            return _mapper.Map<List<FindingTypes>, List<FindingType>>(res);
        }

        public List<FindingType> GetAllActive()
        {
            var res = _ctx.FindingTypes.Include(x => x.ParametrizationsFindingTypes).ThenInclude(x => x.ParametrizationCriteria).Where(x => x.Active == true).OrderBy( x => x.Name).ToList();
            return _mapper.Map<List<FindingTypes>, List<FindingType>>(res);
        }

        public List<FindingType> GetAllForAudit()
        {
            const int PARAMETRIZATON_CRITERIA_EN_AUDITORIA_ID = 3;

            var res = _ctx.FindingTypes
                .Include(x => x.ParametrizationsFindingTypes)
                .Where(ft => ft.ParametrizationsFindingTypes
                    .Where(pft => pft.ParametrizationCriteriaID == PARAMETRIZATON_CRITERIA_EN_AUDITORIA_ID).FirstOrDefault().Value == "true").ToList();

            return _mapper.Map<List<FindingTypes>, List<FindingType>>(res);
        }

        public FindingType Update(FindingType updateFindingType)
        {
            var findingType = _ctx.FindingTypes.Where(x => x.FindingTypeID == updateFindingType.Id).FirstOrDefault();
            findingType.Name = updateFindingType.Name;
            findingType.Code = updateFindingType.Code;
            findingType.Active = updateFindingType.Active;
            findingType.ParametrizationsFindingTypes = _mapper.Map<List<FindingTypeParametrization>,List<ParametrizationsFindingTypes>>(updateFindingType.Parametrizations);
            _ctx.FindingTypes.Update(findingType);
            _ctx.SaveChanges();
            return updateFindingType;

        }
    }
}

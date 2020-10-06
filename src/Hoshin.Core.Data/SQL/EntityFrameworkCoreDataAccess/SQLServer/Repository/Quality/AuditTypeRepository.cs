using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.Quality.Domain.AuditType;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class AuditTypeRepository : IAuditTypeRepository
    {
        private readonly SQLHoshinCoreContext _ctx;
        private readonly IMapper _mapper;

        public AuditTypeRepository(SQLHoshinCoreContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public AuditType Get(int id)
        {
            return _mapper.Map<AuditsTypes, AuditType>(_ctx.AuditsTypes.Find(id));
        }

        public async Task<List<AuditType>> GetAll()
        {
            var list = await _ctx.AuditsTypes.ToListAsync();

            return _mapper.Map<List<AuditsTypes>, List<AuditType>>(list);
        }

        public async Task<List<AuditType>> GetAllActives()
        {
            var list = await _ctx.AuditsTypes.Where(x => x.Active).ToListAsync(); ;

            return _mapper.Map<List<AuditsTypes>, List<AuditType>>(list);
        }

        public AuditType Add(AuditType auditType)
        {
            AuditsTypes typeDB = _mapper.Map<AuditType, AuditsTypes>(auditType);
            _ctx.AuditsTypes.Add(typeDB);
            _ctx.SaveChanges();

            return _mapper.Map<AuditsTypes, AuditType>(typeDB);
        }

        public AuditType Update(AuditType auditType)
        {
            AuditsTypes typeDB = _mapper.Map<AuditType, AuditsTypes>(auditType);
            _ctx.AuditsTypes.Update(typeDB);
            _ctx.SaveChanges();

            return _mapper.Map<AuditsTypes, AuditType>(typeDB);
        }

        public AuditType CheckDuplicated(AuditType auditType)
        {
            return _mapper.Map<AuditsTypes, AuditType>(_ctx.AuditsTypes.Where(x => (x.Code == auditType.Code || x.Name == auditType.Name) && x.AuditTypeID != auditType.Id).FirstOrDefault());
        }
    }
}

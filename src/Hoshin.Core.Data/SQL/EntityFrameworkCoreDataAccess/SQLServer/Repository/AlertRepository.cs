using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Domain.Alert;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository
{
    public class AlertRepository : IAlertRepository
    {
        private readonly SQLHoshinCoreContext _ctx;
        private readonly IMapper _mapper;

        public AlertRepository(SQLHoshinCoreContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<List<Alert>> GetAll()
        {
            var list = await _ctx.Alert.ToListAsync();
            return _mapper.Map<List<Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Alert>, List<Alert>>(list);
        }
    }
}

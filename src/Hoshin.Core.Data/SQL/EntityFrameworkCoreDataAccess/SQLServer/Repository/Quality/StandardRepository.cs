using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.Standard;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class StandardRepository : IStandardRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public StandardRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public string Add(Standard newAuditState)
        {
            string validation = string.Empty;
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                validation = ValidateStandard(_ctx, _mapper, newAuditState);

                if (!string.IsNullOrEmpty(validation)) return validation;

                _ctx.Standards.Add(_mapper.Map<Standard, Standards>(newAuditState));
                _ctx.SaveChanges();

                return validation;
            }
        }

        public Standard Get(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var standard = _ctx.Standards.Include(x=>x.Aspects).Where(x => x.StandardID == id).SingleOrDefault();
                return _mapper.Map<Standards, Standard>(standard);
            }
        }

        public List<Standard> GetAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var standards = _ctx.Standards
                    .Include(x => x.Aspects)                 
                    .ToList();
                return _mapper.Map<List<Standards>, List<Standard>>(standards);
            }
        }

        public string Update(Standard updateStandard)
        {
            string validation = string.Empty;
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                validation = ValidateStandard(_ctx, _mapper, updateStandard);

                if (string.IsNullOrEmpty(validation))
                {
                    updateStandard.Aspects.All(x => { x.AspectID = x.AspectID <= -1 ? 0 : x.AspectID; return true; });
                }
                else
                {
                    return validation;
                }

                _ctx.Standards.Update(_mapper.Map<Standard, Standards>(updateStandard));
                _ctx.SaveChanges();
            }
            return validation;
        }
        
        private string ValidateStandard(SQLHoshinCoreContext _ctx, IMapper _mapper,Standard standard)
        {
            string validation = string.Empty;
            Standards standardValidation = _ctx.Standards.Where(x => (x.Name == standard.Name || x.Code == standard.Code) && x.StandardID != standard.StandardID).FirstOrDefault();
            validation = standardValidation != null ? "NameExist" : string.Empty;

            if (!string.IsNullOrEmpty(validation)) return validation;

            validation = standard.Aspects.Count() == standard.Aspects.Where(x => !x.Active).Count() ? "OneActive" : string.Empty;

            if (!string.IsNullOrEmpty(validation)) return validation;            

            return validation;
        }

    }
}

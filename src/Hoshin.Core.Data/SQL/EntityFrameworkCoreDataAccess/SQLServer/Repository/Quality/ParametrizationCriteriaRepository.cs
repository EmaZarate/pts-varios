using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.ParametrizationCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class ParametrizationCriteriaRepository : IParametrizationCriteriaRepository
    {
        private readonly SQLHoshinCoreContext _ctx;

        public ParametrizationCriteriaRepository(SQLHoshinCoreContext ctx)
        {
            _ctx = ctx;
        }
        public ParametrizationCriteria Add(ParametrizationCriteria newparam)
        {
            var param = _ctx.ParametrizationCriterias.Where(x => x.Name == newparam.Name).FirstOrDefault();
            if (param == null)
            {
                var paramCriteria = new ParametrizationCriterias();
                paramCriteria.Name = newparam.Name;
                paramCriteria.DataType = newparam.DataType;

                _ctx.ParametrizationCriterias.Add(paramCriteria);
                _ctx.SaveChanges();

                newparam.Id = paramCriteria.ParametrizationCriteriaID;
                return newparam;
            }
            return null;
            
        }

        public ParametrizationCriteria Get(string name)
        {
            var param =_ctx.ParametrizationCriterias.Where(x => x.Name == name).FirstOrDefault();
            if(param != null)
            {
                return new ParametrizationCriteria(param.Name, param.DataType, param.ParametrizationCriteriaID);
            }
            return null;
        }
        
        public ParametrizationCriteria Get(int id)
        {
            var param = _ctx.ParametrizationCriterias.Where(x => x.ParametrizationCriteriaID == id).FirstOrDefault();
            if(param != null)
            {
                return new ParametrizationCriteria(param.Name, param.DataType, param.ParametrizationCriteriaID);
            }
            return null;
        }

        public List<ParametrizationCriteria> GetAll()
        {
            var res = _ctx.ParametrizationCriterias.ToList();
            var list = new List<ParametrizationCriteria>();
            foreach (var pc in res)
            {
                list.Add(new ParametrizationCriteria(pc.Name, pc.DataType, pc.ParametrizationCriteriaID));
            }

            return list;
        }

        public ParametrizationCriteria Update(ParametrizationCriteria updateparam)
        {
            var paramCriteria = _ctx.ParametrizationCriterias.Where(x => x.ParametrizationCriteriaID == updateparam.Id).FirstOrDefault();
            paramCriteria.Name = updateparam.Name;
            paramCriteria.DataType = updateparam.DataType;
            _ctx.Update(paramCriteria);
            _ctx.SaveChanges();

            return updateparam;
        }
    }
}

using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Hoshin.Quality.Domain.Evidence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class FindingEvidenceRepository : IFindingEvidenceRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public FindingEvidenceRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public bool Add(int findingId, string url)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                FindingsEvidences fe = new FindingsEvidences()
                {
                    FindingID = findingId,
                    Name = FormatFileName(url),
                    Url = url
                };
                _ctx.FindingsEvidences.Add(fe);
                _ctx.SaveChanges();

                return true;
            }
        }

        public bool Delete(int findingId, string url)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var fe = _ctx.FindingsEvidences.Where(x => x.Url == url && x.FindingID == findingId).FirstOrDefault();
                if (fe != null)
                {
                    _ctx.FindingsEvidences.Remove(fe);
                    _ctx.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private string FormatFileName(string url)
        {
            string name = url.Split("findings/")[1];
            return DecodeUrlString(name);
        }

        private static string DecodeUrlString(string name)
        {
            string newName;
            while ((newName = Uri.UnescapeDataString(name)) != name)
                name = newName;
            return newName;
        }
    }
}
